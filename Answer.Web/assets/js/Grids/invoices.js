var Msr = Msr || {};

Msr.InvoicesGrid = Msr.InvoicesGrid || {
    GetReturnUrl: function () {
        return "/Invoices";
    },
    GetGridId: function () {
        return "jq-grid-invoice";
    },
    GetGridEditUrl: function () {
        return "/Invoices/Edit/";
    },
    DownloadFilteredInvoices: function () {
        const baseURL = window.location.protocol + "//" + window.location.host + "/Invoices/DownloadFilteredInvoices";
        const postData = $("#" + Msr.InvoicesGrid.GetGridId()).jqGrid('getGridParam', 'postData');
        
        let downloadInvoicesURL = baseURL + '?';

        $.each(postData, function (key, value) {

            if (downloadInvoicesURL.endsWith("?") === true) {
                downloadInvoicesURL += key + "=" + encodeURIComponent(value);
            } else {
                downloadInvoicesURL += "&" + key + "=" + encodeURIComponent(value);
            }

        });

        // window.location.href = downloadInvoicesURL;
        window.open(downloadInvoicesURL);
    },
    DownloadAllInvoices: function () {
        const baseURL = window.location.protocol + "//" + window.location.host + "/Invoices/DownloadAllInvoices";

        let downloadInvoicesURL = baseURL;

        window.open(downloadInvoicesURL);
    },
    LoadInvoices: function (url) {

        $.jgrid.defaults.styleUI = 'Bootstrap';

        $("#" + Msr.InvoicesGrid.GetGridId()).jqGrid({
            url: url,
            mtype: "GET",
            styleUI: 'Bootstrap',
            datatype: "local",
            colModel: [
                { name: 'Id', index: 'Id', width: 60, align: 'center', hidden: true, edittype: 'text', editable: true, editrules: { edithidden: true } },
                {
                    label: 'Customer',
                    name: 'Client',
                    index: 'Client',
                    key: true,
                    colmenu: true,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 100,
                    align: 'left'
                },

                {
                    label: 'Description',
                    name: 'Description',
                    index: 'Description',
                    colmenu: false,
                    width: 160,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions:  { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },
                {
                    label: 'Invoice',
                    name: 'InvoiceNumber',
                    index: 'InvoiceNumber',
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },

                {
                    label: 'Amount',
                    name: 'SubTotal',
                    index: 'SubTotal',
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    formatter: 'currency',
                    formatoptions: { prefix: "$" },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },

                {
                    label: "Status",
                    name: 'Status',
                    index: 'Status',
                    colmenu: false,
                    stype: "select",
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    multiselect: true,
                    searchoptions: {
                        sopt: ['eq'],
                        value: "INVOICED:INVOICED;OUTSTANDING:OUTSTANDING;OVERDUE:OVERDUE;CLOSED:CLOSED",
                        dataInit: function (elem) {
                            Msr.JqGridCommon.DataInitBootstrapMultiselect(elem);
                        }
                    },
                    width: 120,
                    align: 'left'
                },
                {
                    label: 'Due Date',
                    name: 'InvoiceDate',
                    index: 'InvoiceDate',
                    formatter: 'date',
                    formatoptions: { newformat: 'm/d/Y' },
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },
                { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: invoiceEditFormatter, width: 130, align: 'center' }
            ],

            viewrecords: true, // show the current page, data rang and total records on the toolbar
            rowNum: 10, rowList: [10, 20, 50, 100],
            loadonce: false, // this is just for the demo
            pager: "#jq-grid-pager",
            height: 'auto',
            gridview: true,
            sortname: 'Id',
            sortable: true,
            sortorder: 'asc',
            cellEdit: true,
            cellsubmit: 'clientArray',
            editurl: 'clientArray',
            autowidth: true,
            colMenu: true,
            gridComplete: function () {
                Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.InvoicesGrid.GetGridId());
            },
            beforeRequest: function () {
                Msr.JqGridCommon.ModifyMultiselectData.call(this);
            }
        });

        Msr.JqGridCommon.BindGridEvents(Msr.InvoicesGrid.GetGridId());

        $("#gs_InvoiceDate").datepicker({
            format: 'm/d/yyyy',
        }).on('changeDate', function () {
            var sgrid = $("#jqGrid")[0];
            sgrid.triggerToolbar();
            $(this).datepicker('hide');
        });

        function invoiceEditFormatter(cellvalue, options, rowObject) {

            var editButton = '<span data-call-id="' + rowObject.Id + '" data-toggle="modal"  data-target="#editInvModal" class="btn btn-xs btn-success"  title="Edit" style="margin:2px;font-size: .8em;"><i class="fa fa-edit" aria-hidden="true"></i></span>';
            var exportButton = '<a href="/Invoices/ExportFile/' + rowObject.Id + '?&po=' + rowObject.CustPo + '"  class="btn btn-xs btn-info" title="Export" style="margin:2px;font-size: .8em;"><i class="fa fa-file-text" aria-hidden="true"></i></a>';

            return editButton + exportButton;
        }
    }
}
