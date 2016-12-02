
$.jgrid.defaults.responsive = true;

function toggleInstructions (theID,action) {
	
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

		$("#jqGrid").jqGrid({
		    url: '/wip/EngineeringData',
		    mtype: "GET",
		    styleUI: 'Bootstrap',
        datatype: "json",
		    colNames:['WO Item #','Supplier','Serial #','PO #', 'Qty', 'Start Date','Due Date', 'Product Name', 'Procedure', 'Current Step/Status', 'Supporting Info', 'Disposition'],
		 colModel: [
		     {
		         name: 'PurchaseItemId',
		         index: 'PurchaseItemId',
		         key: true,
		         colmenu: false,
		         coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         width: 100,
		         align: 'center'
		     },
		     {
		         name: 'SupplierName',
		         index: 'SupplierName',
		         colmenu: false,
		         coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         width: 90,
		         align: 'center'
		     },
		     {
		         name: 'Serial',
		         index: 'Serial',
		         colmenu: false,
		         coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         align: 'center'
		     },
		     {
		         name: 'CustPurchNum',
		         index: 'CustPurchNum',
		         colmenu: false,
		         coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         align: 'center'
		     },
		     {
		         name: 'Qty',
		         index: 'Qty',
		         width: 50,
		         colmenu: false,
		         coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         align: 'center'
		     },
		     {
		         name: 'St_Date',
		         index: 'St_Date',
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
		         name: 'DueDate',
		         index: 'DueDate',
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
		         name: 'ProductName',
		         index: 'ProductName',
		         colmenu: true,
		         coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         width: 250,
		         align: 'left',
                 hidedlg:true
		     },
		     {
		         name: 'ProcName',
		         index: 'ProcName',
		         colmenu: false, width: 180,
		         align: 'left'
		     },
		     {
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
		         formatter: currentStepFormatter,
		         align: 'center'
		     },
		     {
		         name: 'PurchaseId',
		         index: 'PurchaseId',
		         colmenu: false,
		         coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
		         formatter: supportingInfoFormatter,
		         align: 'center'
		     },
		     {
		         name: 'ActualPartId',
		         index: 'ActualPartId',
		         id: 'ActualPartId',
		         colmenu: false,
		         editable: false,
		         edittype: "textarea",
		         width: 180,
		         align: 'center',
		         formatter: actionsFormatter,
		         align: 'center'
		     },
		],
		viewrecords: true, // show the current page, data rang and total records on the toolbar
		rowNum: 10,
		loadonce: false, // this is just for the demo
		pager: "#jqGridPager",
		height: 'auto',
		gridview: true,
		sortname: 'PurchaseItemId',
		sortable: true,
		sortorder: 'asc',
		cellEdit: true,
        cellsubmit : 'clientArray',
        editurl: 'clientArray',
	    autowidth: true,
	    colMenu : true,
	    gridComplete: function() {
            $('div.meter').each(function(index) {
                var progVal = parseFloat($(this).text()).toFixed(2);
                var statClass = 'danger';
                if (progVal > 25) {  statClass = 'warning'; }
                if (progVal > 50) {  statClass = 'info'; }
                if (progVal > 75) {  statClass = 'success'; }
                $(this).replaceWith(
                    '<div class = "progress">' +
                        '<div class = "progress-bar progress-bar-'+ statClass + '" role = "progressbar" aria-valuenow = "' + progVal + '" ' +
                            'aria-valuemin = "0" aria-valuemax = "100" style = "width: ' + progVal + '%;"> ' +
                            '<span>' + progVal + '%</span>' +
                        '</div>' +
                    '</div>'
                );
            });

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
    $('#jqGrid').jqGrid('filterToolbar',{
		stringResult: true,
		searchOnEnter: true,
		searchOperators : true
	});
	
	
	$("#jqGrid").jqGrid().trigger('reloadGrid');
	
	$("#jqGrid").tooltip();


    $('#ncrModal').on('show.bs.modal', function(event) {
        var button = $(event.relatedTarget);
        var id = button.data('id');
        var modal = $(this);

        $.ajax({
            type: "GET",
            url: '/wip/getncrmodel?id=' + id,
            dataType: 'html',
            success: function(data) {
                modal.find('.modal-body').html(data);
            },
            error: function() {

            }
        });
    });

    


	$('#imageModal').on('show.bs.modal', function (event) {
	    var button = $(event.relatedTarget) // Button that triggered the modal
	    var id = button.data('id') // Extract info from data-* attributes
	    var modal = $(this)

	    $.ajax({
	        type: "GET",
	        url: '/wip/GetPhotsModel?id=' + id,
	        dataType: 'html',
	        success: function (data) {
	            modal.find('.modal-body').html(data);
	        },
	        error: function () {

	        }
	    });
	})

	$('#monitorModal').on('show.bs.modal', function (event) {
	    var button = $(event.relatedTarget) // Button that triggered the modal
	    var id = button.data('id'); // Extract info from data-* attributes
	    var modal = $(this);

	    $.ajax({
	        type: "GET",
	        url: '/wip/GetMonitorsModel?id=' + id,
	        dataType: 'html',
	        success: function (data) {
	            modal.find('.modal-body').html(data);
	        },
	        error: function () {

	        }
	    });
	})

    $('#ncrModal').on('hidden.bs.modal', function(event) {
        $(this).data('bs.modal', null);
    });

    $('#monitorModal').on('hidden.bs.modal', function (event) {
        $(this).data('bs.modal', null);
    });

    $('#imageModal').on('hidden.bs.modal', function (event) {
        $(this).data('bs.modal', null);
	});

    $('a.colmenu').click(function( event ) {
      //event.stopPropagation();
      event.preventDefault();
      // Do something
    });
    
	function currentStepFormatter (cellvalue, options, rowObject) {
	    var thisCellVal = '';

	    if (cellvalue !== 'NULL' && cellvalue !== null && cellvalue !== '') {
	        var thisVal = cellvalue;
	        thisCellVal = '<strong>' + thisVal + '</strong><br /> <div class="meter">' + rowObject.TimeComplete + '</div> <div class="meter">' + rowObject.PercComplete + '</div> ';
	    }
        else {
	        thisCellVal = 'Waiting to Start';
	    }

        return thisCellVal;
    }
    
	function supportingInfoFormatter(cellvalue, options, rowObject) {
	    var NcrButton = (rowObject.HasNcr == 1) ? '<button class="btn support-btn btn-xs btn-warning" data-id="' + rowObject.FillId + '" data-toggle="modal"  data-target="#ncrModal" title="View NCR"><i class="fa fa-clipboard"></i>NCR</button>' : '';
	    var FileButton = (rowObject.HasFile == 1) ? '<button class="btn support-btn btn-xs btn-info" data-id="' + rowObject.ActualPartId + '" href="#" data-toggle="modal" data-target="#imageModal" title="View Photos"><i class="fa fa-file-image-o"></i>Photos</button>' : '';
	    var MonitorButton = (rowObject.HasMonitor == 1) ? '<button class="btn support-btn btn-xs btn-success" data-id="' + rowObject.FillId + '" data-toggle="modal" data-target="#monitorModal"  title="View Monitors"><i class="fa fa-bar-chart "></i>Monitors</button>' : '';
        thisCellVal = NcrButton + FileButton + MonitorButton;
        return thisCellVal;
    }
	function actionsFormatter(cellvalue, options, rowObject) {

	    var notes = "";

	    if (rowObject.Notes !== null && rowObject.Notes !== '') {

	        var notesList = JSON.parse(rowObject.Notes);

             
             $(notesList).each(function (index, value) {
                  notes = notes + '<div class="disp-label">' + value.Date + ' ' + value.Name + '</div>';
                  notes = notes + '<div class="disp-instruction"><div class="disp-text">' + value.Message + '</div></div>';
	        });
	        
	    }
	   


	    InstructionsCellVal = notes + '<br/><button id="add-button' + rowObject.FillId + '" onclick="toggleInstructions(' + rowObject.FillId + ',0)" class="btn support-btn btn-xs btn-danger show">Add Instructions</button><div id="instruction' + rowObject.FillId + '" class="hidden" ><textarea style="width: 95%;" rows=4 placeholder="Add disposition Instructions" id="note-' + rowObject.FillId + '"></textarea><button class="btn support-btn btn-xs btn-primary" onclick="toggleInstructions(' + rowObject.FillId + ',1)">Save</button>';
        thisCellVal = (rowObject.HasNcr == 1) ? InstructionsCellVal : 'N/A';
        return thisCellVal;
    }
    
});
