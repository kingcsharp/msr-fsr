var Msr = Msr || {};

Msr.ProcedureStepGrid = Msr.ProcedureStepGrid ||
    {
        GetReturnUrl: function () {
            return "/PreProSearch";
        },
        GetGridId: function () {
            return "jq-grid-template";
        },
        GetGridEditUrl: function () {
            return "/PreProSearch/Edit/";
        },
        LoadProcedureStepGrid: function (url) {

            $("#" + Msr.ProcedureStepGrid.GetGridId()).jqGrid({
                url: url,
                mtype: "GET",
                styleUI: 'Bootstrap',
                datatype: "local",
                colModel: [
                    {
                        label: ' #',
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
                        width: 30,
                        align: 'center'
                    },
                    {
                        label: 'Title',
                        name: 'Title',
                        index: 'Title',
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
                        align: 'center'
                    },
                    {
                        label: 'Step Text',
                        name: 'StepText',
                        index: 'StepText',
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
                        width: 150,
                        align: 'center'
                    },
                    {
                        label: 'Revision',
                        name: 'Rev',
                        index: 'Rev',
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
                        align: 'center'
                    },
                    {
                        label: 'Approval Status',
                        name: 'Status',
                        index: 'Status',
                        colmenu: false,
                        stype: "select",
                        multiselect: true,
                        searchoptions: {
                            sopt: ['eq'],
                            value: "CREATING: Creating;IN_WORKFLOW:In Approval Workflow;APPROVED:Approved;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Old",
                            dataInit: function (elem) {
                                Msr.JqGridCommon.DataInitBootstrapMultiselect(elem);
                            }
                        },
                        coloptions: {
                            sorting: false,
                            columns: true,
                            filtering: false,
                            seraching: false,
                            grouping: false,
                            freeze: false
                        },
                        align: 'center'
                    },
                    {
                        label: 'Reference Files',
                        name: 'ReferenceFiles',
                        index: 'ReferenceFiles',
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
                        formatter: filePreviewFormatter,
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
                        formatter: procedureStepEditFormatter,
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
                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.ProcedureStepGrid.GetGridId());
                    Msr.JqGridCommon.SetupGridLock(Msr.ProcedureStepGrid.GetGridEditUrl());
                    Msr.JqGridCommon.UnLockWorkflow(Msr.ProcedureStepGrid.GetReturnUrl());
                    Msr.JqGridCommon.DocPreview();
                },
                beforeRequest: function () {
                    Msr.JqGridCommon.ModifyMultiselectData.call(this);
                }
            });

            Msr.JqGridCommon.BindGridEvents(Msr.ProcedureStepGrid.GetGridId());

            function filePreviewFormatter(cellvalue, options, rowObject) {
                return Msr.JqGridCommon.FilePreview(cellvalue, options, rowObject);
            }

            function procedureStepEditFormatter(cellvalue, options, rowObject) {

                var actions = Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, Msr.ProcedureStepGrid.GetReturnUrl(), Msr.ProcedureStepGrid.GetGridEditUrl());

                return actions;

            }
        }
    }
