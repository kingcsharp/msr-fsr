function InvoicesGrid(url) {

    $("#jqGrid").jqGrid({
        url: url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            {
                label: 'Wo Item',
                name: 'InvoiceId',
                index: 'InvoiceId',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 100,
                align: 'left'
            },
            {
                label: "Status",
                name: 'Status',
                index: 'Status',
                colmenu: false,
                stype: "select",
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { value: ":[All];CREATING:Creating;CLOSED:Closed;INVOICED:Invoiced;SENT_TO_CUSTOMER:Sent to Cust;PAID_IN_FULL:Paid in Full" },
               
                width: 120,
                align: 'left'
            },
            {
                label: "Supplier",
                name: 'SupplierName',
                index: 'SupplierName',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left',
                hidedlg: false
            },
            {
                label: 'Serial',
                name: 'CustPurchNum',
                index: 'CustPurchNum',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Po',
                name: 'ReferencePo',
                index: 'ReferencePo',
                colmenu: false,
                width: 160,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Qty',
                name: '',
                index: '',
                colmenu: false,
                width: 100,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },

            {
                label: "Product Name",
                name: 'AcctName',
                index: 'AcctName',
                colmenu: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 160,
                align: 'left'
            },
         
            {
                label: 'Invoice',
                name: 'AmtInvoiced',
                index: 'AmtInvoiced',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Status',
                name: 'UninvoicedBalance',
                index: 'UninvoicedBalance',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'center'
            },

            {
                label: 'Price',
                name: 'NewItemsAmt',
                index: 'NewItemsAmt',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                formatter: 'currency',
                formatoptions: { prefix: "$" },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },

            {
                label: 'Amount',
                name: 'TotalDue',
                index: 'TotalDue',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                formatter: 'currency',
                formatoptions: { prefix: "$" },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },

            {
                label: 'Date',
                name: 'InvoiceDate',
                index: 'InvoiceDate',
                formatter: 'date',
                formatoptions: { newformat: 'm/d/Y' },
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            { name: 'Actions', index: 'InvoiceId', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: InvoiceEditFormatter, width: 130, align: 'center' }
        ],

        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPager",
        height: 'auto',
        gridview: true,
        sortname: 'SupplierName',
        sortable: true,
        sortorder: 'asc',
        cellEdit: true,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: true,
        gridComplete: function () {

        }
    });
    $('#jqGrid').navGrid("#jqGridPager", {
        search: false, // show search button on the toolbar
        add: false,
        edit: false,
        del: false,
        refresh: true
    },
        {}, // edit options
        {}, // add options
        {}, // delete options
        { multipleSearch: true }
    );
    $('#jqGrid').jqGrid('filterToolbar', {
        stringResult: true,
        searchOnEnter: true,
        searchOperators: true
    });

    $("#gs_InvoiceDate").datepicker({
        format: 'm/d/yyyy',
    }).on('changeDate', function () {
        var sgrid = $("#jqGrid")[0];
        sgrid.triggerToolbar();
        $(this).datepicker('hide');
    });

    function InvoiceEditFormatter(cellvalue, options, rowObject) {

        var detailtButton = '<a  title="Detail" href="/Invoices/Detail/' + rowObject.InvoiceId + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-eye"></i></a>';

        return detailtButton
    }

    $('#search').click(function () {

        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");

    });
  

}
