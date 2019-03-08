function LoadProductsGrid(url) {

    $.jgrid.defaults.responsive = true;
    $.jgrid.defaults.styleUI = 'Bootstrap';

    $("#jqGridProducts").jqGrid({
        url: url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        emptyrecords: 'No records to display',
        datatype: "json",
        colModel: [
            {
                label: ' #',
                name: 'Order_id',
                index: 'Order_id' +
                    '',
                key: true,
                colmenu: true,
                search: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 30,
                align: 'center',
                formatter: selectFormatter
            },
            {
                label: 'Product No.',
                name: 'Order_id',
                index: 'Order_id',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 100,
                align: 'left'
            },
            {
                label: 'Supplier',
                name: 'supplier_name',
                index: 'supplier_name',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 80,
                align: 'left'
            },
            {
                label: 'Product Name',
                name: 'Name',
                index: 'Name',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 80,
                align: 'left'
            },
            {
                label: 'Procedure Name',
                name: 'ProcedureName',
                index: 'ProcedureName',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 80,
                align: 'left'
            },
            {
                label: 'Customer name',
                name: 'Customer_root_name',
                index: 'Customer_root_name',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 80,
                align: 'left'
            },
            {
                label: 'Progress',
                name: 'Progress',
                index: 'Progress',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 80,
                align: 'left'
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
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridProductsPager",
        height: 'auto',
        gridview: true,
        sortname: 'Order_id',
        sortable: true,
        sortorder: 'asc',
        cellEdit: false,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: true
    });
    function selectFormatter(cellvalue, options, rowObject) {
        var html = "";

        html = '<input class="selected-file" type="checkbox" value="' + cellvalue + '|' + rowObject.Name + '" />';

        return html;
    }
    $('#jqGridProducts').navGrid("#jqGridProductsPager", {
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
    $('#jqGridProducts').jqGrid('filterToolbar', {
        stringResult: true,
        searchOnEnter: true,
        searchOperators: true
    });

    $('#search').click(function () {

        jQuery("#jqGridProducts").setGridParam({
            page: 1
        }).trigger("reloadGrid");

    });
}