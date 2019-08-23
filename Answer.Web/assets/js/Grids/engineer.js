var Msr = Msr || {};

Msr.WipGrid = Msr.WipGrid ||
    {
        GetReturnUrl: function () {
            return "/Wip";
        },
        GetGridId: function () {
            return "jq-grid-wip";
        },
        LoadWipGrid: function (url, locations) {

            $.jgrid.defaults.responsive = true;
            $("#" + Msr.WipGrid.GetGridId()).jqGrid({
                url: url,
                mtype: "GET",
                styleUI: 'Bootstrap',
                datatype: "local",
                colModel: [
                    { name: 'Id', index: 'Id', hidden: true, key: true, editable: false, editrules: { edithidden: true } },
                    { name: 'PurchaseItemId', index: 'PurchaseItemId', hidden: true, editable: false, editrules: { edithidden: true } },
                    { name: 'FillId', index: 'FillId', width: 60, align: 'center', hidden: true, edittype: 'text', editable: true, editrules: { edithidden: true } },
                    {
                        label: 'WO Item #',
                        name: 'WoItem',
                        index: 'WoItem',
                        colmenu: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        //width: '*',
                        align: 'center',
                        formatter: workItemFormatter
                    },
                    {
                        label: 'Customer',
                        name: 'CustomerName',
                        index: 'CustomerName',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 90,
                        align: 'center'
                    },
                    {
                        label: 'MSR-FSR Facility',
                        name: 'LocationName',
                        index: 'LocationName',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        align: 'center',
                        stype: 'select',
                        multiselect: true,
                        searchoptions: {
                            sopt: ['eq'],
                            value: locations,
                            dataInit: function (elem) {
                                Msr.JqGridCommon.DataInitBootstrapMultiselect(elem);
                            }
                        }
                    },
                    {
                        label: 'Serial',
                        name: 'Serial',
                        index: 'Serial',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'center'
                    },
                    {
                        label: 'PO #',
                        name: 'CustPurchNum',
                        index: 'CustPurchNum',
                        colmenu: false,
                        editable: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'center'
                    },
                    {
                        label: 'Qty',
                        name: 'FillQty',
                        index: 'FillQty',
                        width: 50,
                        colmenu: false,
                        editable: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        align: 'center'
                    },
                    {
                        label: 'Start Date',
                        name: 'StDate',
                        index: 'StDate',
                        colmenu: false,
                        sorttype: 'date',
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: {
                            searchOperMenu: false,
                            sopt: ['eq', 'gt', 'lt', 'ge', 'le'],
                            dataInit: function (elem) {
                                Msr.JqGridCommon.DataInitDatePicker(elem);
                            }
                        },
                        formatter: 'date',
                        formatoptions: { srcformat: 'm/d/Y', newformat: 'm/d/Y' },
                        width: 90,
                        align: 'center'
                    },
                    {
                        label: 'Due Date',
                        name: 'DueDate',
                        index: 'DueDate',
                        colmenu: false,
                        editable: true,
                        width: 110,
                        sorttype: 'date',
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        // edittype: 'text', editable: true, editrules: { edithidden: true }
                        searchoptions: {
                            searchOperMenu: false,
                            sopt: ['eq', 'gt', 'lt', 'ge', 'le'],
                            dataInit: function (elem) {
                                Msr.JqGridCommon.DataInitDatePicker(elem);
                            }
                        },
                        formatter: 'date',
                        formatoptions: { srcformat: 'm/d/Y', newformat: 'm/d/Y' },
                        editoptions: { dataInit: initDateEdit, readonly: 'readonly' },
                        align: 'center'
                    },
                    {
                        label: 'Product Name',
                        name: 'ProductName',
                        index: 'ProductName',
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 210,
                        align: 'left'
                    },
                    {
                        label: 'Procedure',
                        name: 'ProcName',
                        index: 'ProcName',
                        colmenu: false, width: 140,
                        align: 'left'
                    },
                    {
                        label: 'Current Step/Status',
                        name: 'CurStepText',
                        index: 'CurStepText',
                        colmenu: false,
                        coloptions:
                        {
                            sorting: false,
                            columns: true,
                            filtering: false,
                            seraching: false,
                            grouping: false,
                            freeze: false
                        },
                        stype: "select",
                        multiselect: true,
                        searchoptions: {
                            sopt: ['eq'],
                            value: "ACCEPTED:In Progress;PENDING_PARENT_ACCEPTANCE,REQUESTED:Waiting to Start;CLOSED,FINISHED:Completed",
                            dataInit: function (elem) {
                                Msr.JqGridCommon.DataInitBootstrapMultiselect(elem);
                            }
                        },
                        formatter: currentStepFormatter,
                        align: 'center'
                    },
                    {
                        label: 'Disposition',
                        name: 'ActualPartId',
                        index: 'ActualPartId',
                        id: 'ActualPartId',
                        colmenu: false,
                        editable: false,
                        edittype: "textarea",
                        width: 180,
                        align: 'center',
                        formatter: dispositionFormatter
                    }
                ],
                viewrecords: true, // show the current page, data rang and total records on the toolbar
                rowNum: 10, rowList: [10, 20, 50, 100],
                loadonce: false, // this is just for the demo
                pager: "#jq-grid-pager",
                height: 'auto',
                gridview: true,
                sortname: 'DueDate',
                sortable: true,
                sortorder: 'Desc',
                cellEdit: true,
                cellsubmit: 'clientArray',
                editurl: 'clientArray',
                //shrinkToFit:true,
                autowidth: true,
                colMenu: true,
                key: true,
                ajaxCellOptions: {},
                beforeSaveCell: function (rowid, cellname, value, iRow, iCol) {

                    var fillId = $("#" + Msr.WipGrid.GetGridId()).jqGrid('getCell', rowid, 'FillId');

                    var purchaseItemId = $("#" + Msr.WipGrid.GetGridId()).jqGrid('getCell', rowid, 'PurchaseItemId');

                    if (cellname === 'DueDate') {
                        //value = $("#" + Msr.WipGrid.GetGridId()).jqGrid('getCell', rowid, cellname);
                        value = moment(value, 'MM/DD/YYYY').format();
                    }

                    var options = {
                        FillId: fillId,
                        PurchaseItemId: purchaseItemId,
                        ColumnName: cellname,
                        Value: value
                    };

                    $.ajax({
                        type: 'POST',
                        url: '/Wip/EditOrderItemInline',
                        data: options,
                        dataType: 'JSON',
                        success: function (resultData) {
                            $("#" + Msr.WipGrid.GetGridId()).jqGrid().trigger('reloadGrid');
                            console.log("row with rowid=" + rowid + " is successfuly modified.");
                        }
                    });
                },
                afterSaveCell: function (rowid, cellname, value, iRow, iCol) {
                    //$("#" + Msr.WipGrid.GetGridId()).jqGrid().trigger('reloadGrid');
                },
                gridComplete: function () {
                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.WipGrid.GetGridId());
                    $('div.meter').each(function (index) {
                        var tooltiptime = '';
                        var type = $(this).data('type');
                        var progVal = parseFloat($(this).text()).toFixed(2);
                        var statClass = 'danger';
                        if (progVal > 25) {
                            statClass = 'warning';
                        }
                        if (progVal > 50) {
                            statClass = 'info';
                        }
                        if (progVal > 75) {
                            statClass = 'success';
                        }
                        if (type === 'pre-complete-text') {
                            tooltiptime = $(this).data('tooltip-pre');
                        } else {
                            tooltiptime = $(this).data('tooltip-time');
                        }
                        $(this).replaceWith(
                            '<div class = "progress" title="' + tooltiptime + '">' +
                            '<div class = "progress-bar progress-bar-' + statClass +
                            '" role = "progressbar" aria-valuenow = "' + progVal + '" '
                            + 'aria-valuemin = "0" aria-valuemax = "100" style = "width: ' +
                            progVal + '%;"> ' +
                            '<span>' + progVal + '%</span>' +
                            '</div>' +
                            '</div>'
                        )
                    });

                    var isGridDone = false;
                    var time = new Date().getTime();
                    var gridComplete = setInterval(function () {
                        var maxWidth = 150;
                        $('[aria-describedby*="jq-grid-wip_WoItem"] span').each(function (index, elem) {
                            maxWidth = $(elem).width() > maxWidth ? $(elem).width() : maxWidth;
                            isGridDone = true;
                        });
                        if (isGridDone) {
                            $("#" + Msr.WipGrid.GetGridId()).jqGrid('resizeColumn', 'WoItem', maxWidth + 25, true);
                            clearInterval(gridComplete);
                        }
                        //after 50 secs do not execute function anymore.
                        if (time > time + 1000 * 50) {
                            clearInterval(gridComplete);
                        }
                    }, 10);
                    $('[data-toggle="tooltip"]').tooltip();
                },
                beforeRequest: function () {
                    Msr.JqGridCommon.ModifyMultiselectData.call(this);
                    Msr.JqGridCommon.ModifySearchingFilter.call(this, ',', 'LocationName');
                }
            });

            Msr.JqGridCommon.BindGridEvents(Msr.WipGrid.GetGridId());

            $('a.colmenu').click(function (event) {
                //event.stopPropagation();
                event.preventDefault();
                // Do something
            });
        }
    }


