var Msr = Msr || {};

Msr.ApprovalGroupsGrid = Msr.ApprovalGroupsGrid || {
    GetReturnUrl: function () {
        return "/ApprovalGroups";
    },
    GetGridId: function () {
        return "jq-grid-approval-groups";
    },
    GetGridEditUrl: function () {
        return "/ApprovalGroups/Edit/";
    },
    LoadApprovalGroups: function (url) {

        $.jgrid.defaults.styleUI = 'Bootstrap';

        $("#" + Msr.ApprovalGroupsGrid.GetGridId()).jqGrid({
            url: url,
            mtype: "GET",
            styleUI: 'Bootstrap',
            emptyrecords: 'No records to display',
            datatype: "local",
            colModel: [
                {
                    label: 'Id',
                    name: 'Id',
                    index: 'Id',
                    key: true,
                    colmenu: true,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 50,
                    align: 'left'
                },
                {
                    label: 'Approval Group Name',
                    name: 'Name',
                    index: 'Name',
                    colmenu: false,
                    editable: true, // must set editable to true if you want to make the field editable
                    editrules: { required: true },
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 200,
                    align: 'left'
                },
                { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: approvalGroupsEditFormatter, width: 100, align: 'center' }
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
            pager: "#jq-grid-pager",
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
                Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.ApprovalGroupsGrid.GetGridId());
            }
        });

        Msr.JqGridCommon.BindGridEvents(Msr.ApprovalGroupsGrid.GetGridId());

        function approvalGroupsEditFormatter(cellvalue, options, rowObject) {
            var thisCellVal = '<a href="/ApprovalGroups/Edit/' + rowObject.Id + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Edit</a>';
            thisCellVal = thisCellVal + '<a href="/ApprovalGroups/Hide/' + rowObject.Id + '" title="Hide Workflow" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-eye-slash"></i> Hide</a>';
            return thisCellVal;
        }
    }
}