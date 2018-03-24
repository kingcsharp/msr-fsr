function SetUpGrid(returnUrl) {


    $(document).ready(function () {
        $("#jqGrid").jqGrid({
            url: '/Locations/locationsData',
            mtype: "GET",
            styleUI: 'Bootstrap',
            datatype: "json",
            colModel: [
                {
                    label: ' #',
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
                    width: 30,
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
                    searchoptions: {
                        value:
                        ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Creating or Approved;CREATING, DENIED:Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete"
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
                    label: 'Locked By',
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
            pager: "#jqGridPager",
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

                Msr.JqGridCommon.SetupGridLock("/Locations/Edit/");
                Msr.JqGridCommon.UnLockWorkflow(returnUrl);
            }

        });
        $('#jqGrid').navGrid("#jqGridPager",
            {
                refresh: true,
                search: false, // show search button on the toolbar
                add: false,
                edit: false,
                del: false,

            },
            {}, // edit options
            {}, // add options
            {}, // delete options
            { multipleSearch: true }
        );
        $('#jqGrid').jqGrid('filterToolbar',
            {
                stringResult: true,
                searchOnEnter: true,
                searchOperators: true
            });

        function LocationEditFormatter(cellvalue, options, rowObject) {

            var actions = Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, returnUrl, '/Locations/Edit/');

            return actions;
        }
    });

}
