var Msr = Msr || {};

Msr.QuotesGrid = Msr.QuotesGrid ||
{
    LoadGrid: function (url, returnUrl) {

    $(document).ready(function () {
        $("#jqGrid").jqGrid({
            url: url,
            mtype: "GET",
            styleUI: 'Bootstrap',
            datatype: "json",
            colModel: [
                {
                    label: 'ObjectId',
                    name: 'ObjectId',
                    index: 'ObjectId',
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    width: 150,
                    hidden: true,
                    align: 'center'
                },
                {
                    label: 'Sumitted Date',
                    name: 'SubmittedDate',
                    index: 'SubmittedDate',
                    colmenu: false,
                    formatter: 'date',
                    formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 150,
                    align: 'center'
                },
                {
                    label: 'Company',
                    name: 'CustomerName',
                    index: 'CustomerName',
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 150,
                    align: 'center'
                },
                {
                    label: 'Division/Fab#',
                    name: 'Division',
                    index: 'Division',
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'center'
                },
                {
                    label: 'Submitted By',
                    name: 'SubmittedBy',
                    index: 'SubmittedBy',
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'center'
                },
                {
                    label: 'Part/Kit No.',
                    name: 'PartKitNo',
                    index: 'PartKitNo',
                    colmenu: true,
                    editable: true, // must set editable to true if you want to make the field editable
                    editrules: { required: true },
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 350,
                    align: 'center'
                },
                {
                    label: 'Procedure Name',
                    name: 'ProcedureName',
                    index: 'ProcedureName',
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'center'
                },
                {
                    label: 'Product Name',
                    name: 'ProductName',
                    index: 'ProductName',
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'center'
                }
                ,
                {
                    label: 'Representative',
                    name: 'Respresentative',
                    index: 'Respresentative',
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'center'
                },
                {
                    label: 'Revision',
                    name: 'Rev',
                    index: 'Rev',
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'center'
                }
                ,
                {
                    label: 'Status',
                    name: 'Status',
                    index: 'Status',
                    colmenu: false,
                    stype: "select",
                    width: '130',
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { value: Msr.JqGridCommon.GetStatusFilters() + ';RECEIVED:Received' },
                    align: 'center',
                    formatter: stausFormatter,
                },
                {
                    name: 'Actions',
                    index: 'ObjectId',
                    key: true,
                    search: false,
                    hidden: false,
                    colmenu: false,
                    editable: false,
                    formatter: actionFormatter,
                    sortable: false,
                    width: 100,
                    align: 'center'
                }
            ],
            viewrecords: true, // show the current page, data rang and total records on the toolbar
            rowNum: 10, rowList: [10, 20, 50, 100],
            loadonce: false, // this is just for the demo
            pager: "#jqGridPager",
            height: 'auto',
            gridview: true,
            sortname: 'SubmittedDate',
            sortable: true,
            sortorder: 'desc',
            cellEdit: false,
            cellsubmit: 'clientArray',
            editurl: 'clientArray',
            autowidth: true,
            colMenu: true,
            key: true,
            ajaxCellOptions: {},
            gridComplete: function () {

                Msr.JqGridCommon.SetupGridLock("/ProductionPlanning/Edit/");
                Msr.JqGridCommon.UnLockWorkflow(returnUrl);
                Msr.JqGridCommon.DocPreview();
            },

        });
        $('#jqGrid').navGrid("#jqGridPager", {
            refresh: true,
            search: false, // show search button on the toolbar
            add: false,
            edit: false,
            del: false,

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


        function stausFormatter(cellvalue, options, rowObject) {

            if (rowObject.IsProduct === false && rowObject.ProductStatus !== 'APPROVED_BUT_REVISING') {
                return 'RECEIVED';
            } else {
                return cellvalue;
            }
        }

        function actionFormatter(cellvalue, options, rowObject) {

            if (rowObject.IsProduct === false) {
                return '<a href="/ProductionPlanning/Edit/' + rowObject.ObjectId + '" class="btn btn-xs btn-primary" title="Start" style="margin:2px;font-size: .8em;"><i class="fa fa-play-circle"> Start</i></a>';
            } else {
                var viewButton = '<a href="/ProductionPlanning/view/' + rowObject.ObjectId + '" class="btn btn-xs btn-success" title="View" style="margin:2px;font-size: .8em;"><i class="fa fa-eye"></i></a>';
                var actions = Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, returnUrl, '/ProductionPlanning/Edit/');

                return actions + viewButton;    
            }
        }


        $('#process-start-btn').click(function (event) {
            event.preventDefault();
            var rowData = $('#custReqsGrid').jqGrid('getRowData', rowId);
            console.log("ROW ID: " + rowId);
            $('#custReqsGrid').jqGrid("setCell", rowid, 'REP_NAME', 'Emily Hart');
        });

        $("#gs_SubmittedDate").datepicker({
            format: 'm/d/yyyy',
        }).on('changeDate', function () {
            var sgrid = $("#jqGrid")[0];
            sgrid.triggerToolbar();
            $(this).datepicker('hide');
        });
    })
    

}}