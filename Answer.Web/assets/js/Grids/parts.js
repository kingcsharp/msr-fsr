var Msr = Msr || {};

Msr.PartsGrid = Msr.PartsGrid ||
{
    SetUpGrid: function (returnUrl) {

    $("#jqGrid").jqGrid({
        url: '/Parts/PartsData',
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            {
                label: 'Database Id',
                name: 'Root',
                index: 'Root',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 100,
                align: 'left'
            },
            {
                label: "Current Owner's Part Name",
                name: 'Name',
                index: 'Name',
                colmenu: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 300,
                align: 'left'
            },
            {
                label: "Current Owner's Part #",
                name: 'CompanyPartNumber',
                index: 'CompanyPartNumber',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left',
                hidedlg: false
            },
            {
                label: 'Company #',
                name: 'CompanyName',
                index: 'CompanyName',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Consumable',
                name: 'Consumable',
                index: 'Consumable',
                colmenu: false,
                width: 100,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Spare',
                name: 'Spare',
                index: 'Spare',
                colmenu: false,
                width: 100,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Ordering Unit',
                name: 'Unit',
                index: 'Unit',
                colmenu: false,
                width: 80,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Revision',
                name: 'Rev',
                index: 'Rev',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Approved Status',
                name: 'Status',
                index: 'Status',
                colmenu: false,
                stype: "select",
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { value: Msr.JqGridCommon.GetStatusFilters() },
                align: 'center'
            },

            {
                label: 'Checked Out To',
                name: 'LockedByName',
                index: 'LockedByName',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Reference Files',
                name: 'ReferenceFiles',
                index: 'ReferenceFiles',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                formatter: FilePreviewFormatter,
                align: 'center'
            },
            { name: 'Actions', index: 'ID', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: PartEditFormatter, width: 200, align: 'center' }
        ],

        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPager",
        height: 'auto',
        gridview: true,
        sortname: 'CompanyPartNumber',
        sortable: true,
        sortorder: 'asc',
        cellEdit: true,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: true,
        gridComplete: function () {
            Msr.JqGridCommon.SetupGridLock("/Parts/Edit/");
            Msr.JqGridCommon.UnLockWorkflow(returnUrl);
            Msr.JqGridCommon.DocPreview();
        }
    });
    $('#jqGrid').navGrid("#jqGridPager", {
            search: false, // show search button on the toolbar
            add: false,
            edit: false,
            del: false,
            refresh: true
        },
        {}, // edit options
        {}, // add options
        {}, // delete options
        { multipleSearch: true }
    );
    $('#jqGrid').jqGrid('filterToolbar', {
        stringResult: true,
        searchOnEnter: true,
        searchOperators: true
    });


    function FilePreviewFormatter(cellvalue, options, rowObject) {
        return Msr.JqGridCommon.FilePreview(cellvalue, options, rowObject);
    }

    function PartEditFormatter(cellvalue, options, rowObject) {

        var actions = Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, returnUrl, '/Parts/Edit/');
        return actions;
    }


    $('#search').click(function () {

        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");

    });


}
}