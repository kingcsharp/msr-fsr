function LoadPurchases(url, returnUrl) {

    $.jgrid.defaults.styleUI = 'Bootstrap';

    $("#jqGrid").jqGrid({
        url: url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        emptyrecords: 'No records to display',
        datatype: "json",
        colModel: [
            {
                label: 'Purchase Number',
                name: 'ObjectId',
                index: 'ObjectId',
                key: true,
                colmenu: false,
                sortable: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 160,
                align: 'left'
            },
            {
                label: 'Cust Ref Number',
                name: 'CustPurchNum',
                index: 'CustPurchNum',
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
                label: 'Order Description',
                name: 'Description',
                index: 'Description',
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
                label: 'Purchase Status',
                name: 'PurchaseStatus',
                index: 'PurchaseStatus',
                colmenu: false,
                stype: "select",
                width: 90,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { value: ":[ALL STATUSES];CLOSED:CLOSED;ALL_FILLED:ALL_FILLED;EXECUTING:EXECUTING;WAITING_FILLS:WAITING_FILLS" },
                align: 'center'
            },
            {
                label: 'CreatedDate',
                name: 'DateCreated',
                index: 'DateCreated',
                colmenu: false,
                sorttype: 'date',
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                formatter: 'date',
                formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                width: 90,
                align: 'center',
            
            },
            {
                label: 'Approval Status',
                name: 'Status',
                index: 'Status',
                colmenu: false,
                stype: "select",
                width: '130',
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { value: ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING :Current ;CREATING, DENIED: Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete" },
                align: 'center'
            },

            { name: 'Actions', index: 'ID', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: ActionFormatter, width: 130, align: 'center' }
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
        pager: "#jqGridPager",
        height: 'auto',
        gridview: true,
        sortname: 'Id',
        sortable: true,
        sortorder: 'asc',
        cellEdit: false,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: true,
        gridComplete: function () {



        }

    });
    $('#jqGrid').navGrid("#jqGridPager", {
            search: false,
            add: false,
            edit: false,
            del: false,
            refresh: true
        },
        {},  // edit options
        {}, // add options
        {}, // delete options
        { multipleSearch: true }
    );

    $('#jqGrid').jqGrid('filterToolbar', {
        stringResult: true,
        searchOnEnter: true,
        searchOperators: true
    });


    $("#gs_DateCreated").datepicker({
        format: 'm/d/yyyy',
    }).on('changeDate', function () {
        var sgrid = $("#jqGrid")[0];
        sgrid.triggerToolbar();
        $(this).datepicker('hide');
    });


    $('#search').click(function () {

        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");

    });


    function ActionFormatter(cellvalue, options, rowObject) {

        var showPoButton = '<a  title="View the Items On this Purchase" href="/PurchaseOrder/CreatePurchase/' + rowObject.ObjectId + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-eye"></i></a>';

        return showPoButton;
    }
}


