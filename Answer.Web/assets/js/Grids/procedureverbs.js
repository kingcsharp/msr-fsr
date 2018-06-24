var Msr = Msr || {};

Msr.ProcedureVerbsGrid = Msr.ProcedureVerbsGrid ||
{
    loadProcedureVerbsGrid: function(url, returnUrl) {
        $.jgrid.defaults.styleUI = 'Bootstrap';

        $("#jqGrid").jqGrid({
            url: url,
            mtype: "GET",
            styleUI: 'Bootstrap',
            datatype: "json",
            colModel: [
                {
                    label: '#',
                    name: 'Root',
                    index: 'Root',
                    key: true,
                    colmenu: false,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 100,
                    align: 'left'
                },
                {
                    label: 'Name',
                    name: 'Name',
                    index: 'Name',
                    colmenu: true,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 250,
                    align: 'left'
                },
                {
                    label: 'Type',
                    name: 'VerbTypeName',
                    index: 'VerbTypeName',
                    colmenu: false,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: true,
                        grouping: false,
                        freeze: false
                    },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left',
                    width: 100,
                    hidedlg: false
                },
                {
                    label: 'Revision',
                    name: 'Revision',
                    index: 'Revision',
                    colmenu: false,
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'left'
                },
                {
                    label: 'Approved Status',
                    name: 'Status',
                    index: 'Status',
                    colmenu: false,
                    stype: "select",
                    coloptions: {
                        sorting: false,
                        columns: true,
                        filtering: false,
                        seraching: false,
                        grouping: false,
                        freeze: false
                    },
                    searchoptions: {
                        value:
                            ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING:Creating or Approved;CREATING, DENIED: Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete"
                    },
                    align: 'center'
                },
                {
                    name: 'Actions',
                    index: 'ID',
                    key: true,
                    search: false,
                    hidden: false,
                    colmenu: false,
                    editable: false,
                    formatter: prodecureVerbsEditFormatter,
                    width: 100,
                    align: 'center'
                }
            ],

            viewrecords: true, // show the current page, data rang and total records on the toolbar
            rowNum: 10,
            rowList: [10, 20, 50, 100],
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
            gridComplete: function() {
                Msr.JqGridCommon.SetupGridLock("/ProcedureVerbs/Edit/");
                Msr.JqGridCommon.UnLockWorkflow(returnUrl);
            }
        });
        $('#jqGrid').navGrid("#jqGridPager",
            {
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
        $('#jqGrid').jqGrid('filterToolbar',
            {
                stringResult: true,
                searchOnEnter: true,
                searchOperators: true
            });

        function prodecureVerbsEditFormatter(cellvalue, options, rowObject) {

            var actions =
                Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, returnUrl, '/Actualparts/Edit/');

            return actions;

        }

        $('#search').click(function() {
            jQuery("#jqGrid").setGridParam({
                page: 1
            }).trigger("reloadGrid");

        });
    }
}