
function LoadGrid() {
    $.jgrid.defaults.styleUI = 'Bootstrap';

    $("#jqGridFiles").jqGrid({
        url: '/Files/LatestFilesData',
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            {
                label: '&#x2610;',
                name: 'LINKED_DOC_ID',
                index: 'LINKED_DOC_ID',
                key: true,
                search: false,
                coloptions: { sorting: false, columns: true, filtering: false, searching: false, grouping: false, freeze: false },
                width: 30,
                align: 'center',
                formatter: selectFormatter,
            },
            {
                label: ' #',
                name: 'LINKED_DOC_ID',
                index: 'LINKED_DOC_ID',
                search: false,
                coloptions: { sorting: false, columns: true, filtering: false, searching: false, grouping: false, freeze: false },
                width: 30,
                align: 'center',
            },
            {
                label: 'Name',
                name: 'NAME',
                index: 'NAME',
                search: true,
                coloptions: { sorting: false, columns: true, filtering: true, searching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: true, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 150,
                align: 'center'
            },
            {
                label: 'File Type',
                name: 'CONTENTTYPE',
                index: 'CONTENTTYPE',
                search: true,
                coloptions: { sorting: false, columns: true, filtering: false, searching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: true, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'center',
                formatter: typeFormatter
            }
        ],
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPagerFiles",
        height: 'auto',
        gridview: true,
        sortname: 'NAME',
        sortable: true,
        sortorder: 'asc',
        cellEdit: false,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: false,
        key: true,
        loadui: 'disable',
        ajaxCellOptions: {},
        gridComplete: function () {
            $('.ui-jqgrid').unblock();

        },
        beforeRequest: function () {
            $('.ui-jqgrid').block({ message: '<h1><img src="/assets/img/nice_loader.gif" />  Loading. Please wait...</h1>' });
        }

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
    return '<input class="selected-file" type="checkbox" value="' + cellvalue + '|' + rowObject.Name + '" />';
}


function typeFormatter(cellvalue) {
    var TypeName = '';
    if (cellvalue === 'application/pdf') {
        TypeName = 'PDF';
    }
    else if (cellvalue == 'image/png' || cellvalue == 'image/jpeg' || cellvalue == 'image/gif'){
        TypeName = 'Image';
    }
    else if (cellvalue == 'application/vnd.openxmlformats-officedocument.wordprocessingml.document' || cellvalue == 'application/msword') {
        TypeName = 'MS Word';
    }
    else if (cellvalue == 'application/vnd.openxmlformats-officedocument.presentationml.presentation') {
        TypeName = 'MS Powerpoint';
    }
    return TypeName;
}
