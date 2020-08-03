using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

using MSR.Domain.Abstractions.QuickBooks;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Domain.Abstractions.Services;

namespace MSR.Infrastructure.Resources.Services.Invoices
{
    public class QuickbooksService : IQuickbooksService
    {
        private const string invoiceDelimiter = "\t";

        private IUnitOfWork _unitOfWork;
        private IUserService _userService;

        public QuickbooksService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<IEnumerable<QuickbooksFormatterModel>> FormatAsync(FormatQuickbooks command)
        {
            return command.Invoices.Select(invoice =>
            {
                (string invoiceNumber, string data, string type) = FormatInvoiceDataAsync(invoice.Id, command.FormatType)
                                                                    .ConfigureAwait(false)
                                                                    .GetAwaiter()
                                                                    .GetResult();

                var quickbooksFormatter = new QuickbooksFormatterModel()
                {
                    Invoice = invoice,
                    Data = data,
                    ExportFileName = $"{invoiceNumber}.{type.ToLower()}"
                };

                return quickbooksFormatter;
            }).ToList();
        }

        private async Task<(string, string, string)> FormatInvoiceDataAsync(int invoiceId, string formatType)
        {
            switch (formatType.ToLower())
            {
                case "iif":
                    (string invoiceNumber, string data) = await FormatInvoiceDataAsIIFAsync(invoiceId);

                    return (invoiceNumber, data, "iif");
                default:
                    throw new InvalidOperationException($"Quickbooks formatter type {formatType} not supported");
            }
        }

