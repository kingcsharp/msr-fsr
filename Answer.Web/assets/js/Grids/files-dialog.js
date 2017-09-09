
function LoadGrid() {
    $.jgrid.defaults.styleUI = 'Bootstrap';

    $("#jqGridFiles").jqGrid({
        url: '/Files/FilesData',
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            {
                label: ' #',
                name: 'Id',
                index: 'Id',
                key: true,
                colmenu: false,
                search: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 30,
                align: 'center',
                formatter: selectFormatter
            },
            {
                label: ' #',
                name: 'Id',
                index: 'Id',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 30,
                align: 'center',
            },
            {
                label: 'Name',
                name: 'Name',
                index: 'Name',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 150,
                align: 'center'
            },
            {
                label: 'Keywords',
                name: 'Description',
                index: 'Description',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'center'
            }
        ],
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPagerFiles",
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
        ajaxCellOptions: {}

    });
    $('#jqGridFiles').navGrid("#jqGridPagerFiles", {
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
    $('#jqGridFiles').jqGrid('filterToolbar', {
        stringResult: true,
        searchOnEnter: true,
        searchOperators: true
    });

}

function selectFormatter(cellvalue, options, rowObject) {

    var html = "";

    html = '<input class="selected-file" type="checkbox" value="' + cellvalue + '|' + rowObject.Name + '" />';

    return html;
}
