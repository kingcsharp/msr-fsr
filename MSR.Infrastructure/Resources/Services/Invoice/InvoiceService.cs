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
using MSR.Domain.Models;
using MSR.Domain.Views;
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

        public async Task<InvoiceView> CreateInvoiceAsync(CreateOneInvoice command)
        {
            if (command.InvoiceItems?.Count == 0)
            {
                throw new DomainException($"{nameof(InvoiceModel)} must contain at least one WorkOrder");
            }

            var invoice = _mapper.Map<Invoice>(command);

            // Unused by required by the model
            invoice.StatusId = _unitOfWork.Status.FirstOrDefault(false, i => i.Name == nameof(ApprovalStatusEnum.Pending)).Id;

            invoice = await SaveInvoiceAsync(invoice, command.InvoiceItems);

            // Retreive saved Invoice
            var retInvoice = _mapper.Map<InvoiceView>(invoice);

            return retInvoice;
        }

        public async Task<IEnumerable<InvoiceView>> CreateInvoicesAsync(CreateIndividualInvoices command)
        {
            var retInvoices = new List<InvoiceView>();

            var statusId = _unitOfWork.Status.FirstOrDefault(false, i => i.Name == nameof(ApprovalStatusEnum.Pending)).Id;

            // Enumerate all invoices command, create a separate Invoice entry per command
            foreach (var invoiceCommand in command.Invoices)
            {
                var invoice = _mapper.Map<Invoice>(invoiceCommand);

                invoice.StatusId = statusId;

                var invoiceDb = await SaveInvoiceAsync(invoice, invoiceCommand.InvoiceItems);

                var retInvoice = _mapper.Map<InvoiceView>(invoiceDb);

                retInvoices.Add(retInvoice);
            }

            return retInvoices;
        }

        public async Task<InvoiceView> UpdateInvoiceAsync(UpdateInvoice command)
        {
            // Retreive invoice to update
            var invoice = await _unitOfWork.Invoices
                                .Query()
                                .Include(i => i.InvoiceItems)
                                .FirstOrDefaultAsync(i => i.Id == command.Id);

            if (invoice is null)
            {
                throw new DomainException($"{nameof(InvoiceModel)} not found with ID: {command.Id}");
            }

            // Clear all InvoiceItems (WorkOrders or PurscheOrders)
            invoice.InvoiceItems.Clear();

            // Update invoice details and items
            UpdateInvoiceDetails(invoice, command);

            // Update invoice totals
            CalculateTotals(invoice);

            // Save invoice changes
            await _unitOfWork.Invoices.UpdateAndSaveChangesAsync(invoice);

            var retInvoice = _mapper.Map<InvoiceView>(invoice);

            return retInvoice;
        }

        public async Task<InvoiceModel> GetInvoiceAsync(int id)
        {
            var Invoice = await _unitOfWork.Invoices.FirstOrDefaultAsync(false, i => i.Id == id);

            if (Invoice is null)
            {
                throw new DomainException($"{nameof(InvoiceModel)} does not exist with {nameof(id)}: {id}");
            }

            return _mapper.Map<InvoiceModel>(Invoice);
        }

        public async Task<IEnumerable<InvoiceModel>> GetInvoicesAsync(GetInvoices command)
        {
            var invoiceList = new List<InvoiceModel>();

            var invoices = GetFilteredInvoices(command);

            foreach (var invoice in await invoices.ToListAsync())
            {
                var isValid = ClientCheckFiltering(command, invoice);

                if (isValid)
                {
                    invoiceList.Add(_mapper.Map<InvoiceModel>(invoice));
                }
            }

            return invoiceList.AsEnumerable();
        }

        public async Task<IEnumerable<InvoiceView>> GetInvoicesAsync(GetInvoicesGridView command)
        {
            var invoiceList = new List<InvoiceView>();

            var invoices = GetFilteredInvoices(command);

            foreach (var invoice in await invoices.ToListAsync())
            {
                var isValid = ClientCheckFiltering(command, invoice);

                if (isValid)
                {
                    invoiceList.Add(_mapper.Map<InvoiceView>(invoice));
                }
            }

            return invoiceList.AsEnumerable();
        }

        private bool ClientCheckFiltering(GetInvoices command, Invoice invoice)
        {
            if (!string.IsNullOrEmpty(command.CreatedByName))
            {
                if (!invoice.Created.GetFullName().Contains(command.CreatedByName, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(command.LastUpdatedByName))
            {

                if (!invoice.LastUpdated.GetFullName().Contains(command.LastUpdatedByName, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Saves the <paramref name="invoice"/> into the Invoice repository. InvoiceItem is processed based on <paramref name="invoiceItems"/> command by creating or updating it in the repository and assigning the WorkOrder and PurscheOrder from the command
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="invoiceItems"></param>
        /// <returns></returns>
        private async Task<Invoice> SaveInvoiceAsync(Invoice invoice, IEnumerable<CreateUpdateInvoiceItem> invoiceItems)
        {
            // Load InvoiceItems properties
            invoice.InvoiceItems = invoice.InvoiceItems.Select(curItem =>
                 _unitOfWork.InvoiceItems.Query()
                                         .Include(ii => ii.WorkOrder)
                                         .ThenInclude(wo => wo.Purchase)
                                         .ThenInclude(p => p.Location)
                                         .Include(ii => ii.PurchaseOrder)
                                         .Where(ii => (ii.WorkOrderId == curItem.WorkOrderId &&
                                                                ii.PurchaseOrderId == curItem.PurchaseOrderId) ||
                                                                ii.InvoiceId == curItem.InvoiceId)
                                         .SingleOrDefault())
                .ToList()
                .Select(ii => ii ?? new InvoiceItem())
                .ToList();

            invoiceItems.ToList().ForEach(cii =>
            {
                invoice.InvoiceItems.ToList().ForEach(iii =>
                {
                    if (!invoice.InvoiceItems.Any(i => iii.WorkOrderId == cii.WorkOrderId &&
                                                        iii.PurchaseOrderId == cii.PurchaseOrderId))
                    {
                        iii.WorkOrderId = cii.WorkOrderId;
                        iii.PurchaseOrderId = cii.PurchaseOrderId;
                    }

                });
            });

            // Create temporary InvoiceNumber based on Invoice Id
            invoice.InvoiceNumber = $"{DateTime.Now:yy}";

            // Save the new Invoice
            await _unitOfWork.Invoices.AddAndSaveChangesAsync(invoice);

            var invoiceDb = _unitOfWork.Invoices.Query()
                                         .Include(i => i.Customer)
                                         .Include(i => i.InvoiceItems)
                                         .ThenInclude(i => i.WorkOrder)
                                         .ThenInclude(wo => wo.Purchase)
                                         .ThenInclude(p => p.Location)
                                         .Include(i => i.InvoiceItems)
                                         .ThenInclude(i => i.PurchaseOrder)
                                         .SingleOrDefault(i => i.Id == invoice.Id);

            // Getting parent InvoiceClass from Location
            var locationInvoiceClass = invoiceDb.InvoiceItems.FirstOrDefault().WorkOrder?.Purchase?.Location?.InvoiceClass;

            // Update InvoiceNumber using the new Invoice ID
            invoiceDb.InvoiceNumber = $"{locationInvoiceClass}-{DateTime.Now:yy}-{invoiceDb.Id}";

            // Calculate Subtotal and Total based on WorkOrders
            CalculateTotals(invoiceDb);

            // Update Invoice with the new InvoiceNumber
            await _unitOfWork.Invoices.UpdateAndSaveChangesAsync(invoiceDb);

            return invoiceDb;
        }

        private void CalculateTotals(Invoice invoice)
        {
            invoice.Subtotal = invoice.InvoiceItems.Sum(x => x.WorkOrder.Price);
            invoice.Total = (decimal)((invoice.Subtotal + (invoice.Subtotal * invoice.TaxPercentage / 100)));
        }

        private IQueryable<Invoice> GetFilteredInvoices(GetInvoices command)
        {
            var invoices = _unitOfWork.Invoices.Query()
                                                .Include(i => i.Customer)
                                                    .ThenInclude(c => c.Created)
                                                    .ThenInclude(c => c.LastUpdated)
                                                .Include(i => i.Status)
                                                .Include(i => i.InvoiceItems)
                                                    .ThenInclude(ii => ii.PurchaseOrder)
                                                .Include(i => i.InvoiceItems)
                                                    .ThenInclude(ii => ii.WorkOrder)
                                                    .ThenInclude(wo => wo.Purchase)
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
            if (command.LastUpdatedOn.HasValue)
            {
                invoices = invoices.Where(i => i.LastUpdatedOn == command.LastUpdatedOn);
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
