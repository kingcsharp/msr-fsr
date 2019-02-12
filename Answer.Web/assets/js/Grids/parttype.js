var Msr = Msr || {};

Msr.PartTypeGrid = Msr.PartTypeGrid ||
    {
        GetReturnUrl: function () {
            return "/PartTypes";
        },
        GetGridId: function () {
            return "jq-grid-part-type";
        },
        GetGridEditUrl: function () {
            return "/PartTypes/Edit/";
        },
        LoadPartTypesGrid: function (url) {

            $("#" + Msr.PartTypeGrid.GetGridId()).jqGrid({
                url: url,
                mtype: "GET",
                styleUI: 'Bootstrap',
                emptyrecords: 'No records to display',
                datatype: "local",
                colModel: [
                    {
                        label: 'ID',
                        name: 'Root',
                        index: 'Root',
                        key: true,
                        colmenu: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 100,
                        align: 'left'
                    },
                    {
                        label: 'Part Type Name',
                        name: 'Name',
                        index: 'Name',
                        colmenu: false,
                        editable: true, // must set editable to true if you want to make the field editable
                        editrules: { required: true },
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 250,
                        align: 'left'
                    },
                    {
                        label: 'Typically Spare',
                        name: 'Spare',
                        index: 'Spare',
                        colmenu: false,
                        editable: true,
                        stype: "select",
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                        searchoptions: { value: ":[All];PTSPARE_NO:Not Typically a Spare Part;PTSPARE_1:L1 - Stock in location with 1 machine;PTSPARE_2:L2 - Stock in location with 10 machine;PTSPARE_3:L3 - Stock in location with 50 machine" },
                        align: 'left',
                        width: 100,
                        hidedlg: false
                    },
                    {
                        label: 'Typically Consumable',
                        name: 'Consumable',
                        index: 'Consumable',
                        colmenu: false,
                        editable: true,
                        stype: "select",
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { value: ":[All];PTCON_NO:Not Consumable Part;PTCON_YES:Consumable Part" },
                        align: 'left'
                    },
                    {
                        label: 'Typically Ordering UNIT',
                        name: 'Unit',
                        index: 'Unit',
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
                        searchoptions: { value: Msr.JqGridCommon.GetStatusFilters() },
                        align: 'left'
                    },
                    {
                        label: 'Checked Out To',
                        name: 'LockedByName',
                        index: 'LockedByName',
                        colmenu: false,
                        editable: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'left'
                    },
                    { name: 'Actions', index: 'ID', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: partTypesEditFormatter, width: 100, align: 'center' }
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
                pager: "#jq-grid-pager",
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

                gridComplete: function () {
                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.PartTypeGrid.GetGridId());
                    Msr.JqGridCommon.SetupGridLock(Msr.PartTypeGrid.GetGridEditUrl());
                    Msr.JqGridCommon.UnLockWorkflow(Msr.PartTypeGrid.GetReturnUrl());
                }
            });
            Msr.JqGridCommon.BindGridEvents(Msr.PartTypeGrid.GetGridId());

            var myEditOptions = {
                keys: true,
                oneditfunc: function (rowresponseid) {
                },
                aftersavefunc: function (rowid, response, options) {
                    $.ajax({
                        type: 'POST',
                        url: '/Parts/Save',
                        data: options,
                        dataType: 'JSON',
                        success: function (resultData) {
                            //alert("Save Complete");
                            console.log("row with rowid=" + rowid + " is successfuly modified.");
                        }
                    });

                }
            };
            $('#' + Msr.PartTypeGrid.GetGridId()).jqGrid('inlineNav', '#jqGridPager', {
                add: false,
                edit: false,
                save: false,
                cancel: false,
                addParams: {
                    position: "afterSelected",
                    addRowParams: myEditOptions
                },
                addedrow: "last",
                editParams: myEditOptions
            });

            function partTypesEditFormatter(cellvalue, options, rowObject) {

                var actions = Msr.JqGridCommon.ActionFormtter(cellvalue, options, rowObject, Msr.PartTypeGrid.GetReturnUrl(), Msr.PartTypeGrid.GetGridEditUrl(),true);

                return actions;


            }
        }
    }