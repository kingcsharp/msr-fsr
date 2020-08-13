using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Resources.Services.Invoices
{
    public class InvoiceService : IInvoiceService

    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Domain.Views.InvoiceView> CreateInvoiceAsync(CreateOneInvoice command)
        {
            if (command.InvoiceItems?.Count == 0)
            {
                throw new DomainException($"{nameof(Domain.Models.InvoiceModel)} must contain at least one WorkOrder");
            }

            var invoice = _mapper.Map<Invoice>(command);

            // Unused by required by the model
            invoice.StatusId = _unitOfWork.Status.FirstOrDefault(false, i => i.Name == "Pending").Id;

            // Load InvoiceItems properties
            invoice.InvoiceItems = invoice.InvoiceItems.Select(curItem =>
                 _unitOfWork.InvoiceItems.Query()
                                         .Include(ii => ii.WorkOrder)
                                         .ThenInclude(wo => wo.Purchase)
                                         .ThenInclude(p => p.Location)
                                         .Include(ii => ii.PurchaseOrder)
                                         .First(ii => ii.Id == curItem.Id)
            ).ToList();

            // Getting parent InvoiceClass from Location
            var locationInvoiceClass = invoice.InvoiceItems.FirstOrDefault().WorkOrder.Purchase?.Location?.InvoiceClass;

            // Create temporary InvoiceNumber based on Invoice Id
            invoice.InvoiceNumber = $"{locationInvoiceClass}-{DateTime.Now:yy}";

            // Calculate Subtotal and Total
            UpdateInvoiceTotals(invoice);

            // Save the new Invoice
            await _unitOfWork.Invoices.AddAndSaveChangesAsync(invoice);

            // Retreive saved Invoice
            var retInvoice = _mapper.Map<Domain.Views.InvoiceView>(invoice);

            // Update InvoiceNumber using the Invoice ID
            invoice.InvoiceNumber = $"{retInvoice.InvoiceNumber}-{retInvoice.Id}";
            retInvoice.InvoiceNumber = invoice.InvoiceNumber;

            // Update Invoice with the new InvoiceNumber
            await _unitOfWork.Invoices.UpdateAndSaveChangesAsync(invoice);

            return retInvoice;
        }

        private void UpdateInvoiceTotals(Invoice invoice)
        {
            invoice.Subtotal = invoice.InvoiceItems.Sum(x => x.WorkOrder.Price);
            invoice.Total = (decimal)((invoice.Subtotal + (invoice.Subtotal * invoice.TaxPercentage / 100)));
        }

        public async Task<IEnumerable<Domain.Views.InvoiceView>> CreateInvoicesAsync(CreateIndividualInvoices command)
        {
            var invoices = new List<Invoice>();
            var retInvoices = new List<Domain.Views.InvoiceView>();

            var statusId = _unitOfWork.Status.FirstOrDefault(false, i => i.Name == "Pending").Id;

            command.Invoices?.ToList().ForEach(async invoiceCommand =>
            {
                var invoice = _mapper.Map<Invoice>(invoiceCommand);

                // Unused by required by the model
                invoice.StatusId = statusId;

                if (invoiceCommand.InvoiceItems?.Count == 0)
                {
                    throw new DomainException($"{nameof(Domain.Models.InvoiceModel)} must contain at least one WorkOrder");
                }

                // Verifying WorkOrder and PurchaseOrder IDs
                invoice.InvoiceItems.ToList().ForEach(item =>
                {
                    item.WorkOrder = _unitOfWork.WorkOrders.Query().FirstOrDefault(x => x.Id == item.WorkOrderId);

                    if (item.WorkOrder is null)
                    {
                        throw new DomainException($"WorkOrder not found", DomainError.NotFound);
                    }

                    item.PurchaseOrder = _unitOfWork.PurchaseOrders.Query().FirstOrDefault(x => x.Id == item.PurchaseOrderId);

                    if (item.PurchaseOrder is null)
                    {
                        throw new DomainException($"PurchaseOrder not found", DomainError.NotFound);
                    }
                });

                // Getting parent InvoiceClass from Location
                var locationInvoiceClass = invoice.InvoiceItems.First().WorkOrder.Purchase?.Location?.InvoiceClass;

                // Create temporary InvoiceNumber based on Invoice Id
                invoice.InvoiceNumber = $"{locationInvoiceClass}-{DateTime.Now:yy}";

                // Save the new Invoice
                await _unitOfWork.Invoices.AddAsync(invoice);

                invoices.Add(invoice);
            });

            if (invoices.Count > 0)
            {
                await _unitOfWork.SaveChangesAsync();

                invoices.ForEach(invoice =>
                {
                    // Retreive saved Invoice
                    var retInvoice = _mapper.Map<Domain.Views.InvoiceView>(invoice);

                    invoice.InvoiceNumber = $"{retInvoice.InvoiceNumber}-{retInvoice.Id}";
                    retInvoice.InvoiceNumber = invoice.InvoiceNumber;

                    // Update Invoice with InvoiceNumber
                    _unitOfWork.Invoices.Update(invoice);

                    retInvoices.Add(retInvoice);
                });

                await _unitOfWork.SaveChangesAsync();
            }

            return retInvoices;
        }

        public async Task<Domain.Views.InvoiceView> UpdateInvoiceAsync(UpdateInvoice command)
        {
            // Retreive invoice to update
            var invoice = await _unitOfWork.Invoices
                                .Query()
                                .Include(i => i.InvoiceItems)
                                .FirstOrDefaultAsync(i => i.Id == command.Id);

            if (invoice is null)
            {
                throw new DomainException($"{nameof(Domain.Models.InvoiceModel)} not found with ID: {command.Id}");
            }

            // Clear all InvoiceItems (WorkOrders or PurscheOrders)
            invoice.InvoiceItems.Clear();

            // Update invoice details and items
            UpdateInvoiceDetails(invoice, command);

            // Update invoice totals
            UpdateInvoiceTotals(invoice);

            // Save invoice changes
            await _unitOfWork.Invoices.UpdateAndSaveChangesAsync(invoice);

            var retInvoice = _mapper.Map<Domain.Views.InvoiceView>(invoice);

            return retInvoice;
        }

        public async Task<Domain.Models.InvoiceModel> GetInvoiceAsync(int id)
        {
            var Invoice = await _unitOfWork.Invoices.FirstOrDefaultAsync(false, i => i.Id == id);

            if (Invoice is null)
            {
                throw new DomainException($"{nameof(Domain.Models.InvoiceModel)} does not exist with {nameof(id)}: {id}");
            }

            return _mapper.Map<Domain.Models.InvoiceModel>(Invoice);
        }

        public async Task<IEnumerable<Domain.Models.InvoiceModel>> GetInvoicesAsync(GetInvoices command)
        {
            var invoiceList = new List<Domain.Models.InvoiceModel>();
           
            var invoices = GetFilteredInvoices(command);

            foreach (var invoice in await invoices.ToListAsync())
            {
                invoiceList.Add(_mapper.Map<Domain.Models.InvoiceModel>(invoice));
            }

            return invoiceList.AsEnumerable();
        }

        private IQueryable<Invoice> GetFilteredInvoices(GetInvoices command)
        {
            var invoices = _unitOfWork.Invoices.Query()
                                                .Include(i => i.Customer)
                                                .Include(i => i.Status)
                                                .Include(i => i.InvoiceItems).ThenInclude(ii => ii.PurchaseOrder)
                                                .Include(i => i.InvoiceItems).ThenInclude(ii => ii.WorkOrder).ThenInclude(wo => wo.Purchase)
                                                .AsQueryable();

            if (command.Id.HasValue)
            {
                invoices = invoices.Where(i => i.Id == command.Id);
            }
            if (!string.IsNullOrEmpty(command.CustomerName))
            {
                invoices = invoices.Where(i => i.Customer.Name.ToLower().Contains(command.CustomerName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(command.Description))
            {
                invoices = invoices.Where(i => i.Description.Contains(command.Description));
            }
            if (!string.IsNullOrEmpty(command.InvoiceNumber))
            {
                invoices = invoices.Where(i => i.InvoiceNumber == command.InvoiceNumber);
            }
            if (command.DueDate.HasValue)
            {
                invoices = invoices.Where(i => i.InvoiceDate == command.DueDate);
            }
            if (command.CreatedOn.HasValue)
            {
                invoices = invoices.Where(i => i.CreatedOn == command.CreatedOn);
            }
            if (!string.IsNullOrEmpty(command.CreatedByName))
            {
                invoices = invoices.Where(i => i.Customer.Created.FirstName.ToLower().Contains(command.CreatedByName.ToLower()) || i.Customer.Created.LastName.ToLower().Contains(command.CreatedByName.ToLower()));
            }
            if (command.LastUpdatedOn.HasValue)
            {
                invoices = invoices.Where(i => i.LastUpdatedOn == command.LastUpdatedOn);
            }
            if (!string.IsNullOrEmpty(command.LastUpdatedByName))
            {
                invoices = invoices.Where(i => i.Customer.Created.FirstName.ToLower().Contains(command.LastUpdatedByName.ToLower()) || i.Customer.Created.LastName.ToLower().Contains(command.LastUpdatedByName.ToLower()));
            }
            if (command.Amount.HasValue)
            {
                invoices = invoices.Where(i => i.Total == command.Amount);
            }
            if (command.StatusId.HasValue)
            {
                invoices = invoices.Where(i => i.StatusId == command.StatusId);
            }

            return invoices;
        }

        public async Task<IEnumerable<Domain.Views.InvoiceView>> GetInvoicesAsync(GetInvoicesGridView command)
        {
            var invoiceList = new List<Domain.Views.InvoiceView>();

            var invoices = GetFilteredInvoices(command);

            foreach (var invoice in await invoices.ToListAsync())
            {
                invoiceList.Add(_mapper.Map<Domain.Views.InvoiceView>(invoice));
            }

            return invoiceList.AsEnumerable();
        }

        /// <summary>
        /// Update Invoice Details based on REQ365
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="command"></param>
        private void UpdateInvoiceDetails(Invoice invoice, UpdateInvoice command)
        {
            invoice.Description = command.Description ?? invoice.Description;
            invoice.InvoiceDate = command.InvoiceDate > DateTime.MinValue ? command.InvoiceDate : invoice.InvoiceDate;
            invoice.InvoiceItems = command.InvoiceItems.Select(x =>
                    _unitOfWork.InvoiceItems.Query()
                                            .Include(ii => ii.WorkOrder)
                                            .Include(ii => ii.PurchaseOrder)
                                            .First(ii => ii.Id == x.Id)
            ).ToList();
            invoice.TaxPercentage = command.TaxPercentage ?? invoice.TaxPercentage;
        }
    }
}
