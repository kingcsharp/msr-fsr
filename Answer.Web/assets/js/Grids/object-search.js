var Msr = Msr || {};

Msr.SearhResult = Msr.SearhResult ||
    {
        LoadSearchResultsGrid: function (url) {

            $.jgrid.defaults.responsive = true;

            var filterList =
                     ':[All];' +
                    'Procedures: Procedures;' +
                    'Documents:Documents;' +
                    'Monitors:Monitors;' +
                    'Parts:Parts;' +
                    'PartsTypes:PartsTypes;' +
                    'ActualParts:ActualParts;' +
                    'Templates:Templates;' +
                    'Companies:Companies;' +
                    'Steps:Steps';

            $("#jqGrid").jqGrid({
                url: url,
                mtype: "GET",
                styleUI: 'Bootstrap',
                datatype: "json",
                colModel: [
                    {
                        label: 'Item Name',
                        name: 'Name',
                        index: 'Name',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 90,
                        align: 'center'
                    },
                    {
                        label: 'Description',
                        name: 'Description',
                        index: 'Description',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 90,
                        align: 'center'
                    },
                    {
                        label: 'Last Updated',
                        name: 'CreateDate',
                        index: 'CreateDate',
                        colmenu: false,
                        sorttype: 'date',
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        formatter: 'date',
                        formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                        width: 90,
                        align: 'center'
                    },
                    {
                        label: 'Item Type',
                        name: 'ItemType',
                        index: 'ItemType',
                        colmenu: false,
                        stype: "select",
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { value: filterList },
                        align: 'center'
                    },
                    { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: workItemFormatter, width: 200, align: 'center' }

                ],
                viewrecords: true, // show the current page, data rang and total records on the toolbar
                rowNum: 10, rowList: [10, 20, 50, 100],
                loadonce: false, // this is just for the demo
                pager: "#jqGridPager",
                height: 'auto',
                gridview: true,
                sortname: 'Name',
                sortable: true,
                sortorder: 'Desc',
                cellEdit: true,
                cellsubmit: 'clientArray',
                editurl: 'clientArray',
                autowidth: true,
                colMenu: true,
                key: true,
                ajaxCellOptions: {},

                gridComplete: function () {

                }
            });

            $('#jqGrid').navGrid("#jqGridPager", {
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
            $('#jqGrid').jqGrid('filterToolbar', {
                stringResult: true,
                searchOnEnter: true,
                searchOperators: true
            });


            $("#jqGrid").jqGrid().trigger('reloadGrid');

            $("#jqGrid").tooltip();

            $('a.colmenu').click(function (event) {
                event.preventDefault();
            });

   
        }
    }

function workItemFormatter(cellvalue, options, rowObject) {
    var url='';

    if (rowObject.ItemType === 'Procedures') {
        url = '/Procedures/view/' + rowObject.ObjectId;
    }
    else if (rowObject.ItemType === 'Documents') {
        url = '/Documents/view/' + rowObject.ObjectId;
    }
    else if (rowObject.ItemType === 'Monitors') {
        url = '/Monitors/view/' + rowObject.ObjectId;
    }
    else if (rowObject.ItemType === 'Steps') {
        url = '/wip/details/' + rowObject.ObjectId;
    }

    if (url !== '') {
        return '<span class="btn btn-xs btn-info"><a style="color:white;" href="' + url + '"><i class="fa fa-eye"></i> View Details</a><span>';
    }

    return '<span class="btn btn-xs btn-info"><a style="color:white;" href="#"><i class="fa fa-eye"></i> View Details</a><span>';
}

