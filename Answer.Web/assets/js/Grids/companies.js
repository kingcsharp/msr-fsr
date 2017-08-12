function LoadCompanyGrid() {
    //$.jgrid.defaults.responsive = true;
    $.jgrid.defaults.styleUI = 'Bootstrap';

    Smooch.init({ appToken: '9wxoxi2wbcbymhjf1ex1a0dux' });

    $("#jqGrid").jqGrid({
        url: '/Companies/CompaniesData',
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
                width: 50,
                align: 'left'
            },
            {
                label: 'Name',
                name: 'Name',
                index: 'Name',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 200,
                align: 'left'
            },
            {
                label: 'Immediate Parent',
                name: 'ParentName',
                index: 'ParentName',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left',
                width: 150,
                hidedlg: false
            },
            {
                label: 'lOGO',
                name: 'PicRecord',
                index: 'PicRecord',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Root',
                name: 'Root',
                index: 'Root',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Revision',
                name: 'Rev',
                index: 'Rev',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Approval Status',
                name: 'Status',
                index: 'Status',
                colmenu: false,
                editable: true,
                stype: "select",
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { value: ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING:Creating or Approved;CREATING, DENIED: Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete" },
                align: 'left'
            },
            { name: 'Actions', index: 'ID', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: CompaniesEditFormatter, width: 200, align: 'center' }
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
        sortname: 'Id',
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
    function CompaniesEditFormatter(cellvalue, options, rowObject) {
        thisCellVal = '<a href="/Companies/Edit/' + rowObject.ObjectId + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Edit</a>';
        thisCellVal = thisCellVal + '<a href="/Companies/Details/' + rowObject.ObjectId + '" title="View Company" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-eye"></i> Details</a>';
        thisCellVal = thisCellVal + '<a href="/Companies/Delete/' + rowObject.ObjectId + '" data-call-back-name="' + rowObject.Name + '" data-call-back-id="' + rowObject.ObjectId + '" title="Delete" class="btn btn-xs btn-danger deletepart" style="margin:2px;font-size: .8em;"><i class="fa fa-trash"></i> Delete</a>';
        return thisCellVal;
    }
    $('#search').click(function () {

        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");

    });
}

function LoadCompanyDialogGrid() {
    $.jgrid.defaults.styleUI = 'Bootstrap';

    $("#jqGridCompanies").jqGrid({
        url: '/Companies/CompaniesData',
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            {
                label: ' #',
                name: 'ObjectId',
                index: 'ObjectId',
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
                width: 50,
                align: 'left'
            },
            {
                label: 'Name',
                name: 'Name',
                index: 'Name',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 200,
                align: 'left'
            },
            {
                label: 'Immediate Parent',
                name: 'ParentName',
                index: 'ParentName',
                colmenu: false,
                editable: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left',
                width: 150,
                hidedlg: false
            }
        ],
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10,
        loadonce: false, // this is just for the demo
        pager: "#jqGridPagerCompanies",
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
    $('#jqGridCompanies').navGrid("#jqGridPagerCompanies", {
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
