function LoadGrid(url, returnUrl) {

    $(document).ready(function () {
        $("#jqGrid").jqGrid({
            url: url,
            mtype: "GET",
            styleUI: 'Bootstrap',
            datatype: "json",
            colModel: [
                {
                    label: 'PObjectId',
                    name: 'PObjectId',
                    index: 'PObjectId',
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
                    name: 'ProductStatus',
                    index: 'ProductStatus',
                    colmenu: false,
                    stype: "select",
                    width: '130',
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { value: Msr.JqGridCommon.GetStatusFilters() },
                    align: 'center'
                },
                { name: 'Actions', index: 'PObjectId', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: actionFormatter, sortable: false, width: 100, align: 'center' }

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
                Msr.JqGridCommon.UnLockWorkflow(returnUrl);
                $('.editp').on('click',
                    function (e) {
                        e.preventDefault();

                        var callBackId = $(this).data('call-back-id');
                        var callBackName = $(this).data('call-back-name');

                        eModal.confirm(
                            'Are You Sure? Locking prevents others from editing. Checking out create the next revision for you to edit?', 'Confirmation Edit')
                            .then(confirmCallback, optionalCancelCallback);

                        function confirmCallback() {
                            $.ajax({
                                type: "GET",
                                url: '/WorkFlow/CheckOutObject/' + callBackId,
                                dataType: 'JSON',
                                cache: false,
                                success: function (data) {
                                    window.location.href = '/ProductionPlanning/edit/' + data.ObjectId;
                                },
                                error: function (error) {
                                    alert(error);
                                }
                            });

                        }

                        function optionalCancelCallback() {
                        }

                    });
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
            var viewButton = '';

            if (rowObject.Status === 'Received') {
                var startButton = '<a href="/ProductionPlanning/start/' + rowObject.CustomerSubmitId + '" class="btn btn-xs btn-primary" title="View" style="margin:2px;font-size: .8em;"><i class="fa fa-play-circle"> Start</i></a>';
            }

            if ((rowObject.ProductStatus !== 'APPROVED_BUT_REVISING' && rowObject.ProductStatus !== 'APPROVED') && rowObject.Status !== 'Received') {
                if (rowObject.ProductId == null) {
                    editButton = '<a  title="Edit" href="/ProductionPlanning/edit/' + rowObject.CustomerSubmitId + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';
                } else {
                    editButton = '<a  title="Edit" href="/ProductionPlanning/edit/' + rowObject.PObjectId + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';
                }
            }

            if (rowObject.ProductStatus === 'APPROVED') {
                viewButton = '<a  title="View" href="/ProductionPlanning/view/' + rowObject.PObjectId + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-eye"></i></a>';
                editButton = '<a  title="Edit" data-call-back-id ="' + rowObject.PObjectId + '" data-call-back-name ="ProductSubmit" href="/ProductionPlanning/edit/' + rowObject.PObjectId + '" class="btn btn-xs btn-success editp" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';
            }

            var deleteButton = '';
            var buttonWorkflowLeft = '';
            var buttonWorkflowRight = '';
            var url = '';

            if (rowObject.ProductStatus === 'CREATING') {

                url = '/workflow/submit?objId=' + rowObject.PObjectId + '&returnUrl=' + returnUrl;
                buttonWorkflowLeft = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '"  data-call-back-id="' + rowObject.PObjectId + '" class="btn btn-xs btn-danger unlock" title="Cancel Creation. Edit will be lost" style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-left"></i></a>';

                buttonWorkflowRight = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-success" title="Proceed to approval workflow for release." style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-right"></i></a>';
            } else {
                url = '/workflow/delete?objId=' + rowObject.PObjectId + '&returnUrl=' + returnUrl;
                deleteButton = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-danger" title="Proceed to Delete." style="margin:2px;font-size: .8em;"><i class="fa fa fa-trash-o"></i></a>';
            }

            return startButton + editButton + buttonWorkflowLeft + buttonWorkflowRight + viewButton;
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