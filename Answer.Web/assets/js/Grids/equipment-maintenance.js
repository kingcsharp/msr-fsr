function LoadEquipmentGrid() {
    $.jgrid.defaults.responsive = true;

    $.jgrid.defaults.styleUI = 'Bootstrap';

    $("#jqGrid").jqGrid({
        url: '/EquipmentMaintenance/EquipmentMaintenanceData',
        mtype: "GET",
        styleUI: 'Bootstrap',
        emptyrecords: 'No records to display',
        datatype: "json",
        colModel: [
            {
                label: 'Id',
                name: 'Id',
                index: 'Id',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 100,
                align: 'left'
            },
          {
                label: 'Primary Location',
                name: 'PrimaryLocation',
                index: 'PrimaryLocation',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left',
                hidedlg: false
            },
            {
                label: 'Sub Location 1',
                name: 'SubLocationFirst',
                index: 'SubLocationFirst',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Sub Location 2',
                name: 'SubLocationSecond',
                index: 'SubLocationSecond',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Date/Time',
                name: 'DateTime',
                index: 'DateTime',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                formatter: 'date',
                formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y H:i" },
                align: 'center'
            },
            {
                label: 'Technician',
                name: 'Technician',
                index: 'Technician',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Equipment Trouble State',
                name: 'TroubleState',
                index: 'TroubleState',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Maintenance Task',
                name: 'MaintenanceTask',
                index: 'MaintenanceTask',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Comments',
                name: 'Comments',
                index: 'Comments',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Status',
                name: 'Status',
                index: 'Status',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                stype: "select",
                searchoptions: { value: ":[All];NEW:New;PROCESSING:Processing;COMPLETED:Completed," },
                width: 150,
                align: 'left'
            },
            { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: EquipmentsEditFormatter, width: 200, align: 'center' }
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
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPager",
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

        }

    });
    $('#jqGrid').navGrid("#jqGridPager", {
            search: false, 
            add: false,
            edit: false,
            del: false,
            refresh: true
        },
        
        { multipleSearch: true }
    );
    $('#jqGrid').jqGrid('filterToolbar', {
        stringResult: true,
        searchOnEnter: true,
        searchOperators: true
    });

    $("#gs_DateTime").datepicker({
        dateFormat: "mm/dd/yy"
    }).on('changeDate', function () {
        var sgrid = $("#jqGrid")[0];
        sgrid.triggerToolbar();
        $(this).datepicker('hide');
    });

    function EquipmentsEditFormatter(cellvalue, options, rowObject) {

        var editButton = '<a  title="Edit" href="/EquipmentMaintenance/Edit/' + rowObject.Id + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';

        return editButton;
    }

    $('#search').click(function () {

        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");

    });
}

