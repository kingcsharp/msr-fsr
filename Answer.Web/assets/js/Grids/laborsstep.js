function LoadLaborstepGrid(url, procedureStepId, id, relationship) {
    
    //$.jgrid.defaults.responsive = true;
    $.jgrid.defaults.styleUI = 'Bootstrap';

    Smooch.init({ appToken: '9wxoxi2wbcbymhjf1ex1a0dux' });

    $("#jqGrid").jqGrid({
        url: url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        emptyrecords: 'No records to display',
        datatype: "json",
        colModel: [
            {
                label: 'RoleName',
                name: 'RoleName',
                index: 'RoleName',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 50,
                align: 'left'
            },
            {
                label: 'Duration',
                name: 'Qty',
                index: 'Qty',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 200,
                align: 'left'
            },
            {
                label: 'Duration Type',
                name: 'QtyType',
                index: 'QtyType',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 200,
                align: 'left'
            },
            {
                label: 'Labor Type',
                name: 'LaborRole',
                index: 'LaborRole',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 200,
                align: 'left'
            },
            { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: procedureObjectsEditFormatter, width: 100, align: 'center' }
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
        loadonce: false, // this is just for the demo
        pager: "#jqGridPager",
        height: 'auto',
        gridview: true,
        sortname: 'RoleName',
        sortable: true,
        sortorder: 'asc',
        cellEdit: false,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: true,

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
    function procedureObjectsEditFormatter(cellvalue, options, rowObject) {
        var thisCellVal = '<a href="/PreProSearch/EditLabor/' + rowObject.Id + '?procStepId=' + procedureStepId + '&relationship=' + relationship + '&id=' + id + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Edit</a>';
        thisCellVal = thisCellVal + '<a href="//Hide/' + rowObject.Id + '" title="Delete" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-eye-slash"></i> Hide</a>';
        return thisCellVal;
    }

    $('#search').click(function () {
        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");
    });
}