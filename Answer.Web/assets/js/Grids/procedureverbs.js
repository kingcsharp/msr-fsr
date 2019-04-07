var Msr = Msr || {};

Msr.ProcedureVerbsGrid = Msr.ProcedureVerbsGrid ||
    {
        GetReturnUrl: function () {
            return "/ProcedureVerbs";
        },
        GetGridId: function () {
            return "jq-grid-procedure-type";
        },
        GetGridEditUrl: function () {
            return "/ProcedureVerbs/Edit/";
        },
        loadProcedureVerbsGrid: function (url) {
            $.jgrid.defaults.styleUI = 'Bootstrap';

            $("#" + Msr.ProcedureVerbsGrid.GetGridId()).jqGrid({
                url: url,
                mtype: "GET",
                styleUI: 'Bootstrap',
                datatype: "local",
                colModel: [
                    {
                        label: '#',
                        name: 'Root',
                        index: 'Root',
                        key: true,
                        colmenu: true,
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
                        label: 'Name',
                        name: 'Name',
                        index: 'Name',
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
                        width: 250,
                        align: 'left'
                    },
                    {
                        label: 'Type',
                        name: 'VerbTypeName',
                        index: 'VerbTypeName',
                        colmenu: false,
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
                        width: 100,
                        hidedlg: false
                    },
                    {
                        label: 'Revision',
                        name: 'Revision',
                        index: 'Revision',
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
                        align: 'left'
                    },
                    {
                        label: 'Approved Status',
                        name: 'Status',
                        index: 'Status',
                        colmenu: false,
                        stype: "select",
                        coloptions: {
                            sorting: false,
                            columns: true,
                            filtering: false,
                            seraching: false,
                            grouping: false,
                            freeze: false
                        },
                        multiselect: true,
                        searchoptions: {
                            sopt: ['eq'],
                            value: "CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING:Creating or Approved;CREATING, DENIED: Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DELETED:Deleted;OLD:Old",
                            dataInit: function (elem) {
                                Msr.JqGridCommon.DataInitBootstrapMultiselect(elem);
                            }
                        },
                        align: 'center'
                    },
                    {
                        name: 'Actions',
                        index: 'ID',
                        key: true,
                        search: false,
                        hidden: false,
                        colmenu: false,
                        editable: false,
                        formatter: prodecureVerbsEditFormatter,
                        width: 100,
                        align: 'center'
                    }
                ],

                viewrecords: true, // show the current page, data rang and total records on the toolbar
                rowNum: 10,
                rowList: [10, 20, 50, 100],
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
                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.ProcedureVerbsGrid.GetGridId());
                    Msr.JqGridCommon.SetupGridLock(Msr.ProcedureVerbsGrid.GetGridEditUrl());
                    Msr.JqGridCommon.UnLockWorkflow(Msr.ProcedureVerbsGrid.GetReturnUrl());
                },
                beforeRequest: function () {
                    Msr.JqGridCommon.ModifyMultiselectData.call(this);
                }
            });

            Msr.JqGridCommon.BindGridEvents(Msr.ProcedureVerbsGrid.GetGridId());

            function prodecureVerbsEditFormatter(cellvalue, options, rowObject) {

                var actions = Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, Msr.ProcedureVerbsGrid.GetReturnUrl(), Msr.ProcedureVerbsGrid.GetGridEditUrl());

                return actions;

            }
        }
    }
