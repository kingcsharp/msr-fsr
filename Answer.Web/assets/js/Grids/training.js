var Msr = Msr || {};

Msr.TrainingGrid = Msr.TrainingGrid ||
    {
        ReturnUrl: '/Training',
        GetReturnUrl: function () {
            return "/Training";
        },
        GetGridId: function () {
            return "jq-grid-training";
        },
        GetGridEditUrl: function () {
            return "#";
        },
        LoadTrainingGrid: function (url) {

            $("#" + Msr.TrainingGrid.GetGridId()).jqGrid({
                url: url,
                mtype: "GET",
                styleUI: 'Bootstrap',
                datatype: "local",
                colModel: [
                    {
                        label: 'Employee_Name',
                        name: 'FullName',
                        index: 'FullName',
                        key: true,
                        colmenu: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 100,
                        align: 'center'
                    },
                    {
                        label: "Certification",
                        name: 'PositionName',
                        index: 'PositionName',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 200,
                        align: 'center'
                    },
                    {
                        label: "Issue_Date",
                        name: 'StartDate',
                        index: 'StartDate',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'center',
                        width: 200,
                        formatter: 'date',
                        formatoptions: { srcformat: "m/d/Y", newformat: "m/d/Y" },
                        hidedlg: false
                    },
                    {
                        label: 'Expiration_Date',
                        name: 'EndDate',
                        index: 'EndDate',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'center',
                        formatter: 'date',
                        formatoptions: { srcformat: "m/d/Y", newformat: "m/d/Y" }
                    },
                    {
                        label: 'Status',
                        name: 'Status',
                        index: 'Status',
                        colmenu: false,
                        width: 150,
                        formatter: changeStatusName,
                        stype: "select",
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        multiselect: true,
                        searchoptions: {
                            sopt: ['eq'],
                            value: ':[All];Active:Active;Deactive:Inactive',
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
                        align: 'center'
                    }
                ],

                viewrecords: true,
                rowNum: 10, rowList: [10, 20, 50, 100],
                loadonce: false,
                pager: "#jq-grid-pager",
                height: 'auto',
                gridview: true,
                sortname: 'FullName',
                sortable: true,
                sortorder: 'asc',
                cellEdit: true,
                cellsubmit: 'clientArray',
                editurl: 'clientArray',
                autowidth: true,
                colMenu: true,
                gridComplete: function () {
                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.TrainingGrid.GetGridId());
                    Msr.JqGridCommon.SetupGridLock(Msr.TrainingGrid.GetGridEditUrl());
                    Msr.JqGridCommon.UnLockWorkflow(Msr.TrainingGrid.GetReturnUrl());
                }
            });

            Msr.JqGridCommon.BindGridEvents(Msr.TrainingGrid.GetGridId());


        }
    }

function changeStatusName(cellvalue, options, rowObject) {
    if (rowObject.Status === "Deactive") {
        return "Inactive";
    }
    return rowObject.Status;
}