        private async Task<(string, string)> FormatInvoiceDataAsIIFAsync(int invoiceId)
        {
            var invoiceFileText = new StringBuilder();

            var invoice = _unitOfWork.Invoices.Query().Where(x => x.Id == invoiceId).SingleOrDefault();

            if (invoice == null)
            {
                throw new InvalidOperationException($"Unable to format invoice. Invoice {invoiceId} not found");
            }

            var locationInvoiceClass = invoice.InvoiceItems?.FirstOrDefault().WorkOrder?.Purchase?.Location?.InvoiceClass;

            var invoiceNumber = $"{locationInvoiceClass}-{DateTime.Now:yy}-{invoice.Id}";

            invoiceFileText.AppendFormat("!TRNS{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("TRNSID{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("TRNSTYPE{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("DATE{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("ACCNT{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("NAME{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("CLASS{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("AMOUNT{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("DOCNUM{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("MEMO{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("CLEAR{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("TOPRINT{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("ADDR1{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("ADDR2{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("ADDR3{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("ADDR4{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("ADDR5{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("DUEDATE{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("TERMS{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("PAID{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("SHIPDATE{0}", invoiceDelimiter);
            invoiceFileText.Append(Environment.NewLine);

            invoiceFileText.AppendFormat("!SPL{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("SPLID{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("TRNSTYPE{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("DATE{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("ACCNT{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("NAME{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("CLASS{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("AMOUNT{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("DOCNUM{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("MEMO{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("CLEAR{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("QNTY{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("PRICE{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("INVITEM{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("PAYMETH{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("TAXABLE{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("REIMBEXP{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("EXTRA{0}", invoiceDelimiter);
            invoiceFileText.Append(Environment.NewLine);
            invoiceFileText.AppendFormat("!ENDTRNS");
            invoiceFileText.Append(Environment.NewLine);

            invoiceFileText.AppendFormat("TRNS{0}", invoiceDelimiter); //TRNS
            invoiceFileText.AppendFormat("{0}{1}", invoiceNumber, invoiceDelimiter); //TRNSID
            invoiceFileText.AppendFormat("{0}{1}", "INVOICE", invoiceDelimiter); //TRNSTYPE
            invoiceFileText.AppendFormat("{0:d}{1}", invoice.InvoiceDate, invoiceDelimiter); //DATE
            invoiceFileText.AppendFormat("{0}{1}", "1100", invoiceDelimiter); //ACCNT
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //NAME
            invoiceFileText.AppendFormat("{0}{1}", invoice.InvoiceClass, invoiceDelimiter); //CLASS
            invoiceFileText.AppendFormat("{0}{1}", invoice.Subtotal, invoiceDelimiter); //AMOUNT
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //DOCNUM
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //MEMO
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //CLEAR
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //TOPRINT
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //ADDR1
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //ADDR2
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //ADDR3
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //ADDR4
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //ADDR5
            invoiceFileText.AppendFormat("{0:d}{1}", "", invoiceDelimiter); //DUEDATE
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //TERMS
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //PAID
            invoiceFileText.AppendFormat("{0:d}{1}", "", invoiceDelimiter); //SHIPDATE

            invoiceFileText.Append(Environment.NewLine);

            foreach (var item in invoice.InvoiceItems ?? new List<EntityFramework.Entities.InvoiceItem>())
            {
                var purchaseName = await GetPurchaseName(item);
                var qnty = (decimal)item.WorkOrder?.Purchase?.Qty * item.WorkOrder?.Purchase?.PurchasePrice;

                invoiceFileText.AppendFormat("SPL{0}", invoiceDelimiter); //SPL
                invoiceFileText.AppendFormat("{0}{1}", invoiceNumber, invoiceDelimiter); //SPLID
                invoiceFileText.AppendFormat("{0}", "INVOICE"); //TRNSTYPE
                invoiceFileText.AppendFormat("{0:d}{1}", invoice.InvoiceDate, invoiceDelimiter); //DATE
                invoiceFileText.AppendFormat("{0}{1}", "1100", invoiceDelimiter); //ACCNT
                invoiceFileText.AppendFormat("{0}{1}", purchaseName /*item.Purchaser*/ , invoiceDelimiter); //NAME
                invoiceFileText.AppendFormat("{0}{1}", item.WorkOrder.Purchase.Location.InvoiceClass /*invoice.InvoiceClass*/, invoiceDelimiter); //CLASS
                invoiceFileText.AppendFormat("{0}{1}", item.WorkOrder.Purchase.PurchasePrice /*item.Amount*/ , invoiceDelimiter); //AMOUNT
                invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //DOCNUM
                invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //MEMO
                invoiceFileText.AppendFormat("{0}{1}", item.WorkOrder.Purchase.Qty /*item.FillQty*/ , invoiceDelimiter); //MEMO
                invoiceFileText.AppendFormat("{0}{1}", qnty /*item.TotalSalePrice*/ , invoiceDelimiter); //QNTY
                invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //PRICE
                invoiceFileText.AppendFormat("{0}{1}", "Y", invoiceDelimiter); //INVITEM
                invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //PAYMETH
                invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //TAXABLE
                invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //REIMBEXP
                invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //EXTRA
                invoiceFileText.Append(Environment.NewLine);
            }

            invoiceFileText.AppendFormat("SPL{0}", invoiceDelimiter);
            invoiceFileText.AppendFormat("{0}{1}", invoiceNumber, invoiceDelimiter);
            invoiceFileText.AppendFormat("{0:d}{1}", "INVOICE", invoiceDelimiter);
            invoiceFileText.AppendFormat("{0:d}{1}", invoice.InvoiceDate, invoiceDelimiter);
            invoiceFileText.AppendFormat("{0}{1}", "1100", invoiceDelimiter); //ACCNT
            invoiceFileText.AppendFormat("{0}{1}", "Sales Tax Payable", invoiceDelimiter); //NAME
            invoiceFileText.AppendFormat("{0}{1}", invoice.InvoiceClass, invoiceDelimiter); //CLASS
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //AMOUNT
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //DOCNUM
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //MEMO
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //MEMO
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //Qty
            invoiceFileText.AppendFormat("{0}%", invoice.TaxPercentage); //PRICE
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter);//INVITEM
            invoiceFileText.AppendFormat("{0}{1}", "N", invoiceDelimiter); //PAYMETH
            invoiceFileText.AppendFormat("{0}{1}", "Y", invoiceDelimiter);//TAXABLE
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //REIMBEXP
            invoiceFileText.AppendFormat("{0}{1}", "", invoiceDelimiter); //EXTRA

            invoiceFileText.Append(Environment.NewLine);

            invoiceFileText.AppendFormat("ENDTRNS");

            return (invoiceNumber, invoiceFileText.ToString());
        }

        private async Task<string> GetPurchaseName(EntityFramework.Entities.InvoiceItem item)
        {
            if (item?.WorkOrder?.Purchase != null)
            {
                var workOrderCreator = await _userService.GetUserAsync(item.WorkOrder.Purchase.CreatedBy);

                if (!string.IsNullOrEmpty(workOrderCreator.FullName?.Trim()))
                {
                    return workOrderCreator.FullName;
                }
            }

            return "N/A";
        }

        public async Task<MemoryStream> ArchiveInvoicesAsync(ArchiveQuickbooksInvoices command)
        {
            var invoiceArchiveMemoryStream = new MemoryStream();

            using (var invoiceArchive = new ZipArchive(invoiceArchiveMemoryStream, ZipArchiveMode.Create, true))
            {

                foreach (var formattedInvoice in command.FormattedInvoices)
                {
                    var fileName = Path.ChangeExtension(formattedInvoice.ExportFileName, command.FormatType ?? "iif");

                    var entry = invoiceArchive.CreateEntry(fileName);

                    using (var entryStream = entry.Open())
                    {
                        using (var entryStreamWriter = new StreamWriter(entryStream))
                        {
                            await entryStreamWriter.WriteAsync(formattedInvoice.Data);
                        }
                    }
                }
            }

            invoiceArchiveMemoryStream.Seek(0, SeekOrigin.Begin);

            return invoiceArchiveMemoryStream;
        }
    }
}
