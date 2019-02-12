var Msr = Msr || {};

Msr.MonitorsGrid = Msr.MonitorsGrid ||
    {
        GetReturnUrl: function () {
            return "/Monitors";
        },
        GetGridId: function () {
            return "jq-grid-monitors";
        },
        GetGridEditUrl: function () {
            return "/Monitors/Edit/";
        },
        LoadMonitorsGrid: function (url, description) {

            $("#" + Msr.MonitorsGrid.GetGridId()).jqGrid({
                url: url + '?description=' + description,
                mtype: "GET",
                styleUI: 'Bootstrap',
                datatype: "local",
                colModel: [
                    {
                        label: '#',
                        name: 'RollUpId',
                        index: 'RollUpId',
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
                        width: 50,
                        align: 'center',
                    },
                    {
                        label: 'Description',
                        name: 'Description',
                        index: 'Description',
                        colmenu: false,
                        coloptions: {
                            sorting: false,
                            columns: true,
                            filtering: false,
                            seraching: false,
                            grouping: false,
                            freeze: false
                        },
                        searchoptions: { defaultValue: description },
                        width: 150,
                        align: 'center'
                    },
                    {
                        label: 'MonitorType',
                        name: 'MonitorType',
                        index: 'MonitorType',
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
                        label: 'Result',
                        name: 'PrintResult',
                        index: 'PrintResult',
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
                        label: 'Passing',
                        name: 'IsPassing',
                        index: 'IsPassing',
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
                        label: 'Worker Name',
                        name: 'WorkerName',
                        index: 'WorkerName',
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
                        label: 'Task Completed',
                        name: 'TaskStopDate',
                        index: 'TaskStopDate',
                        colmenu: false,
                        formatter: 'date',
                        formatoptions: { newformat: 'm/d/Y' },
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
                        label: 'Serial Number',
                        name: 'ActualPartsApprovedDataSerial',
                        index: 'ActualPartsApprovedDataSerial',
                        colmenu: false,
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
                sortname: 'WorkerName',
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
                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.MonitorsGrid.GetGridId());

                    $('.deletepart').on('click',
                        function (e) {
                            e.preventDefault();

                            var callBackId = $(this).data('call-back-id');
                            var callBackName = $(this).data('call-back-name');

                            eModal.confirm('Do you really want to delete ' + callBackName + ' ?', 'Confirmation delete')
                                .then(confirmCallback, optionalCancelCallback);

                            function confirmCallback() {
                                window.location.href = "/Locations/LocationDelete/" + callBackId;
                            }

                            function optionalCancelCallback() {
                            }

                        });
                }

            });

            Msr.JqGridCommon.BindGridEvents(Msr.MonitorsGrid.GetGridId());

        }
    }