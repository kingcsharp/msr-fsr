var Msr = Msr || {};

Msr.DocumentsGrid = Msr.DocumentsGrid ||
    {
        GetReturnUrl: function () {
            return "/Documents";
        },
        GetGridId: function () {
            return "jq-grid-documents";
        },
        GetGridEditUrl: function () {
            return "/Documents/Edit/";
        },
        LoadDocumentsGrid: function (url) {
            $("#" + Msr.DocumentsGrid.GetGridId()).jqGrid({
                url: url,
                mtype: "GET",
                styleUI: 'Bootstrap',
                datatype: "local",
                colModel: [
                    {
                        label: 'Id',
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
                        align: 'center'
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
                        align: 'center'
                    },
                    {
                        label: 'Creating Co',
                        name: 'CreatingCoName',
                        index: 'CreatingCoName',
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
                        label: 'Creating Dept Name',
                        name: 'DeptName',
                        index: 'DeptName',
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
                        label: 'Security Level',
                        name: 'SecurityLevel',
                        index: 'SecurityLevel',
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
                            value: Msr.JqGridCommon.GetStatusFilters(),
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
                        label: 'Approval Date',
                        name: 'ApprovalDate',
                        index: 'ApprovalDate',
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
                        formatter: 'date',
                        formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                        align: 'center'
                    },
                    {
                        label: 'Checked Out To',
                        name: 'LockedByName',
                        index: 'LockedByName',
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
                        label: 'Updated By',
                        name: 'UpdatedBy',
                        index: 'UpdatedBy',
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
                        label: 'Updated Date',
                        name: 'UpdatedDate',
                        index: 'UpdatedDate',
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
                        formatter: 'date',
                        formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
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
                        formatter: actionFormtter,
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
                sortname: 'Name',
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
                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.DocumentsGrid.GetGridId());
                    Msr.JqGridCommon.SetupGridLock(Msr.DocumentsGrid.GetGridEditUrl());
                    Msr.JqGridCommon.UnLockWorkflow(Msr.DocumentsGrid.GetReturnUrl());
                    Msr.JqGridCommon.DocPreview();
                }

            });


            Msr.JqGridCommon.BindGridEvents(Msr.DocumentsGrid.GetGridId());

            function actionFormtter(cellvalue, options, rowObject) {

                var actions = Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, Msr.DocumentsGrid.GetReturnUrl(), Msr.DocumentsGrid.GetGridEditUrl(), hasAdministratorRole);

                return actions;

            };

            function filePreviewFormatter(cellvalue, options, rowObject) {
                return Msr.JqGridCommon.FilePreview(cellvalue, options, rowObject);
            }


        }
    }
