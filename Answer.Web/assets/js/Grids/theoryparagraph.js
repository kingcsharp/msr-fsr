function LoadTheoryParagraphDialogGrid() {
    $.jgrid.defaults.styleUI = 'Bootstrap';

    $("#jqGridTheory").jqGrid({
        url: '/TheoryParagraph/TheoryParagraphsData',
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            {
                label: ' #',
                name: 'SendId',
                index: 'SendId',
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
                label: 'Id',
                name: 'Id',
                index: 'Id',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 80,
                align: 'left'
            },
            {
                label: 'Theory Name',
                name: 'Name',
                index: 'Name',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 300,
                align: 'left'
            },
            {
                label: 'Creating Co',
                name: 'CreatingCoName',
                index: 'CreatingCoName',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 150,
                align: 'left'
            },
            {
                label: 'Approval Status',
                name: 'Status',
                index: 'Status',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 150,
                align: 'left'
            }
        ],
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPagerTheory",
        height: 'auto',
        gridview: true,
        sortname: 'Root',
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
    $('#jqGridTheory').navGrid("#jqGridPagerTheory", {
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
}

function selectFormatter(cellvalue, options, rowObject) {

    var html = "";

    html = '<input class="selected-file" type="checkbox" value="' + cellvalue + '|' + rowObject.Name + '" />';

    return html;
}