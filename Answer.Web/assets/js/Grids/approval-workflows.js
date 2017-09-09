$("#jqGrid").jqGrid({
    url: '/ApprovalWorkflows/ApprovalWorkflowsData',
    mtype: "GET",
    styleUI: 'Bootstrap',
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
            label: 'Approval Workflow Name',
            name: 'Name',
            index: 'Name',
            colmenu: true,
            editable: true, // must set editable to true if you want to make the field editable
            editrules: { required: true },
            coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
            searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
            width: 200,
            align: 'left'
        },
        { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: WFEditFormatter, width: 200, align: 'center' }
    ],

    viewrecords: true, // show the current page, data rang and total records on the toolbar
    rowNum: 10, rowList: [10, 20, 50, 100],
    loadonce: false, // this is just for the demo
    pager: "#jqGridPager",
    height: 'auto',
    gridview: true,
    sortname: 'Id',
    sortable: true,
    sortorder: 'asc',
    cellEdit: true,
    cellsubmit: 'clientArray',
    editurl: 'clientArray',
    autowidth: true,
    colMenu: true,
    gridComplete: function () {
        $('.deletepart').on('click', function (e) {
            e.preventDefault();

            var callBackId = $(this).data('call-back-id');
            var callBackName = $(this).data('call-back-name');

            eModal.confirm('Do you really want to delete ' + callBackName + ' ?', 'Confirmation delete')
                .then(confirmCallback, optionalCancelCallback);

            function confirmCallback() {
                console.log("ok")
                window.location.href = "/parts/PartDelete/" + callBackId
            }
            function optionalCancelCallback() {
                console.log("cancel")
            }

        })
    },
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
function WFEditFormatter(cellvalue, options, rowObject) {
    thisCellVal = '<a href="/ApprovalWorkflows/edit/' + rowObject.Id + '" title="Edit" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Edit</a>';
    thisCellVal = thisCellVal + '<a href="/ApprovalWorkflows/Hide/' + rowObject.Id + '" title="Hide Workflow" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Hide</a>';
    return thisCellVal;
}
$('#search').click(function () {

    jQuery("#jqGrid").setGridParam({
        page: 1
    }).trigger("reloadGrid");

});
