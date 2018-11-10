var Msr = Msr || {};

Msr.LocationsGrid = Msr.LocationsGrid ||
    {
        GetReturnUrl: function () {
            return "/Locations";
        },
        GetGridId: function () {
            return "jq-grid-location";
        },
        GetGridEditUrl: function () {
            return "/Locations/Edit/";
        },
        SetUpGrid: function (url) {
            $(document).ready(function () {
                $("#" + Msr.LocationsGrid.GetGridId()).jqGrid({
                    url: url,
                    mtype: "GET",
                    styleUI: 'Bootstrap',
                    datatype: "local",
                    colModel: [
                        {
                            label: 'ID',
                            name: 'Root',
                            index: 'Root',
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
                            width: 50,
                            align: 'center',
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
                            width: 150,
                            align: 'center'
                        },
                        {
                            label: 'Region',
                            name: 'RegionName',
                            index: 'RegionName',
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
                            label: 'PARENTS',
                            name: 'ParentLocationName',
                            index: 'ParentLocationName',
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
                            name: 'Actions',
                            index: 'ID',
                            key: true,
                            search: false,
                            hidden: false,
                            colmenu: false,
                            editable: false,
                            formatter: LocationEditFormatter,
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

                        Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.LocationsGrid.GetGridId());
                        Msr.JqGridCommon.SetupGridLock(Msr.LocationsGrid.GetGridEditUrl());
                        Msr.JqGridCommon.UnLockWorkflow(Msr.LocationsGrid.GetReturnUrl());
                    }

                });
                Msr.JqGridCommon.BindGridEvents(Msr.LocationsGrid.GetGridId());

                function LocationEditFormatter(cellvalue, options, rowObject) {

                    var actions =
                        Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, Msr.LocationsGrid.GetReturnUrl(), '/Locations/Edit/');

                    return actions;
                }
            });

        }
    }