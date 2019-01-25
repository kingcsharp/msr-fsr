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
                    { name: 'FillId', index: 'FillId', width: 60, align: 'center', hidden: true, edittype: 'text', editable: true, editrules: { edithidden: true } },
                    {
                        label: 'WO Item #',
                        name: 'WoItem',
                        index: 'WoItem',
                        key: true,
                        colmenu: false,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 144,
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
                            value: 'HiddenOption:;' + locations,
                            attr: { multiple: 'multiple', size: 4 },
                            dataInit: function (elem) {
                                Msr.JqGridCommon.DataInitMultiselect(elem);
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
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        formatter: 'date',
                        formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                        width: 90,
                        align: 'center'
                    },
                    {
                        label: 'Due Date',
                        name: 'DueDate',
                        index: 'DueDate',
                        colmenu: false,
                        editable: true,
                        sorttype: 'date',
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        formatter: 'date',
                        formatoptions: { srcformat: "m/d/Y", newformat: "m/d/Y" },
                        editoptions: { dataInit: initDateEdit, readonly: 'readonly' },
                        width: 90,
                        align: 'center'
                    },
                    {
                        label: 'Product Name',
                        name: 'ProductName',
                        index: 'ProductName',
                        colmenu: true,
                        coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                        searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                        width: 250,
                        align: 'left',
                    },
                    {
                        label: 'Procedure',
                        name: 'ProcName',
                        index: 'ProcName',
                        colmenu: false, width: 180,
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
                        searchoptions: {
                            value: "All:[All];REQUESTED,ACCEPTED:In Progress;PENDING_PARENT_ACCEPTANCE,REQUESTED:Waiting to Start;CLOSED,FINISHED:Completed",
                            defaultValue: 'In Progress'
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
                autowidth: true,
                colMenu: true,
                key: true,
                ajaxCellOptions: {},
                beforeSaveCell: function (rowid, cellname, value, iRow, iCol) {

                    var fillId = $("#" + Msr.WipGrid.GetGridId()).jqGrid('getCell', rowid, 'FillId');

                    var purchaseItemId = $("#" + Msr.WipGrid.GetGridId()).jqGrid('getCell', rowid, 'PurchaseItemId');

                    var options = {
                        FillId: fillId,
                        PurchaseItemId: purchaseItemId,
                        ColumnName: cellname,
                        Value: value
                    }

                    $.ajax({
                        type: 'POST',
                        url: '/Wip/EditOrderItemInline',
                        data: options,
                        dataType: 'JSON',
                        success: function (resultData) {
                            console.log("row with rowid=" + rowid + " is successfuly modified.")
                        }
                    });
                },
                afterSaveCell: function (rowid, cellname, value, iRow, iCol) {
                    $("#" + Msr.WipGrid.GetGridId()).jqGrid().trigger('reloadGrid');
                },
                gridComplete: function () {
                    Msr.JqGridCommon.TriggerSaveLoadGridState(Msr.WipGrid.GetGridId());

                    $('div.meter').each(function (index) {
                        var tooltiptime = '';
                        var type = $(this).data('type');
                        var progVal = parseFloat($(this).text()).toFixed(2);
                        var statClass = 'danger';
                        if (progVal > 25) { statClass = 'warning'; }
                        if (progVal > 50) { statClass = 'info'; }
                        if (progVal > 75) { statClass = 'success'; }
                        if (type === 'pre-complete-text') {
                            tooltiptime = $(this).data('tooltip-pre');
                        }
                        else {
                            tooltiptime = $(this).data('tooltip-time');
                        }
                        $(this).replaceWith(
                            '<div class = "progress" title="' + tooltiptime + '">' +
                            '<div class = "progress-bar progress-bar-' + statClass + '" role = "progressbar" aria-valuenow = "' + progVal + '" ' +
                            'aria-valuemin = "0" aria-valuemax = "100" style = "width: ' + progVal + '%;"> ' +
                            '<span>' + progVal + '%</span>' +
                            '</div>' +
                            '</div>'
                        );
                    });

                    $('.ui-multiselect-checkboxes li:first-child').hide();

                },
                beforeRequest: function () {

                    Msr.JqGridCommon.ModifySearchingFilter.call(this, ',', 'LocationName');
                }
            });

            Msr.JqGridCommon.BindGridEvents(Msr.WipGrid.GetGridId());

            $("#" + Msr.WipGrid.GetGridId()).tooltip();

            $('a.colmenu').click(function (event) {
                //event.stopPropagation();
                event.preventDefault();
                // Do something
            });


        }
    }


function initDateEdit(elem, options) {
    $(elem).datepicker({
        maxDate: "10/27/2015",
        dateFormat: "mm/dd/yy",
        autoSize: true,
        changeYear: true,
        changeMonth: true,
        showButtonPanel: true,
        showWeek: true
    });

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

    var thisCellVal = '<span class="badge info"><a style="color:white;" href="/wip/details/' + rowObject.FillId + '">' + cellvalue + '</a><span>';

    return thisCellVal;
}
