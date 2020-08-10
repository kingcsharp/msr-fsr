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

        public async Task<Domain.Models.InvoiceModel> CreateInvoiceAsync(CreateOneInvoice command)
        {
            var invoice = _mapper.Map<Invoice>(command);

            // Unused by required by the model
            invoice.StatusId = _unitOfWork.Status.FirstOrDefault(false, i => i.Name == "Pending").Id;

            if (command.InvoiceItems?.Count == 0)
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
            var locationInvoiceClass = invoice.InvoiceItems.FirstOrDefault().WorkOrder.Purchase?.Location?.InvoiceClass;

            // Create temporary InvoiceNumber based on Invoice Id
            invoice.InvoiceNumber = $"{locationInvoiceClass}-{DateTime.Now:yy}";

            // Calculate Subtotal and Total
            InvoiceCalculateTotals(invoice);

            // Save the new Invoice
            await _unitOfWork.Invoices.AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync();

            // Retreive saved Invoice
            var retInvoice = _mapper.Map<Domain.Models.InvoiceModel>(invoice);

            invoice.InvoiceNumber = $"{retInvoice.InvoiceNumber}-{retInvoice.Id}";
            retInvoice.InvoiceNumber = invoice.InvoiceNumber;

            // Update Invoice with InvoiceNumber
            _unitOfWork.Invoices.Update(invoice);

            await _unitOfWork.SaveChangesAsync();

            return retInvoice;
        }

        private void InvoiceCalculateTotals(Invoice invoice)
        {
            invoice.Subtotal = invoice.InvoiceItems.Sum(x => x.WorkOrder.Price);
            invoice.Total = (decimal)((invoice.Subtotal + (invoice.Subtotal * invoice.TaxPercentage / 100)));
        }

        public async Task<IEnumerable<Domain.Models.InvoiceModel>> CreateInvoicesAsync(CreateIndividualInvoices command)
        {
            var invoices = new List<Invoice>();
            var retInvoices = new List<Domain.Models.InvoiceModel>();

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
                var locationInvoiceClass = invoice.InvoiceItems.FirstOrDefault().WorkOrder.Purchase?.Location?.InvoiceClass;

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
                    var retInvoice = _mapper.Map<Domain.Models.InvoiceModel>(invoice);

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

        public async Task<Domain.Models.InvoiceModel> UpdateInvoiceAsync(UpdateInvoice command)
        {
            var curInvoice = await _unitOfWork.Invoices.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (curInvoice is null)
            {
                throw new DomainException($"{nameof(Domain.Models.InvoiceModel)} not found with ID: {command.Id}");
            }

            command.InvoiceItems?.ToList().ForEach(invoiceItem =>
            {
                if (!_unitOfWork.WorkOrders.Exists(x => x.Id == invoiceItem.WorkOrderId))
                {
                    throw new DomainException($"WorkOrder not found", DomainError.NotFound);
                }

                if (!_unitOfWork.PurchaseOrders.Exists(x => x.Id == invoiceItem.PurchaseOrderId))
                {
                    throw new DomainException($"PurchaseOrder not found", DomainError.NotFound);
                }
            });

            curInvoice.InvoiceItems.ToList().ForEach(invoiceItem => _unitOfWork.InvoiceItems.Delete(false, invoiceItem, true));

            UpdateInvoiceRecord(curInvoice, command);

            _unitOfWork.Invoices.Update(curInvoice);

            await _unitOfWork.SaveChangesAsync();

            var retInvoice = _mapper.Map<Domain.Models.InvoiceModel>(curInvoice);

            InvoiceCalculateTotals(curInvoice);

            _unitOfWork.Invoices.Update(curInvoice);

            await _unitOfWork.SaveChangesAsync();

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
            var invoices = _unitOfWork.Invoices.Query();

            if (command.Id > 0)
            {
                invoices = invoices.Where(i => i.Id == command.Id);
            }
            if (command.CustomerId > 0)
            {
                invoices = invoices.Where(i => i.CustomerId == command.CustomerId);
            }
            if (!string.IsNullOrEmpty(command.InvoiceNumber))
            {
                invoices = invoices.Where(i => i.InvoiceNumber == command.InvoiceNumber);
            }
            if (!string.IsNullOrWhiteSpace(command.Description))
            {
                invoices = invoices.Where(i => i.Description == command.Description);
            }
            if (command.InvoiceDate > DateTime.MinValue)
            {
                invoices = invoices.Where(i => i.InvoiceDate == command.InvoiceDate);
            }
            if (command.Total.HasValue)
            {
                invoices = invoices.Where(i => i.Total == command.Total);
            }
            if (command.StatusId.HasValue)
            {
                invoices = invoices.Where(i => i.StatusId == command.StatusId);
            }

            foreach (var invoice in await invoices.Include(i => i.Customer).ToListAsync())
            {
                invoiceList.Add(_mapper.Map<Domain.Models.InvoiceModel>(invoice));
            }

            return invoiceList.AsEnumerable();
        }

        public async Task<IEnumerable<Domain.Views.InvoiceView>> GetInvoicesAsync(GetInvoicesGridView command)
        {
            var invoiceList = new List<Domain.Views.InvoiceView>();
            // TO FIX: Including Customer returns an empty enumeration
            var invoices = _unitOfWork.Invoices.Query();//.Include("Customer");

            if (command.Id > 0)
            {
                invoices = invoices.Where(i => i.Id == command.Id);
            }
            if (command.CustomerId > 0)
            {
                invoices = invoices.Where(i => i.CustomerId == command.CustomerId);
            }
            if (!string.IsNullOrEmpty(command.InvoiceNumber))
            {
                invoices = invoices.Where(i => i.InvoiceNumber == command.InvoiceNumber);
            }
            if (!string.IsNullOrWhiteSpace(command.Description))
            {
                invoices = invoices.Where(i => i.Description == command.Description);
            }
            if (command.InvoiceDate > DateTime.MinValue)
            {
                invoices = invoices.Where(i => i.InvoiceDate == command.InvoiceDate);
            }
            if (command.Total.HasValue)
            {
                invoices = invoices.Where(i => i.Total == command.Total);
            }
            if (command.StatusId.HasValue)
            {
                invoices = invoices.Where(i => i.StatusId == command.StatusId);
            }

            foreach (var invoice in invoices.ToList())
            {
                invoiceList.Add(_mapper.Map<Domain.Views.InvoiceView>(invoice));
            }

            return invoiceList.AsEnumerable();
        }

        private void UpdateInvoiceRecord(Invoice curInvoice, UpdateInvoice command)
        {
            curInvoice.Description = command.Description ?? curInvoice.Description;
            curInvoice.InvoiceDate = command.InvoiceDate > DateTime.MinValue ? command.InvoiceDate : curInvoice.InvoiceDate;
            curInvoice.InvoiceItems = command.InvoiceItems?.Select(x => new InvoiceItem()
            {
                WorkOrderId = x.WorkOrderId,
                WorkOrder = _unitOfWork.WorkOrders.Query().FirstOrDefault(wo => wo.Id == x.WorkOrderId),
                PurchaseOrderId = x.PurchaseOrderId,
                PurchaseOrder = _unitOfWork.PurchaseOrders.Query().FirstOrDefault(po => po.Id == x.PurchaseOrderId)
            }).ToList();
            curInvoice.TaxPercentage = command.TaxPercentage ?? curInvoice.TaxPercentage;
        }
    }
}
