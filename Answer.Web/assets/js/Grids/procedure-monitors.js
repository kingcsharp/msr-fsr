function LoadProcedureMonitorGrid(url, objectId) {
    $.jgrid.defaults.styleUI = 'Bootstrap';

    

    $("#jqGrid").jqGrid({
        url: url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        emptyrecords: 'No records to display',
        datatype: 'json',
        colModel: [
            {
                name: 'Id',
                index: 'Id',
                key: true,
                colmenu: true,
                hidden: true,
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
                label: 'Monitor Number',
                name: 'MonitorNumber',
                index: 'MonitorNumber',
                width: 100,
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'center'
            },
            {
                label: 'Description',
                name: 'Description',
                index: 'Description',
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
                label: 'Monitor Type',
                name: 'Monitor_Type',
                index: 'Monitor_Type',
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
                label: 'Should Be',
                name: 'Should_Be',
                index: 'Should_Be',
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
                label: 'Highest Threshold',
                name: 'Highest_Threshold',
                index: 'Highest_Threshold',
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
                label: 'High Threshold',
                name: 'High_Threshold',
                index: 'High_Threshold',
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
                label: 'Target',
                name: 'Target',
                index: 'Target',
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
                label: 'Low Threshold',
                name: 'Low_Threshold',
                index: 'Low_Threshold',
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
                label: 'Lowest Threshold',
                name: 'Lowest_Threshold',
                index: 'Lowest_Threshold',
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
                label: 'Based On Opinion',
                name: 'OpinionText',
                index: 'OpinionText',
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
                label: 'Use Result',
                name: 'Use_Result_Text',
                index: 'Use_Result_Text',
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
                label: 'Hide Target',
                name: 'Hide_Target_Text',
                index: 'Hide_Target_Text',
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
                label: 'If Fail Next',
                name: 'Fail_Action',
                index: 'Fail_Action',
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
                name: 'Related_Object_Id',
                index: 'Related_Object_Id',
                key: true,
                colmenu: false,
                hidden: true,
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
                name: 'Actions',
                index: 'Description',
                key: true,
                search: false,
                hidden: false,
                colmenu: false,
                editable: false,
                formatter: monitorEditFormatter,
                width: 200,
                align: 'center'
            }
        ],
        ajaxRowOptions: {
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        },
        serializeRowData: function(postdata) {
            return JSON.stringify(postdata);
        },
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPager",
        height: 'auto',
        gridview: true,
        sortname: 'MonitorNumber',
        sortable: true,
        sortorder: 'asc',
        cellEdit: true,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: true,
        afterSaveCell: function (rowid, cellname, value, iRow, iCol) {
            var id = $('#jqGrid').jqGrid('getCell', rowid, 'Id');
            var monitorNumber = $('#jqGrid').jqGrid('getCell', rowid, 'MonitorNumber');
            var options = {
                Id: id,
                MonitorNumber: monitorNumber
            }
            $.ajax({
                type: 'POST',
                url: '/ProcedureMonitors/Reorder',
                data: options,
                dataType: 'JSON',
                success: function (resultData) {
                    alert("hi");
                    console.log("row with rowid=" + rowid + " is successfuly modified.");
                }
            });
            $("#jqGrid").jqGrid().trigger('reloadGrid');
            console.log('afterSaveCell : ' + cellname + 'rowid :' + rowid);
        },
        gridComplete: function() {
            
        }
});
    $('#jqGrid').navGrid("#jqGridPager", {
        search: false, // show search button on the toolbar
        add: false,
        edit: false,
        del: false,
        refresh: true
    },
        {},  // edit options
        {}, // add options
        {}, // delete options
        { multipleSearch: true }
    );
    $('#jqGrid').jqGrid('filterToolbar', {
        stringResult: true,
        searchOnEnter: true,
        searchOperators: true
    });

    function monitorEditFormatter(cellvalue, options, rowObject) {

        var editButton = '<a href="/ProcedureMonitors/Edit/' + rowObject.Id + '" data-call-back-id ="' + rowObject.Id + '"  class="btn btn-xs btn-success editpeople" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i>  Edit</a>';
        editButton = editButton + '<a href="/ProcedureMonitors/Delete/' + rowObject.Id + "," + rowObject.Related_Object_Id + '" data-call-back-id="' + rowObject.Related_Object_Id + '" title="Delete" class="btn btn-xs btn-danger deletepart" style="margin:2px;font-size: .8em;"><i class="fa fa-trash"></i> Delete</a>';
       return editButton;

    };
   
    $('#search').click(function () {

        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");

    });
}