function initDateEdit(el, options) {
    Msr.JqGridCommon.DataInitDatePicker(el, 'mm/dd/yyyy');
};

function currentStepFormatter(cellvalue, options, rowObject) {
    var thisCellVal = '';

    if (rowObject.Status !== 'ACCEPTED') {
        if (rowObject.Status === 'REQUESTED') {
            thisCellVal = 'Waiting Start';
        } else {
            thisCellVal = 'Completed';
        }
    } else {
        if (rowObject.PercComplete !== '') {
            if (rowObject.CurStepText != null) {
                thisCellVal = '<strong>' + rowObject.CurStepText + '</strong><br /> <div class="meter"  data-tooltip-pre="' + rowObject.PercCompletedText + '" data-type=' + "pre-complete-text" + '>' + rowObject.PercComplete +

                    '</div> <div class="meter"  data-tooltip-time="' + rowObject.TimeCompletedText + '">' + rowObject.TimeComplete + '</div>';
            } else {
                thisCellVal = '<div class="meter" data-tooltip-pre="' + rowObject.PercCompletedText + '">' + rowObject.PercComplete + '</div> <div class="meter" data-tooltip-time="' + rowObject.TimeCompletedText + '">' + rowObject.TimeComplete + '</div> ';
            }
        }
    }

    return thisCellVal;
}

function dispositionFormatter(cellvalue, options, rowObject) {

    var notes = "";

    if (rowObject.Notes !== null && rowObject.Notes !== '') {

        var notesList = JSON.parse(rowObject.Notes);

        $(notesList).each(function (index, value) {
            notes = notes + '<div class="disp-label">' + value.Date + ' ' + value.Name + '</div>';
            notes = notes + '<div class="disp-instruction"><div class="disp-text">' + value.Message + '</div></div>';
        });
    }

    return notes;
}

function workItemFormatter(cellvalue, options, rowObject) {
    var hasNCR = (rowObject.HasNcr > 0) ? 'warning' : 'info';
    var hasNCRTooltip = (rowObject.HasNcr > 0) ? 'data-placement="right" data-toggle="tooltip" title="This WO has an NCR Reported"' : '';
    var thisCellVal = '<span class="badge '+ hasNCR + '" '+ hasNCRTooltip + '><a style="color:white;" href="/wip/details/' + rowObject.FillId + '">' + cellvalue + '</a><span>';
    return thisCellVal;
}
