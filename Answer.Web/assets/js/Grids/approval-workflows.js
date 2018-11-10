var Msr = Msr || {};

Msr.ApprovalWorkflowsGrid = Msr.ApprovalWorkflowsGrid || {

    GetReturnUrl: function () {
        return "/ApprovalWorkflows";
    },
    GetGridId: function () {
        return "jq-grid-approval-workflow";
    },
    GetGridEditUrl: function () {
        return "/ApprovalWorkflows/Edit/";
    },
    LoadApprovalWorkflows: function (url) {

        $("#" + Msr.ApprovalWorkflowsGrid.GetGridId()).jqGrid({
            url: url,
            mtype: "GET",
            styleUI: 'Bootstrap',
            datatype: "local",
            colModel: [
                {
                    label: 'Approval Workflow Name',
                    name: 'Name',
                    index: 'Name',
                    key: true,
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 100,
                    align: 'center'
                },
                {
                    label: 'Approval Stamp Name',
                    name: 'Stamp_Name',
                    index: 'Stamp_Name',
                    colmenu: true,
                    editable: true, // must set editable to true if you want to make the field editable
                    editrules: { required: true },
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 200,
                    align: 'center'
                },
                { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: wfEditFormatter, width: 200, align: 'center' }
            ],

            viewrecords: true, // show the current page, data rang and total records on the toolbar
            rowNum: 10, rowList: [10, 20, 50, 100],
            loadonce: false, // this is just for the demo
            pager: "#jq-grid-pager",
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
                Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.ApprovalWorkflowsGrid.GetGridId());
            }
        });

        Msr.JqGridCommon.BindGridEvents(Msr.ApprovalWorkflowsGrid.GetGridId());

        function wfEditFormatter(cellvalue, options, rowObject) {
            var thisCellVal = '<a href="/ApprovalWorkflows/edit/' + rowObject.Id + '" title="Edit" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Edit</a>';
            thisCellVal = thisCellVal + '<a href="/ApprovalWorkflows/Hide/' + rowObject.Id + '" title="Hide Workflow" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Hide</a>';
            return thisCellVal;
        }
    }
}


