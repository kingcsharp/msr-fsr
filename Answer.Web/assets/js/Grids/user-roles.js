var Msr = Msr || {};

Msr.RoleGrid = Msr.RoleGrid ||
    {
        GetReturnUrl: function () {
            return "/Roles";
        },
        GetGridId: function () {
            return "jq-grid-role";
        },
        GetGridEditUrl: function () {
            return "/Roles/Edit/";
        },
        LoadRolesTypesGrid: function (url) {

            $.jgrid.defaults.styleUI = 'Bootstrap';

            $("#" + Msr.RoleGrid.GetGridId()).jqGrid({
                url: url,
                mtype: "GET",
                styleUI: 'Bootstrap',
                emptyrecords: 'No records to display',
                datatype: "local",
                colModel: [
                    {
                        label: 'Role Name',
                        name: 'RoleName',
                        index: 'RoleName',
                        colmenu: true,
                        editable: true, // must set editable to true if you want to make the field editable
                        editrules: { required: true },
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 150,
                        align: 'left'
                    },
                    {
                        label: 'Security Level Name',
                        name: 'SecurityLevelName',
                        index: 'SecurityLevelName',
                        colmenu: false,
                        editable: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left',
                        width: 250,
                        hidedlg: false
                    },
                    {
                        label: 'Security Level',
                        name: 'SecurityLevel',
                        index: 'SecurityLevel',
                        colmenu: false,
                        editable: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'center'
                    },
                    {
                        label: 'Locked By',
                        name: 'LockedBy',
                        index: 'LockedBy',
                        colmenu: false,
                        editable: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'center'
                    },
                    {
                        label: 'Revision',
                        name: 'Revision',
                        index: 'Revision',
                        colmenu: false,
                        editable: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Approval Status',
                        name: 'Status',
                        index: 'Status',
                        colmenu: false,
                        editable: true,
                        stype: "select",
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        multiselect: true,
                        searchoptions: {
                            sopt: ['eq'],
                            value: Msr.JqGridCommon.GetStatusFilters(),
                            dataInit: function (elem) {
                                Msr.JqGridCommon.DataInitBootstrapMultiselect(elem);
                            }
                        },
                        align: 'left'
                    },
                    {
                        label: 'Checked Out To',
                        name: 'LockedByName',
                        index: 'LockedByName',
                        colmenu: false,
                        editable: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'center'
                    },
                    {
                        name: 'Actions',
                        index: 'ObjectId',
                        key: true,
                        search: false,
                        hidden: false,
                        colmenu: false,
                        editable: false,
                        formatter: actionFormtter,
                        width: 150,
                        align: 'center'
                    }
                ],
                ajaxRowOptions: {
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                serializeRowData: function (postdata) {
                    return JSON.stringify(postdata);
                },
                viewrecords: true,
                rowNum: 10,
                rowList: [10, 20, 50, 100],
                loadonce: false,
                pager: "#jq-grid-pager",
                height: 'auto',
                gridview: true,
                sortname: 'RoleName',
                sortable: true,
                sortorder: 'asc',
                cellEdit: false,
                cellsubmit: 'clientArray',
                editurl: 'clientArray',
                autowidth: true,
                colMenu: true,
                gridComplete: function () {
                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.RoleGrid.GetGridId());
                    Msr.JqGridCommon.SetupGridLock(Msr.RoleGrid.GetGridEditUrl());
                    Msr.JqGridCommon.UnLockWorkflow(Msr.RoleGrid.GetReturnUrl());
                },
                beforeRequest: function () {
                    Msr.JqGridCommon.ModifyMultiselectData.call(this);
                }
            });

            Msr.JqGridCommon.BindGridEvents(Msr.RoleGrid.GetGridId());

            function actionFormtter(cellvalue, options, rowObject) {

                var actions = Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, Msr.RoleGrid.GetReturnUrl(), Msr.RoleGrid.GetGridEditUrl());

                return actions;
            }

        }

    }