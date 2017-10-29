$(document).ready(function () {
    $("#jqGrid").jqGrid({
        url: '/ProductionPlanning/ProductionPlanningData',
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
                name: 'DivisionFab',
                index: 'DivisionFab',
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
                name: 'ShortDescription',
                index: 'ShortDescription',
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
                name: 'Status',
                index: 'Status',
                colmenu: false,
                stype: "select",
                width: '130',
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { value: ":[All];IN_PROGRESS: In Progress;RECEIVED: Received;IN_APPROVAL: In Approval;CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING:Creating or Approved;CREATING, DENIED: Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete" },
                align: 'center'
            },
            { name: 'Actions', index: 'ID', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: productionEditFormatter, sortable: false, width: 100, align: 'center' }
           
        ],
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPager",
        height: 'auto',
        gridview: true,
        sortname: 'Company',
        sortable: true,
        sortorder: 'asc',
        cellEdit: false,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: true,
        key: true,
        ajaxCellOptions: {},
        gridComplete: function () {
            $('.deletepart').on('click', function (e) {
                e.preventDefault();

                var callBackId = $(this).data('call-back-id');
                var callBackName = $(this).data('call-back-name');

                eModal.confirm('Do you really want to delete ' + callBackName + ' ?', 'Confirmation delete')
                    .then(confirmCallback, optionalCancelCallback);

                function confirmCallback() {
                    console.log("ok")
                    window.location.href = "/Locations/LocationDelete/" + callBackId
                }
                function optionalCancelCallback() {
                    console.log("cancel")
                }

            })
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

    function productionEditFormatter(cellvalue, options, rowObject) {
        switch (rowObject.Status) {
            case 'IN_APPROVAL':
               actionButtons = '<a  class="btn btn-xs btn-danger" data-toggle="tooltip" data-placement="left" title="Cancel Workflow"><i class="fa fa-arrow-left"></i></a><a  href="/ProductionPlanning/EditStatus?id=' + rowObject.Id + '&currentStatus=IN_APPROVAL" class="btn btn-xs btn-success" data-toggle="tooltip" data-placement="left" title="Start Approval Workflow" style="margin-left:2px;"><i class="fa fa-arrow-right"></i></a>';
                break;
            case 'IN_PROGRESS':
                actionButtons = '<a  title="Edit" href="/ProductionPlanning/Edit/' + rowObject.Id + '" class="btn btn-xs btn-warning" style="margin:2px;font-size: .8em;"><i class="fa fa-pencil"> Edit</i></a>';
                break;
            default:
                actionButtons = '<a id="process-start-btn" type="submit" class="btn btn-xs btn-primary" data-toggle="tooltip" data-placement="left" title="Start Process Definition" onclick="received(&#39;' + rowObject.Id + '&#39;,&#39;IN_PROGRESS&#39;)"><i class="fa fa-play-circle"></i> Start</a>';
                   
        }
        thisCellVal = actionButtons;
        return thisCellVal;
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

