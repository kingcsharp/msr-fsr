var Msr = Msr || {};

Msr.ProceduresGrid = Msr.ProceduresGrid ||
    {
        GetReturnUrl: function () {
            return "/Procedures";
        },
        GetGridId: function () {
            return "jq-grid-procedure";
        },
        GetGridEditUrl: function () {
            return "/Procedures/Edit/";
        },
        LoadProceduresGrid: function (url) {

            $("#" + Msr.ProceduresGrid.GetGridId()).jqGrid({
                url: url,
                mtype: "GET",
                styleUI: 'Bootstrap',
                datatype: "local",
                colModel: [
                    {
                        label: 'Root',
                        name: 'Root',
                        index: 'Root',
                        key: true,
                        colmenu: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 100,
                        align: 'left'
                    },
                    {
                        label: 'Procedure Name ',
                        name: 'Name',
                        index: 'Name',
                        colmenu: false,
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
                        align: 'center'
                    },
                    {
                        label: 'Security Level',
                        name: 'SecurityLevel',
                        index: 'SecurityLevel',
                        colmenu: false,
                        stype: "select",
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        multiselect: true,
                        searchoptions: {
                            sopt: ['eq'],
                            attr: { multiple: 'multiple', size: 4 },
                            value: ":[All];1: View What All Users Are Allowed to View;2: View What Managers & Above Are Allowed to View;3: Only Directors & Above Allowed To View;4 :View What VP's & Above Are Allowed to View",
                            dataInit: function (elem) {
                                Msr.JqGridCommon.DataInitBootstrapMultiselect(elem,
                                    { includeSelectAllOption: false }, function (elem) {
                                        $(elem).multiselect('select', "[All]");
                                        var elemInput = $(elem).parent().parent().find('input[value=""]');
                                        elemInput.prop('checked', true);
                                        elemInput.parent().parent().parent().addClass('active');
                                    });
                            }
                        },
                        align: 'center'
                    },
                    {
                        label: 'Revision',
                        name: 'Rev',
                        index: 'Rev',
                        colmenu: false,
                        //stype: "select",
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        align: 'center'
                    },
                    {
                        label: 'Approval Status',
                        name: 'Status',
                        index: 'Status',
                        colmenu: false,
                        stype: "select",
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        multiselect: true,
                        searchoptions: {
                            sopt: ['eq'],
                            value: ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING:Creating or Approved;CREATING, DENIED: Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete",
                            dataInit: function (elem) {
                                Msr.JqGridCommon.DataInitBootstrapMultiselect(elem,
                                    { includeSelectAllOption: false }, function (elem) {
                                        $(elem).multiselect('select', "[All]");
                                        var elemInput = $(elem).parent().parent().find('input[value=""]');
                                        elemInput.prop('checked', true);
                                        elemInput.parent().parent().parent().addClass('active');
                                    });
                            }
                        },
                        align: 'center'
                    },
                    {
                        label: 'Reference Files',
                        name: 'ReferenceFiles',
                        index: 'ReferenceFiles',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        formatter: filePreviewFormatter,
                        align: 'center'
                    },
                    { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: actionsFormatter, width: 100, align: 'center' }
                ],

                viewrecords: true,
                rowNum: 10,
                rowList: [10, 20, 50, 100],
                loadonce: false,
                pager: "#jq-grid-pager",
                height: 'auto',
                gridview: true,
                sortname: 'Name',
                sortable: true,
                sortorder: 'asc',
                cellEdit: true,
                cellsubmit: 'clientArray',
                editurl: 'clientArray',
                autowidth: true,
                colMenu: true,
                gridComplete: function () {
                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.ProceduresGrid.GetGridId());
                    Msr.JqGridCommon.SetupGridLock(Msr.ProceduresGrid.GetGridEditUrl());
                    Msr.JqGridCommon.UnLockWorkflow(Msr.ProceduresGrid.GetReturnUrl());
                    Msr.JqGridCommon.DocPreview();
                }
            });

            Msr.JqGridCommon.BindGridEvents(Msr.ProceduresGrid.GetGridId());

            function filePreviewFormatter(cellvalue, options, rowObject) {
                return Msr.JqGridCommon.FilePreview(cellvalue, options, rowObject);
            }

            function actionsFormatter(cellvalue, options, rowObject) {

                var viewButton = '<a href="/Procedures/view/' + rowObject.ObjectId + '" class="btn btn-xs btn-success" title="View" style="margin:2px;font-size: .8em;"><i class="fa fa-eye"></i></a>';
                var actions = Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, Msr.ProceduresGrid.GetReturnUrl(), Msr.ProceduresGrid.GetGridEditUrl(), hasAdministratorRole);

                return actions + viewButton;
            }
        }
    }