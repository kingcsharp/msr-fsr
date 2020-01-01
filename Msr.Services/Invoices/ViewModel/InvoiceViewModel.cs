using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.Invoices;
using Msr.Services.PurchesOrder;

namespace Msr.Services.Invoices.ViewModel
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {
            StatusList = new List<SelectListItem>();
            ClientList = new List<SelectListItem>();
            InvoiceIdList = new List<SelectListItem>();
            PoList = new List<POListItem>();
            InvoiceWorkItem = new List<InvoiceWorkItem>();
            InvoiceItemList = new List<InvoicePoWorkItem>();
        }
        public int? Id { get; set; }

        [Required]
        public string Client { get; set; }

        public string InvoiceDescription { get; set; }

        public string Status { get; set; }

        [Required]
        public DateTime? InvoiceDate { get; set; }

        public int InvoiceNumber { get; set; }

        public decimal? Tax { get; set; }

        public string Items { get; set; }

        public string Supplier { get; set; }

        public string InvoiceId { get; set; }

        public string InvoiceClass { get; set; }
        public List<InvoicePoWorkItem> InvoiceItemList { get; set; }
        public List<SelectListItem> ClientList { get; set; }

        public List<SelectListItem> StatusList { get; set; }

        public List<SelectListItem> InvoiceIdList { get; set; }

        public List<POListItem> PoList { get; set; }

        public List<InvoiceWorkItem> InvoiceWorkItem { get; set; }
        public void SetUp(PurchesOrderService purchesOrderService, InvoicesService invoicesService, bool onEdit)
        {

            StatusList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "INVOICED",
                    Value = "INVOICED",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "OUTSTANDING",
                    Value = "OUTSTANDING"
                },
                new SelectListItem
                {
                    Text = "OVERDUE",
                    Value = "OVERDUE"
                },
                new SelectListItem
                {
                    Text = "CLOSED",
                    Value = "CLOSED"
                }
            };

            InvoiceIdList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "PDX",
                    Value = "03",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = "PHX",
                    Value = "04"
                },
                new SelectListItem
                {
                    Text = "IRE",
                    Value = "05"
                },
                new SelectListItem
                {
                    Text = "ISL",
                    Value = "06"
                },

            };


            ClientList = purchesOrderService.GetCompaniesList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();
            ClientList.Insert(0, new SelectListItem() { Value = "", Text = @"Select Client" });
        }

        public InvoiceViewModel MapToDto(Invoice invoiceView)
        {
            return new InvoiceViewModel
            {
                Id = invoiceView.Id,
                Client = invoiceView.Client,
                InvoiceDescription = invoiceView.Description,
                Status = invoiceView.Status,
                InvoiceDate = invoiceView.InvoiceDate,
                Tax = invoiceView.Tax,
                InvoiceClass = invoiceView.InvoiceClass,
                Items = invoiceView.Items,
                Supplier = invoiceView.Supplier,
            };

        }
    }
}
