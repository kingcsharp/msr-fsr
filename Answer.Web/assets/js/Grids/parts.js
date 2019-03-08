var Msr = Msr || {};

Msr.PartsGrid = Msr.PartsGrid ||
    {
        ReturnUrl: '/Parts',
        GetReturnUrl: function () {
            return "/Parts";
        },
        GetGridId: function () {
            return "jq-grid-part";
        },
        GetGridEditUrl: function () {
            return "/Parts/Edit/";
        },
        LoadPartsGrid: function (url) {

            $("#" + Msr.PartsGrid.GetGridId()).jqGrid({
                url: url,
                mtype: "GET",
                styleUI: 'Bootstrap',
                datatype: "local",
                colModel: [
                    {
                        label: 'Database Id',
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
                        label: "Part Name",
                        name: 'Name',
                        index: 'Name',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 300,
                        align: 'left'
                    },
                    {
                        label: "Part #",
                        name: 'CompanyPartNumber',
                        index: 'CompanyPartNumber',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left',
                        width: 200,
                        hidedlg: false
                    },
                    {
                        label: 'Company #',
                        name: 'CompanyName',
                        index: 'CompanyName',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Consumable',
                        name: 'Consumable',
                        index: 'Consumable',
                        colmenu: false,
                        width: 150,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Spare',
                        name: 'Spare',
                        index: 'Spare',
                        colmenu: false,
                        width: 100,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Ordering Unit',
                        name: 'Unit',
                        index: 'Unit',
                        colmenu: false,
                        width: 150,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Revision',
                        name: 'Rev',
                        index: 'Rev',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Approved Status',
                        name: 'Status',
                        index: 'Status',
                        colmenu: false,
                        stype: "select",
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        //searchoptions: { value: Msr.JqGridCommon.GetStatusFilters() },
                        multiselect: true,
                        searchoptions: {
                            sopt: ['eq'],
                            value: Msr.JqGridCommon.GetStatusFilters(),
                            dataInit: function (elem) {
                                Msr.JqGridCommon.DataInitBootstrapMultiselect(elem);
                            }
                        },
                        align: 'center',
                        width: 180
                    },

                    {
                        label: 'Checked Out To',
                        name: 'LockedByName',
                        index: 'LockedByName',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left',
                        width: 190
                    },
                    {
                        label: 'Reference Files',
                        name: 'ReferenceFiles',
                        index: 'ReferenceFiles',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        formatter: FilePreviewFormatter,
                        align: 'center',
                        width: 180,
                        sortable: false
                    },
                    {
                        name: 'Actions', index: 'ID', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: PartEditFormatter, width: 200,
                        align: 'center',
                        sortable: false
                    }
                ],

                viewrecords: true,
                rowNum: 10, rowList: [10, 20, 50, 100],
                loadonce: false,
                pager: "#jq-grid-pager",
                height: 'auto',
                gridview: true,
                sortname: 'CompanyPartNumber',
                sortable: true,
                sortorder: 'asc',
                cellEdit: true,
                cellsubmit: 'clientArray',
                editurl: 'clientArray',
                autowidth: true,
                colMenu: true,
                gridComplete: function () {
                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.PartsGrid.GetGridId());
                    Msr.JqGridCommon.SetupGridLock(Msr.PartsGrid.GetGridEditUrl());
                    Msr.JqGridCommon.UnLockWorkflow(Msr.PartsGrid.GetReturnUrl());
                    Msr.JqGridCommon.DocPreview();
                },
                beforeRequest: function () {
                    Msr.JqGridCommon.ModifyMultiselectData.call(this);
                }
            });

            Msr.JqGridCommon.BindGridEvents(Msr.PartsGrid.GetGridId());

            function FilePreviewFormatter(cellvalue, options, rowObject) {
                return Msr.JqGridCommon.FilePreview(cellvalue, options, rowObject);
            }

            function PartEditFormatter(cellvalue, options, rowObject) {
                var actions = Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, Msr.PartsGrid.GetReturnUrl(), '/Parts/Edit/', hasAdministratorRole);
                return actions;
            }
        }
    }
