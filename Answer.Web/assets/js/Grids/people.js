function LoadPeopleGrid(url, returnUrl) {

    $.jgrid.defaults.styleUI = 'Bootstrap';

    $("#jqGrid").jqGrid({
        url: url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        emptyrecords: 'No records to display',
        datatype: "json",
        colModel: [
            {
                label: 'Id',
                name: 'Id',
                index: 'Id',
                key: true,
                colmenu: false,
                coloptions: {
                    sorting: false,
                    columns: true,
                    filtering: false,
                    seraching: false,
                    grouping: false,
                    freeze: false
                },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 100,
                align: 'left'
            },
            {
                label: 'First Name',
                name: 'FirstName',
                index: 'FirstName',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: {
                    sorting: false,
                    columns: true,
                    filtering: false,
                    seraching: false,
                    grouping: false,
                    freeze: false
                },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 200,
                align: 'left'
            },
            {
                label: 'Last Name',
                name: 'LastName',
                index: 'LastName',
                colmenu: false,
                editable: true,
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
                width: 150,
                hidedlg: false
            },
            {
                label: 'User Name',
                name: 'LoginId',
                index: 'LoginId',
                colmenu: false,
                editable: true,
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
                width: 150,
                hidedlg: false
            },
            {
                label: 'Picture',
                name: 'PicRecord',
                index: 'PicRecord',
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
                label: 'Position',
                name: 'PositionName',
                index: 'PositionName',
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
                label: 'Supervisor',
                name: 'BossName',
                index: 'BossName',
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
                label: 'Main Company Root',
                name: 'RootCoName',
                index: 'RootCoName',
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
                label: 'Company Department',
                name: 'CompanyName',
                index: 'CompanyName',
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
                label: 'Primary Work Phone',
                name: 'PrimaryPhoneNumber',
                index: 'PrimaryPhoneNumber',
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
                label: 'Work Email',
                name: 'WorkEmailAddress',
                index: 'WorkEmailAddress',
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
                label: 'Hire Date',
                name: 'DateHired',
                index: 'DateHired',
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
                label: 'System Status',
                name: 'SystemStatus',
                index: 'SystemStatus',
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
                coloptions: {
                    sorting: false,
                    columns: true,
                    filtering: false,
                    seraching: false,
                    grouping: false,
                    freeze: false
                },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left',
                width: 100
            },
            {
                label: 'Approval Status',
                name: 'Status',
                index: 'Status',
                colmenu: false,
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
                searchoptions: {
                    value:
                        ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING:Creating or Approved;CREATING, DENIED: Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete"
                },
                align: 'left'
            },
            {
                label: 'Checked Out To',
                name: 'LockedByName',
                index: 'LockedByName',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: {
                    sorting: false,
                    columns: true,
                    filtering: false,
                    seraching: false,
                    grouping: false,
                    freeze: false
                },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 200,
                align: 'left'
            },
            {
                label: 'Reference Files',
                name: 'ReferenceFiles',
                index: 'ReferenceFiles',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                formatter: FilePreviewFormatter,
                align: 'center'
            },
            {
                name: 'Actions',
                index: 'ObjectId',
                key: true,
                search: false,
                hidden: false,
                colmenu: false,
                editable: false,
                formatter: peopleEditFormatter,
                width: 200,
                align: 'center'
            }
        ],
        ajaxRowOptions: {
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        },
        serializeRowData: function (postdata) {
            return JSON.stringify(postdata);
        },
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
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
            $('.editpeople').on('click',
                function (e) {
                    e.preventDefault();

                    var callBackId = $(this).data('call-back-id');
                    var callBackName = $(this).data('call-back-name');

                    eModal.confirm(
                            'Are You Sure? Locking prevents others from editing. Checking out create the next revision for you to edit?', 'Confirmation Edit')
                        .then(confirmCallback, optionalCancelCallback);

                    function confirmCallback() {
                        console.log("ok");
                        window.location.href = "/People/Edit/" + callBackId;
                    }

                    function optionalCancelCallback() {
                    }

                });

            UnLockWorkflow(returnUrl);
        }

    });
    $('#jqGrid').navGrid("#jqGridPager", {
            search: false, // show search button on the toolbar
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


    function FilePreviewFormatter(cellvalue, options, rowObject) {
        return Msr.JqGridCommon.FilePreview(cellvalue, options, rowObject);
    }

    function peopleEditFormatter(cellvalue, options, rowObject) {

        var editButton = '<a href="/People/Edit/' + rowObject.ObjectId + '" data-call-back-id ="' + rowObject.ObjectId + '"  class="btn btn-xs btn-success editpeople" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';

        var deleteButton = '';
        var buttonWorkflowLeft = '';
        var buttonWorkflowRight = '';
        var url = '';
        var subordinate = '';

        if (rowObject.Status === 'CREATING') {

            url = '/workflow/submit?objId=' + rowObject.ObjectId + '&returnUrl=' + returnUrl;
            buttonWorkflowLeft = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '"  data-call-back-id="' + rowObject.ObjectId + '" class="btn btn-xs btn-success unlock" title="Cancel Creation. Edit will be lost" style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-left"></i></a>';

            buttonWorkflowRight = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-success" title="Proceed to approval workflow for release." style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-right"></i></a>';
        } else {
            url = '/workflow/delete?objId=' + rowObject.ObjectId + '&returnUrl=' + returnUrl;
            deleteButton = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-danger" title="Proceed to delete." style="margin:2px;font-size: .8em;"><i class="fa fa fa-trash-o"></i></a>';
        }
        if (rowObject.Status == 'APPROVED') {
            subordinate = '<a  title="SubOrdinate" href="/People/Add/' + rowObject.ObjectId + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-list-alt"></i></a>';
        }

        if (rowObject.Status == 'APPROVED_BUT_REVISING') {
            editButton = '';
            deleteButton = '';
        }

        return editButton + deleteButton + buttonWorkflowLeft + buttonWorkflowRight + subordinate;

    }

    $('#search').click(function () {

        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");

    });
}

