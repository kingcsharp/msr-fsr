
$.jgrid.defaults.responsive = true;

function toggleInstructions(theID, action) {

    var addButton = '#add-button' + theID;
    var textareaDiv = '#instruction' + theID;
    $(addButton).toggleClass('hidden show');
    $(textareaDiv).toggleClass('hidden show');

    if (action === 1) {

        $.ajax({
            type: "POST",
            data: { message: $('#note-' + theID).val() },
            url: '/wip/AddInstruction?id=' + theID,
            dataType: 'json',
            success: function (data) {
                $('#note-' + theID).val('')
                location.reload();
            },
            error: function () {

            }
        });
    }

}

$(document).ready(function () {


    $('#search').click(function () {

        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");

    });

    $("#jqGrid").jqGrid({
        url: '/wip/EngineeringData',
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        postData: {
            FromDate: function () { return $('#from-date').val(); },
            ToDate: function () { return $('#to-date').val(); }
        },
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
                label: 'Supplier',
                name: 'SupplierName',
                index: 'SupplierName',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 90,
                align: 'center'
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
                hidedlg: true
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
                    value: "All:[All];ACCEPTED:In Progress;PENDING_PARENT_ACCEPTANCE,REQUESTED:Waiting to Start;CLOSED,FINISHED:Completed",
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
                formatter: dispositionFormatter,
            },


        ],
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPager",
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

            var fillId = $('#jqGrid').jqGrid('getCell', rowid, 'FillId');

            var purchaseItemId = $('#jqGrid').jqGrid('getCell', rowid, 'PurchaseItemId');

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
            $("#jqGrid").jqGrid().trigger('reloadGrid');
            console.log('afterSaveCell : ' + cellname);
        },
        gridComplete: function () {
            $('div.meter').each(function (index) {
                var progVal = parseFloat($(this).text()).toFixed(2);
                var statClass = 'danger';
                if (progVal > 25) { statClass = 'warning'; }
                if (progVal > 50) { statClass = 'info'; }
                if (progVal > 75) { statClass = 'success'; }
                $(this).replaceWith(
                    '<div class = "progress">' +
                    '<div class = "progress-bar progress-bar-' + statClass + '" role = "progressbar" aria-valuenow = "' + progVal + '" ' +
                    'aria-valuemin = "0" aria-valuemax = "100" style = "width: ' + progVal + '%;"> ' +
                    '<span>' + progVal + '%</span>' +
                    '</div>' +
                    '</div>'
                );
            });
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
        //event.stopPropagation();
        event.preventDefault();
        // Do something
    });

    function initDateEdit(elem, options) {
        //console.log(options);
        var StartDate = $('#jqGrid').jqGrid('getCell', options.rowId, 'StDate');
        console.log(StartDate);
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

        if (cellvalue !== 'NULL' && cellvalue !== null && cellvalue !== '') {
            var thisVal = cellvalue;
            thisCellVal = '<strong>' + thisVal + '</strong><br /> <div class="meter">' + rowObject.TimeComplete + '</div> <div class="meter">' + rowObject.PercComplete + '</div> ';
        }
        else {
            thisCellVal = rowObject.Status;
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

        InstructionsCellVal = notes + '<br/><button id="add-button' + rowObject.FillId + '" onclick="toggleInstructions(' + rowObject.FillId + ',0)" class="btn support-btn btn-xs btn-danger show">Add Instructions</button><div id="instruction' + rowObject.FillId + '" class="hidden" ><textarea style="width: 95%;" rows=4 placeholder="Add disposition Instructions" id="note-' + rowObject.FillId + '"></textarea><button class="btn support-btn btn-xs btn-primary" onclick="toggleInstructions(' + rowObject.FillId + ',1)">Save</button>';
        thisCellVal = (rowObject.HasNcr === 1) ? InstructionsCellVal : 'N/A';
        return thisCellVal;
    }

    function workItemFormatter(cellvalue, options, rowObject) {

        var thisCellVal = '<span class="badge info"><a style="color:white;" href="/wip/details/' + rowObject.FillId + '">' + cellvalue + '</a><span>';

        return thisCellVal;
    }
});



