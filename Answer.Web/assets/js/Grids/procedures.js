function LoadProceduresGrid(url) {
    $("#jqGrid").jqGrid({
        url: url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            {
                label: '#',
                name: 'Id',
                index: 'Id',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 100,
                align: 'left'
            },
            {
                label: 'Procedure Name ',
                name: 'Name',
                index: 'Name',
                colmenu: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 250,
                align: 'left'
            },
            {
                label: 'Creator Co',
                name: 'CreatingCoName',
                index: 'CreatingCoName',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left',
                width: 100,
                hidedlg: false
            },
            {
                label: 'Create Dept Name',
                name: 'DeptName',
                index: 'DeptName',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
            	label: 'Procedure Type',
                name: 'VerbName',
                index: 'VerbName',
            	colmenu: false,
            	//stype: "select",
            	coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
            	// searchoptions: { value: ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING:Creating or Approved;CREATING, DENIED: Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete" },
            	align: 'center'
            },
            {
                label: 'Security Level',
                name: 'SecurityLevel',
                index: 'SecurityLevel',
                colmenu: false,
                stype: "select",
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { value: ":[All];1: View What All Users Are Allowed to View;2: View What Managers & Above Are Allowed to View;3: Only Directors & Above Allowed To View;4 :View What VP's & Above Are Allowed to View" },
                align: 'center'
            },
            {
                label: 'Revision',
                name: 'Rev',
                index: 'Rev',
                colmenu: false,
                //stype: "select",
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                // searchoptions: { value: ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING:Creating or Approved;CREATING, DENIED: Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete" },
                align: 'center'
            },
            {
                label: 'Approval Status',
                name: 'Status',
                index: 'Status',
                colmenu: false,
                stype: "select",
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { value: ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING:Creating or Approved;CREATING, DENIED: Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete" },
                align: 'center'
            },

            { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: procedureEditFormatter, width: 100, align: 'center' }
        ],

        viewrecords: true, 
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, 
        pager: "#jqGridPager",
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

            $('.editprocedure').on('click',
                function (e) {
                    e.preventDefault();

                    var callBackId = $(this).data('call-back-id');

                    eModal.confirm('Are you sure? Locking prevents others from editing. Checking out creates the next revision for you to edit?')
                        .then(confirmCallback, optionalCancelCallback);

                    function confirmCallback() {
                        window.location.href = "/Procedures/Edit/" + callBackId;
                    }

                    function optionalCancelCallback() {
                    }

                });

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
    function procedureEditFormatter(cellvalue, options, rowObject) {

        var thisCellVal = '<a href="/Procedures/Edit/' + rowObject.ObjectId + '" data-call-back-id ="' + rowObject.ObjectId +'" class="btn btn-xs btn-success editprocedure" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Edit</a>';

        var viewButton = '<a href="/Procedures/view/' + rowObject.ObjectId + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> View</a>';

        var assignProcedureButton = '<a href="/Procedures/assignProcedure/' + rowObject.ObjectId + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Assign</a>';

        return thisCellVal + viewButton + assignProcedureButton;
    }

    $('#search').click(function () {

        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");

    });
}