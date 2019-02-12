var Msr = Msr || {};

Msr.PurchaseOrderGrid = Msr.PurchaseOrderGrid ||
{
    GetReturnUrl: function () {
        return "/PurchaseOrder";
    },
    GetGridId: function () {
        return "jq-grid-purchase";
    },
    LoadPurchaseOrderGrid: function(url) {

        $("#" + Msr.PurchaseOrderGrid.GetGridId()).jqGrid({
            url: url,
            mtype: "GET",
            styleUI: 'Bootstrap',
            emptyrecords: 'No records to display',
            datatype: "local",
            colModel: [
                {
                    label: 'Id',
                    name: 'Root',
                    index: 'Root',
                    key: true,
                    colmenu: true,
                    sortable: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 100,
                    align: 'left'
                },
                {
                    label: 'Name',
                    name: 'Name',
                    index: 'Name',
                    key: true,
                    colmenu: false,
                    sortable: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 160,
                    align: 'left'
                },
                {
                    label: 'PO#',
                    name: 'ReferencePo',
                    index: 'ReferencePo',
                    colmenu: false,
                    editable: true,
                    width: 110,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },
                {
                    label: 'Reference',
                    name: 'ReferenceName',
                    index: 'ReferenceName',
                    colmenu: false,
                    editable: true,
                    width: 110,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },
                {
                    label: 'Invoiced',
                    name: 'InvoicedBalance',
                    index: 'InvoicedBalance',
                    colmenu: false,
                    editable: true,
                    width: 90,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    formatter: 'currency',
                    formatoptions: { prefix: "$" },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },
                {
                    label: 'Not Invoiced',
                    name: 'UninvoicedBalance',
                    index: 'UninvoicedBalance',
                    colmenu: false,
                    width: 130,
                    editable: true,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    formatter: 'currency',
                    formatoptions: { prefix: "$" },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },

                {
                    label: 'Balance',
                    name: 'Balance',
                    index: 'Balance',
                    colmenu: false,
                    width: 120,
                    editable: true,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    formatter: 'currency',
                    formatoptions: { prefix: "$" },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },
                {
                    label: 'Facility/Supplier',
                    name: 'SupplierName',
                    index: 'SupplierName',
                    colmenu: false,
                    editable: true,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },
                {
                    label: 'Cust/Co/Dept',
                    name: 'CustomerCo',
                    index: 'CustomerCo',
                    colmenu: false,
                    editable: false,
                    editrules: { required: true },
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: true,
                        grouping: false,
                        freeze: false
                    },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left',
                    hidedlg: false
                },
                {
                    label: 'Open Date',
                    name: 'OpenDate',
                    index: 'OpenDate',
                    colmenu: false,
                    editable: true,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    formatter: 'date',
                    formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left',
                    width: 120
                },

                {
                    label: 'Close Date',
                    name: 'CloseDate',
                    index: 'CloseDate',
                    colmenu: false,
                    editable: true,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    formatter: 'date',
                    formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left',
                    width: 120
                },


                {
                    label: 'Total Purchase Limit',
                    name: 'TotalPurchaseLimit',
                    index: 'TotalPurchaseLimit',
                    colmenu: false,
                    width: 140,
                    editable: true,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    formatter: moneyFormatter,
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },
                {
                    label: 'Unused Amount',
                    name: 'UnusedAmount',
                    index: 'UnusedAmount',
                    colmenu: false,
                    width: 140,
                    editable: true,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    formatter: moneyFormatter,
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },
                {
                    label: 'Type',
                    name: 'AccType',
                    index: 'AccType',
                    colmenu: false,
                    width: 220,
                    editable: true,
                    stype: "select",
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    searchoptions: { value: ":[All];CUSTOMER_ACCOUNT:Customer Account;PRODUCT_ACCOUNT:Product Account;PURCHASING_ACCOUNT:Purchasing Account" },
                    align: 'left'
                },
                {
                    label: 'Close/Cut Trigger',
                    name: 'InvoiceTrigger',
                    index: 'InvoiceTrigger',
                    width: 140,
                    colmenu: false,
                    editable: true,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },

                {
                    label: 'Revision',
                    name: 'Rev',
                    index: 'Rev',
                    colmenu: false,
                    editable: true,
                    width: 110,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'center'
                },
                {
                    label: 'Status',
                    name: 'Status',
                    index: 'Status',
                    colmenu: false,
                    stype: "select",
                    width: '130',
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { value: Msr.JqGridCommon.GetStatusFilters() },
                    align: 'center'
                },
                { name: 'Actions', index: 'ID', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: ActionFormatter, width: 100, align: 'center' }
            ],
            ajaxRowOptions: {
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            },
            serializeRowData: function (postdata) {
                return JSON.stringify(postdata);
            },
            viewrecords: true,
            rowNum: 10, rowList: [10, 20, 50, 100],
            loadonce: false,
            pager: "#jq-grid-pager",
            height: 'auto',
            gridview: true,
            sortname: 'Id',
            sortable: true,
            sortorder: 'Desc',
            cellEdit: false,
            cellsubmit: 'clientArray',
            editurl: 'clientArray',
            autowidth: true,
            colMenu: true,
            gridComplete: function () {
                Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.PurchaseOrderGrid.GetGridId());
                Msr.JqGridCommon.SetupGridLock("/PurchaseOrder/Edit/");
                Msr.JqGridCommon.UnLockWorkflow(Msr.PurchaseOrderGrid.GetReturnUrl());
                $('.close-account').on('click',
                    function (e) {
                        e.preventDefault();

                        var callBackId = $(this).data('call-back-id');

                        eModal.confirm('Are you sure you want to close this account?')
                            .then(confirmCallback, optionalCancelCallback);

                        function confirmCallback() {
                            window.location.href =
                                "/PurchaseOrder/CloseAccount/" + callBackId;
                        }

                        function optionalCancelCallback() {

                        }
                    });
            }

        });

        Msr.JqGridCommon.BindGridEvents(Msr.PurchaseOrderGrid.GetGridId());
        
        $("#gs_CloseDate").datepicker({
            format: 'm/d/yyyy',
        }).on('changeDate', function () {
            var sgrid = $("#jqGrid")[0];
            sgrid.triggerToolbar();
            $(this).datepicker('hide');
        });



        $("#gs_OpenDate").datepicker({
            format: 'm/d/yyyy',
        }).on('changeDate', function () {
            var sgrid = $("#" + Msr.PurchaseOrderGrid.GetGridId())[0];
            sgrid.triggerToolbar();
            $(this).datepicker('hide');
        });

        $('#search').click(function () {

            $("#" + Msr.PurchaseOrderGrid.GetGridId()).setGridParam({
                page: 1
            }).trigger("reloadGrid");

        });

        function moneyFormatter(cellvalue, options, rowObject) {

            if (cellvalue !== null) {
                return '$ ' + cellvalue + '';
            }

            return '';
        };

        function ActionFormatter(cellvalue, options, rowObject) {

            var closeButton = '';
            var showPoButton = '';
            var actions = Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, Msr.PurchaseOrderGrid.GetReturnUrl(), '/PurchaseOrder/Edit/', true);

            if (rowObject.Product !== null) {
                showPoButton = '<a  title="Purchase On this PO" href="/PurchaseOrder/PurchasePoDetails/' + rowObject.Root + '" data-call-back-id ="' + rowObject.Root + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-dollar"></i></a>';
            }
            var d = new Date();
            var strDate = (d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear();

            if (Date.parse(rowObject.CloseDate) > Date.parse(strDate) && rowObject.Status === "APPROVED" && rowObject.SupplierCo === rootCompany) {
                closeButton = '<a  title="Close this Account" href="#" data-call-back-id ="' + rowObject.ObjectId + '" class="btn btn-xs btn-danger close-account" style="margin:2px;font-size: .8em;"><i class="fa fa-close" aria-hidden="true"></i></a>';
            }

            return showPoButton + actions + closeButton;
        }
    }
}