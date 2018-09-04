var Msr = Msr || {};

Msr.PendingApprovalsGrid = Msr.PendingApprovalsGrid ||
    {
        SetUpGrid: function (returnUrl, itemType) {

            $.jgrid.defaults.styleUI = 'Bootstrap';

            $("#jqGrid").jqGrid({
                url: 'PendingApproval/PendingApprovalsData',
                mtype: "GET",
                styleUI: 'Bootstrap',
                datatype: "json",
                colModel: [
                    {
                        label: 'Object Id',
                        name: 'ObjectId',
                        index: 'ObjectId',
                        key: true,
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: true, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 100,
                        align: 'left'
                    },
                    {
                        label: "Item Type",
                        name: 'ItemType',
                        index: 'ItemType',
                        colmenu: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { defaultValue: itemType },
                        width: 200,
                        align: 'left'
                    },
                    {
                        label: "Item Name",
                        name: 'ItemName',
                        index: 'ItemName',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left',
                        width: 300,
                        formatter: itemNameFormatter,
                        hidedlg: false
                    },
                    {
                        label: 'Item #',
                        name: 'ItemNumber',
                        index: 'ItemNumber',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Workflow Name',
                        name: 'WfName',
                        index: 'WfName',
                        colmenu: false,
                        width: 150,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                   
                    {
                        label: 'Workflow Group',
                        name: 'GroupName',
                        index: 'GroupName',
                        colmenu: false,
                        width: 150,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Workflow Initiator',
                        name: 'Initiator',
                        index: 'Initiator',
                        colmenu: false,
                        width: 150,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Status',
                        name: 'Status',
                        index: 'Status',
                        colmenu: false,
                        width: 200,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    { name: 'Actions', index: 'ID', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: approvalEditFormatter, width: 200, align: 'center' }
                ],

                viewrecords: true, // show the current page, data rang and total records on the toolbar
                rowNum: 10, rowList: [10, 20, 50, 100],
                loadonce: false, // this is just for the demo
                pager: "#jqGridPager",
                height: 'auto',
                gridview: true,
                sortname: 'DateStarted',
                sortable: true,
                sortorder: 'asc',
                cellEdit: true,
                cellsubmit: 'clientArray',
                editurl: 'clientArray',
                autowidth: true,
                colMenu: true,
                gridComplete: function () {
                    Msr.JqGridCommon.UnLockWorkflow("/PendingApproval?itemType=" + itemType);
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

            setTimeout(function () {
                $('#gs_ItemType').val(itemType);
                $("#jqGrid")[0].triggerToolbar();
            }, 300);


            function approvalEditFormatter(cellvalue, options, rowObject) {

                return Msr.PendingApprovalsGrid.ActionLinks(cellvalue, options, rowObject, returnUrl);
            }
            function itemNameFormatter(cellvalue, options, rowObject) {
                var itemName = 'Approving Changes to approval_' + rowObject.ItemType + ' Called ' + rowObject.ItemName + " " + rowObject.Revision;
                return itemName;
            }

            $('#search').click(function () {

                jQuery("#jqGrid").setGridParam({
                    page: 1
                }).trigger("reloadGrid");

            });
    },

        SetUpProductGrid: function (returnUrl, itemType) {
            $.jgrid.defaults.styleUI = 'Bootstrap';

            $("#jqProductGrid").jqGrid({
                url: 'PendingApproval/PendingApprovalsProductData',
                mtype: "GET",
                styleUI: 'Bootstrap',
                datatype: "json",
                colModel: [
                    {
                        label: 'Database Id',
                        name: 'ObjectId',
                        index: 'ObjectId',
                        key: true,
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 100,
                        align: 'left'
                    },
                    {
                        label: "Item Type",
                        name: 'ItemType',
                        index: 'ItemType',
                        colmenu: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 200,
                        align: 'left'
                    },
                    {
                        label: "Item Name",
                        name: 'ItemName',
                        index: 'ItemName',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left',
                        width: 300,
                        formatter: itemNameFormatter,
                        hidedlg: false
                    },
                    {
                        label: 'Item #',
                        name: 'ItemNumber',
                        index: 'ItemNumber',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Workflow Name',
                        name: 'WfName',
                        index: 'WfName',
                        colmenu: false,
                        width: 150,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Workflow Group',
                        name: 'GroupName',
                        index: 'GroupName',
                        colmenu: false,
                        width: 150,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Workflow Initiator',
                        name: 'Initiator',
                        index: 'Initiator',
                        colmenu: false,
                        width: 150,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    {
                        label: 'Status',
                        name: 'Status',
                        index: 'Status',
                        colmenu: false,
                        width: 200,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    { name: 'Actions', index: 'ID', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: approvalEditFormatter, width: 200, align: 'center' }
                ],

                viewrecords: true, // show the current page, data rang and total records on the toolbar
                rowNum: 10, rowList: [10, 20, 50, 100],
                loadonce: false, // this is just for the demo
                pager: "#jqGridProductPager",
                height: 'auto',
                gridview: true,
                sortname: 'DateStarted',
                sortable: true,
                sortorder: 'asc',
                cellEdit: true,
                cellsubmit: 'clientArray',
                editurl: 'clientArray',
                autowidth: true,
                colMenu: true,
                gridComplete: function () {
                    Msr.JqGridCommon.UnLockWorkflow("/PendingApproval?itemType=" + itemType);
                }
            });
            $('#jqProductGrid').navGrid("#jqGridProductPager", {
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
            $('#jqProductGrid').jqGrid('filterToolbar', {
                stringResult: true,
                searchOnEnter: true,
                searchOperators: true
            });

            function approvalEditFormatter(cellvalue, options, rowObject) {

                return Msr.PendingApprovalsGrid.ActionLinks(cellvalue, options, rowObject, returnUrl);

            }
            function itemNameFormatter(cellvalue, options, rowObject) {
                var itemName = 'Approving Changes to approval_' + rowObject.ItemType + ' Called ' + '<a href="">' + rowObject.ItemName+" "+  rowObject.Revision + '</a>';
                return itemName;
            }

            $('#search').click(function () {

                jQuery("#jqProductGrid").setGridParam({
                    page: 1
                }).trigger("reloadGrid");

            });


        }

    , ActionLinks: function ActionLinks(cellvalue, options, rowObject, returnUrl) {
            var urlApprove;
            var approvalWorkflows;
            var deleteworkflow;
            var buttonWorkflowLeft;
            var buttonWorkflowRight;

            if (rowObject.Status === 'APPROVED') {
                var urlDelete = '/workflow/Denied?objId=' + rowObject.ObjectId + '&WfsId=' + rowObject.WfsId + '&WfStageId=' + rowObject.WfStageId + '&WfGroupId=' + rowObject.WfGroupId + '&returnUrl=' + returnUrl;
                urlApprove = '/workflow/submit?objId=' + rowObject.ObjectId + '&returnUrl=' + returnUrl;
                approvalWorkflows = '<a href="' + urlApprove + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-success" title="Approve this item" style="margin:2px;font-size: .8em;"><i class="fa fa-smile-o fa-2x" aria-hidden="true"></i></a>';
                deleteworkflow = '<a href="' + urlDelete + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-danger" title="Deny this item" style="margin:2px;font-size: .8em;"><i class="fa fa-frown-o fa-2x" aria-hidden="true"></i></a>';

                return approvalWorkflows + deleteworkflow;
            }

            if (rowObject.NotificationType === 'CREATING') {
                urlApprove = '/workflow/submit?objId=' + rowObject.ObjectId + '&returnUrl=' + returnUrl;

                buttonWorkflowLeft = '<a href="' + urlApprove + '" data-call-back-name="' + rowObject.Name + '"  data-call-back-id="' + rowObject.ObjectId + '" class="btn btn-xs btn-success unlock" title="Cancel Creation. Edit will be lost" style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-left"></i></a>';

                buttonWorkflowRight = '<a href="' + urlApprove + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-success" title="Proceed to approval workflow for release." style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-right"></i></a>';

                return buttonWorkflowLeft + buttonWorkflowRight;
            }
        }
    }