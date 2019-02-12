var Msr = Msr || {};

Msr.ActualPartsGrid = Msr.ActualPartsGrid ||
    {
        GetReturnUrl: function () {
            return "ActualParts";
        },
        GetGridId: function () {
            return "jq-grid-actual-part";
        },
        GetGridEditUrl: function () {
            return "/ActualParts/Edit/";
        },
        LoadActualPartsGrid: function (url) {

            $.jgrid.defaults.styleUI = 'Bootstrap';

            $("#" + Msr.ActualPartsGrid.GetGridId()).jqGrid({
                url: url,
                mtype: "GET",
                styleUI: 'Bootstrap',
                emptyrecords: 'No records to display',
                datatype: "local",
                colModel: [
                    {
                        label: 'Database ID',
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
                        width: 150,
                        align: 'left'
                    },
                    {
                        label: 'Serial Num',
                        name: 'Serial',
                        index: 'Serial',
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
                        searchoptions: {
                            searchOperMenu: false,
                            sopt: ['eq', 'gt', 'lt', 'ge', 'le']
                        },
                        width: 200,
                        align: 'left',
                        formatter: serialFormatter
                    },
                    {
                        label: 'Nick Name',
                        name: 'NickName',
                        index: 'NickName',
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
                        label: 'Part Description',
                        name: 'PartDesc',
                        index: 'PartDesc',
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
                        width: 350,
                        align: 'left'
                    },
                    {
                        label: 'Company Part Number',
                        name: 'CompanyPartNumber',
                        index: 'CompanyPartNumber',
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
                        width: 300,
                        align: 'left'
                    },
                    {
                        label: 'Qty',
                        name: 'Qty',
                        index: 'Qty',
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
                        width: 80,
                        align: 'left'
                    },
                    {
                        label: 'Location Name',
                        name: 'LocationName',
                        index: 'LocationName',
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
                        width: 250,
                        align: 'left',
                        formatter: locationFormatter
                    },
                    {
                        label: 'Current Owner',
                        name: 'CurrentOwnerName',
                        index: 'CurrentOwnerName',
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
                        width: 150,
                        align: 'left'
                    },
                    {
                        label: 'Part Status',
                        name: 'ApStatus',
                        index: 'ApStatus',
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
                        stype: "select",
                        searchoptions: { value: ":[All];ap_available:ap_available;ap_installed:ap_installed;ap_consumed:ap_consumed;ap_filled:ap_filled;ap_filled:ap_filled;ap_held:ap_held;ap_installed:ap_installed;ap_received:ap_received" },
                        width: 150,
                        align: 'left',
                        hidedlg: false
                    },
                    {
                        label: 'Revision',
                        name: 'Rev',
                        index: 'Rev',
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
                        width: 80,
                        align: 'left'
                    },
                    {
                        label: 'Approval Status',
                        name: 'Status',
                        index: 'Status',
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
                        stype: "select",
                        searchoptions: { value: Msr.JqGridCommon.GetStatusFilters() },
                        width: 150,
                        align: 'left'
                    },
                    {
                        label: 'Checked out to',
                        name: 'CreatingCoName',
                        index: 'CreatingCoName',
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
                        name: 'Actions',
                        index: 'Id',
                        key: true,
                        search: false,
                        hidden: false,
                        colmenu: false,
                        editable: false,
                        formatter: actionFormatter,
                        width: 250,
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
                viewrecords: true, // show the current page, data rang and total records on the toolbar
                rowNum: 10,
                rowList: [10, 20, 50, 100],
                loadonce: false, // this is just for the demo
                pager: "#jq-grid-pager",
                height: 'auto',
                gridview: true,
                sortname: 'PartDesc',
                sortable: true,
                sortorder: 'asc',
                cellEdit: false,
                cellsubmit: 'clientArray',
                editurl: 'clientArray',
                autowidth: true,
                colMenu: true,

                gridComplete: function () {
                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.ActualPartsGrid.GetGridId());
                    Msr.JqGridCommon.SetupGridLock(Msr.ActualPartsGrid.GetGridEditUrl());
                    Msr.JqGridCommon.UnLockWorkflow(Msr.ActualPartsGrid.GetReturnUrl());
                }
            });

            Msr.JqGridCommon.BindGridEvents(Msr.ActualPartsGrid.GetGridId());

            function actionFormatter(cellvalue, options, rowObject) {

                var actions =
                    Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, Msr.ActualPartsGrid.GetReturnUrl(), Msr.ActualPartsGrid.GetGridEditUrl());

                return actions;
            }

            function serialFormatter(cellvalue, options, rowObject) {

                var thisCellVal = '<a href="/ActualParts/Index/?serial=' + cellvalue + '">' + cellvalue + '</a>';

                return thisCellVal;
            }

            function locationFormatter(cellvalue, options, rowObject) {

                var thisCellVal = '<a href="/Locations/edit/' + rowObject.LocationObjectId + '">' + cellvalue + '</a>';

                return thisCellVal;
            }
        }
    }