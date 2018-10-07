var Msr = Msr || {};

Msr.RegionGrid = Msr.RegionGrid ||
    {
        GetReturnUrl: function () {
            return "/Regions";
        },
        GetGridId: function () {
            return "jq-grid-regions";
        },
        GetGridEditUrl: function () {
            return "/Regions/Edit/";
        },
        SetUpRegionGrid: function (url) {


            $("#" + Msr.RegionGrid.GetGridId()).jqGrid({
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
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 30,
                        align: 'center',
                    },
                    {
                        label: 'Name',
                        name: 'Name',
                        index: 'Name',
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
                    },
                    {
                        label: 'Approval Status',
                        name: 'Status',
                        index: 'Status',
                        colmenu: false,
                        stype: "select",
                        searchoptions: { value: Msr.JqGridCommon.GetStatusFilters() },
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        align: 'center'
                    },
                    {
                        label: 'Checkout Out To',
                        name: 'LockedByName',
                        index: 'LockedByName',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
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
                        formatter: RegionEditFormatter,
                        width: 80,
                        align: 'center'
                    }
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
                cellEdit: false,
                cellsubmit: 'clientArray',
                editurl: 'clientArray',
                autowidth: true,
                colMenu: true,
                key: true,
                ajaxCellOptions: {},
                gridComplete: function () {

                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.RegionGrid.GetGridId());
                    Msr.JqGridCommon.SetupGridLock(Msr.RegionGrid.GetGridEditUrl());
                    Msr.JqGridCommon.UnLockWorkflow(Msr.RegionGrid.GetReturnUrl());
                }
            });

            Msr.JqGridCommon.BindGridEvents(Msr.RegionGrid.GetGridId());

            function RegionEditFormatter(cellvalue, options, rowObject) {

                var actions = Msr.JqGridCommon.ActionFormtter(cellvalue,
                    options,
                    rowObject,
                    Msr.RegionGrid.GetReturnUrl(),
                    Msr.RegionGrid.GetGridEditUrl()
                );

                return actions;
            }


        }

    }


