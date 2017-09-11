function LoadProcedureStepGrid(url, returnUrl) {

    $("#jqGrid").jqGrid({
        url: url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            {
                label: 'Title',
                name: 'Title',
                index: 'Title',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 100,
                align: 'center'
            },
            {
                label: ' #',
                name: 'Id',
                index: 'Id',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 30,
                align: 'center'
            },
            {
                label: 'Step Text',
                name: 'StepText',
                index: 'StepText',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 150,
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
                label: 'Approval Status',
                name: 'Status',
                index: 'Status',
                colmenu: false,
                stype: "select",
                searchoptions: { value: ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Creating or Approved;CREATING, DENIED:Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete" },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                align: 'center'
            },
            {
                label: 'Checked Out To',
                name: 'LockedByName',
                index: 'LockedByName',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'center'
            }
            ,
            { name: 'Actions', index: 'ID', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: procedureStepEditFormatter, width: 100, align: 'center' }
        ],
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10,
        rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPager",
        height: 'auto',
        gridview: true,
        sortname: 'StepText',
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
            $('.deleteprepro').on('click',
                function (e) {
                    e.preventDefault();

                    var callBackId = $(this).data('call-back-id');
                    var callBackName = $(this).data('call-back-name');

                    eModal.confirm('Do you really want to delete ' + callBackName + ' ?', 'Confirmation delete')
                        .then(confirmCallback, optionalCancelCallback);

                    function confirmCallback() {
                        window.location.href = "/PreProSearch/PreProDelete/" + callBackId
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
    function procedureStepEditFormatter(cellvalue, options, rowObject) {

        var editButton = '<a  title="Edit" href="/PreProSearch/edit/' + rowObject.ObjectId + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';
        var detailButton = '<a href="/PreProSearch/Details/' + rowObject.ObjectId + '" title="Details" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-eye"></i></a>';
        var deleteButton = '';
        var buttonWorkflowLeft = '';
        var buttonWorkflowRight = '';
        var url = '';

        if (rowObject.Status === 'CREATING') {

            url = '/workflow/submit?objId=' + rowObject.ObjectId + '&returnUrl=' + returnUrl;
            buttonWorkflowLeft = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '"  data-call-back-id="' + rowObject.ObjectId + '" class="btn btn-xs btn-success unlock" title="Cancel Creation. Edit will be lost" style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-left"></i></a>';

            buttonWorkflowRight = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-success" title="Proceed to approval workflow for release." style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-right"></i></a>';
        } else {
            url = '/workflow/delete?objId=' + rowObject.ObjectId + '&returnUrl=' + returnUrl;
            deleteButton = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '"  class="btn btn-xs btn-danger" title="Proceed to delete." style="margin:2px;font-size: .8em;"><i class="fa fa fa-trash"></i></a>';
        }

        return editButton + deleteButton + buttonWorkflowLeft + buttonWorkflowRight + detailButton;

    }
}