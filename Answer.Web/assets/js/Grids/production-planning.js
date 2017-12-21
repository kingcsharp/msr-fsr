function LoadGrid(url, returnUrl) {

    $(document).ready(function () {
        $("#jqGrid").jqGrid({
            url: url,
            mtype: "GET",
            styleUI: 'Bootstrap',
            datatype: "json",
            colModel: [
                {
                    label: 'Id',
                    name: 'Id',
                    index: 'Id',
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
                    name: 'Company',
                    index: 'Company',
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
                    label: 'Description',
                    name: 'Description',
                    index: 'Description',
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
                    label: 'Status',
                    name: 'ProductStatus',
                    index: 'ProductStatus',
                    colmenu: false,
                    stype: "select",
                    width: '130',
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { value: ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING:Creating or Approved;CREATING, DENIED: Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete" },
                    align: 'center'
                },
                { name: 'Actions', index: 'ID', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: actionFormatter, sortable: false, width: 100, align: 'center' }

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


        function actionFormatter(cellvalue, options, rowObject) {

            var startButton = '';
            var editButton = '';
            if (rowObject.Status === 'Received') {
                var startButton = '<a href="/ProductionPlanning/start/' + rowObject.Id + '" class="btn btn-xs btn-primary" title="View" style="margin:2px;font-size: .8em;"><i class="fa fa-play-circle"> Start</i></a>';
            } else if (rowObject.Status === 'In Progress') {
                editButton = '<a  title="Edit" href="/ProductionPlanning/Edit/' + rowObject.Id + '" class="btn btn-xs btn-warning" style="margin:2px;font-size: .8em;"><i class="fa fa-pencil"> Edit</i></a>';
            }

            var editButton = '';
            if ((rowObject.ProductStatus !== 'APPROVED_BUT_REVISING' && rowObject.ProductStatus !== 'APPROVED') && rowObject.Status !== 'Received') {
                var editButton = '<a  title="Edit" href="/ProductionPlanning/edit/' + rowObject.Id + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';
            }

            if (rowObject.ProductStatus === 'APPROVED') {
                var editButton = '<a  title="Edit" href="/ProductionPlanning/view/' + rowObject.Id + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-eye"></i></a>';
            }

            var deleteButton = '';
            var buttonWorkflowLeft = '';
            var buttonWorkflowRight = '';
            var url = '';

            if (rowObject.ProductStatus === 'CREATING') {

                url = '/workflow/submit?objId=' + rowObject.ProductId + '&returnUrl=' + returnUrl;
                buttonWorkflowLeft = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '"  data-call-back-id="' + rowObject.ProductId + '" class="btn btn-xs btn-danger unlock" title="Cancel Creation. Edit will be lost" style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-left"></i></a>';

                buttonWorkflowRight = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-success" title="Proceed to approval workflow for release." style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-right"></i></a>';
            } else {
                url = '/workflow/delete?objId=' + rowObject.ProductId + '&returnUrl=' + returnUrl;
                deleteButton = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-danger" title="Proceed to Delete." style="margin:2px;font-size: .8em;"><i class="fa fa fa-trash-o"></i></a>';
            }

            return startButton + editButton + buttonWorkflowLeft + buttonWorkflowRight;
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
    function received(id, status) {
        $.ajax({
            type: "POST",
            url: "/ProductionPlanning/Status?id=" + id + "&currentStatus=" + status,
            dataType: 'html',
            success: function (data) {
                location.reload();
            },
            error: function () {
            }
        });
    }

}