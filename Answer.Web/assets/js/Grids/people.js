var Msr = Msr || {};

Msr.PeopleGrid = Msr.PeopleGrid ||
    {
        GetReturnUrl: function () {
            return "/People";
        },
        GetGridId: function () {
            return "jq-grid-people";
        },
        GetGridEditUrl: function () {
            return "/People/Edit/";
        },

        LoadPeopleGrid: function (url) {

            $.jgrid.defaults.styleUI = 'Bootstrap';

            $("#" + Msr.PeopleGrid.GetGridId()).jqGrid({
                url: url,
                mtype: "GET",
                styleUI: 'Bootstrap',
                emptyrecords: 'No records to display',
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
                        width: 100,
                        align: 'left'
                    },
                    {
                        label: 'First Name',
                        name: 'FirstName',
                        index: 'FirstName',
                        colmenu: false,
                        editable: true, // must set editable to true if you want to make the field editable
                        editrules: { required: true },
                        coloptions: {
                            sorting: false,
                            columns: true,
                            filtering: false,
                            seraching: false,
                            grouping: false,
                            freeze: false
                        },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 200,
                        align: 'left'
                    },
                    {
                        label: 'Last Name',
                        name: 'LastName',
                        index: 'LastName',
                        colmenu: false,
                        editable: true,
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
                        width: 150,
                        hidedlg: false
                    },
                    {
                        label: 'User Name',
                        name: 'LoginId',
                        index: 'LoginId',
                        colmenu: false,
                        editable: true,
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
                        width: 150,
                        hidedlg: false
                    },
                    {
                        label: 'Picture',
                        name: 'PicRecord',
                        index: 'PicRecord',
                        colmenu: false,
                        editable: true,
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
                        label: 'Position',
                        name: 'PositionName',
                        index: 'PositionName',
                        colmenu: false,
                        editable: true,
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
                        label: 'Supervisor',
                        name: 'BossName',
                        index: 'BossName',
                        colmenu: false,
                        editable: true,
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
                        label: 'Main Company Root',
                        name: 'RootCoName',
                        index: 'RootCoName',
                        colmenu: false,
                        editable: true,
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
                        label: 'Company Department',
                        name: 'CompanyName',
                        index: 'CompanyName',
                        colmenu: false,
                        editable: true,
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
                        label: 'Primary Work Phone',
                        name: 'PrimaryPhoneNumber',
                        index: 'PrimaryPhoneNumber',
                        colmenu: false,
                        editable: true,
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
                        label: 'Work Email',
                        name: 'EmailAddress',
                        index: 'EmailAddress',
                        colmenu: false,
                        editable: true,
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
                        label: 'Hire Date',
                        name: 'DateHired',
                        index: 'DateHired',
                        colmenu: false,
                        editable: true,
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
                        label: 'System Status',
                        name: 'SystemStatus',
                        index: 'SystemStatus',
                        colmenu: false,
                        editable: true,
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
                        label: 'Revision',
                        name: 'Rev',
                        index: 'Rev',
                        colmenu: false,
                        editable: true,
                        coloptions: {
                            sorting: false,
                            columns: true,
                            filtering: false,
                            seraching: false,
                            grouping: false,
                            freeze: false
                        },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left',
                        width: 100
                    },
                    {
                        label: 'Approval Status',
                        name: 'Status',
                        index: 'Status',
                        colmenu: false,
                        editable: true,
                        stype: "select",
                        coloptions: {
                            sorting: false,
                            columns: true,
                            filtering: false,
                            seraching: false,
                            grouping: false,
                            freeze: false
                        },
                        searchoptions: { value: Msr.JqGridCommon.GetStatusFilters() },
                        align: 'left'
                    },
                    {
                        label: 'Checked Out To',
                        name: 'LockedByName',
                        index: 'LockedByName',
                        colmenu: false,
                        editable: true, // must set editable to true if you want to make the field editable
                        editrules: { required: true },
                        coloptions: {
                            sorting: false,
                            columns: true,
                            filtering: false,
                            seraching: false,
                            grouping: false,
                            freeze: false
                        },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 200,
                        align: 'left'
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
                        index: 'ObjectId',
                        key: true,
                        search: false,
                        hidden: false,
                        colmenu: false,
                        editable: false,
                        formatter: actionFormtter,
                        width: 200,
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
                sortname: 'Id',
                sortable: true,
                sortorder: 'asc',
                cellEdit: false,
                cellsubmit: 'clientArray',
                editurl: 'clientArray',
                autowidth: true,
                colMenu: true,
                gridComplete: function () {

                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.PeopleGrid.GetGridId());
                    Msr.JqGridCommon.SetupGridLock(Msr.PeopleGrid.GetGridEditUrl());
                    Msr.JqGridCommon.UnLockWorkflow(Msr.PeopleGrid.GetReturnUrl());
                    Msr.JqGridCommon.DocPreview();
                }

            });

            Msr.JqGridCommon.BindGridEvents(Msr.PeopleGrid.GetGridId());

            function filePreviewFormatter(cellvalue, options, rowObject) {
                return Msr.JqGridCommon.FilePreview(cellvalue, options, rowObject);
            }

            function actionFormtter(cellvalue, options, rowObject) {

                var actions = Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, Msr.PeopleGrid.GetReturnUrl(), Msr.PeopleGrid.GetGridEditUrl(), hasAdministratorRole);

                var subordinate = '<a  title="SubOrdinate" href="/People/Add/' +
                    rowObject.ObjectId +
                    '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-list-alt"></i></a>';

                return actions + subordinate;
            }
        }
    }
