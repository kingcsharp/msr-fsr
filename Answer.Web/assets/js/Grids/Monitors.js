var Msr = Msr || {};

Msr.MonitorsGrid = Msr.MonitorsGrid ||
{
    LoadMonitorsGrid: function (url, description) {

        $("#jqGrid").jqGrid({
            url: url +'?description='+ description,
            mtype: "GET",
            styleUI: 'Bootstrap',
            datatype: "json",
            colModel: [
                {
                    label: '#',
                    name: 'RollUpId',
                    index: 'RollUpId',
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
            pager: "#jqGridPager",
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
            gridComplete: function() {
                $('.deletepart').on('click',
                    function(e) {
                        e.preventDefault();

                        var callBackId = $(this).data('call-back-id');
                        var callBackName = $(this).data('call-back-name');

                        eModal.confirm('Do you really want to delete ' + callBackName + ' ?', 'Confirmation delete')
                            .then(confirmCallback, optionalCancelCallback);

                        function confirmCallback() {
                            console.log("ok")
                            window.location.href = "/Locations/LocationDelete/" + callBackId
                        }

                        function optionalCancelCallback() {
                            console.log("cancel")
                        }

                    })
            },

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
            //thisCellVal = '<a href="/Monitors/edit/'+ '" title="Edit" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Edit</a>';

            //thisCellVal += '<a href="/Monitors/Delete/' + '" data-call-back-name="' + '" data-call-back-id="' + '" title="Delete" class="btn btn-xs btn-danger deletepart" style="margin:2px;font-size: .8em; "><i class="fa fa-trash"></i> Delete</a>';
            //return thisCellVal;
        }
    }
}