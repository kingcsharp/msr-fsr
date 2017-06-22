
$.jgrid.defaults.responsive = true;

function toggleInstructions (theID) {
	event.preventDefault();
    var addButton = '#add-button' + theID;
    var textareaDiv = '#instruction' + theID;
    $(addButton).toggleClass('hidden show');
    $(textareaDiv).toggleClass('hidden show');
}

$(document).ready(function () {

		$("#jqGrid").jqGrid({
		url: '/assets/data/wip-engineer.json',
        mtype: "GET",
        datatype: "json",
		    colNames:['WO Item #','Supplier','Serial #','PO #', 'Qty', 'Start Date','Due Date', 'Product Name', 'Procedure', 'Current Step/Status', 'Supporting Info', 'Disposition'],
		 colModel: [
			{ name: 'PURCHASE_ITEM_ID', index: 'PURCHASE_ITEM_ID', key: true, colmenu : false, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : false},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 100, align: 'center' },
			{ name: 'SUPPLIER_NAME' , index: 'SUPPLIER_NAME', colmenu : false, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : false},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 90, align: 'center' },
			{ name: 'SERIAL' , index: 'SERIAL', colmenu : false, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : false},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'CUST_PURCH_NUM', index: 'CUST_PURCH_NUM', colmenu : false, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : false},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center'  },
			{ name: 'QTY', index: 'QTY', width: 50 , colmenu : false, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : false},
						searchoptions : {searchOperMenu : false, sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'ST_DATE' , index: 'ST_DATE', colmenu : false, sorttype:'date', coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : false},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},formatter: 'date', formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" }, width: 90, align: 'center' },			
			{ name: 'ACTUAL_START_DATE', index: 'ACTUAL_START_DATE', colmenu : false, sorttype:'date', coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : false},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},formatter: 'date', formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" }, width: 90, align: 'center' },	
			{ name: 'PRODUCT_NAME',  index: 'PRODUCT_NAME', colmenu : true, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : false},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 250, align: 'left' },
			{ name: 'PROC_NAME' , index: 'PROC_NAME', colmenu : false, width: 180, align: 'left' },
			{ name: 'CUR_STEP_TEXT', index: 'CUR_STEP_TEXT', colmenu : false, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : false}, formatter: currentStepFormatter, align: 'center' },
			{ name: 'PURCHASE_ID', index: 'PURCHASE_ID', colmenu : false, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : false}, formatter: supportingInfoFormatter, align: 'center' },
			{ name: 'ACTUAL_PART_ID', index: 'ACTUAL_PART_ID', id: 'ACTUAL_PART_ID', colmenu : false, editable: false, edittype:"textarea",width: 180, align: 'center' ,formatter: actionsFormatter, align: 'center'},
		],
		viewrecords: true, // show the current page, data rang and total records on the toolbar
		rowNum: 10,
		loadonce: true, // this is just for the demo
		pager: "#jqGridPager",
		height: 'auto',
		gridview: true,
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
		//searchOnEnter: false,
		searchOperators : true
	});
	
	
	$("#jqGrid").jqGrid().trigger('reloadGrid');
	
	$("#jqGrid").tooltip();
    
    $('a.colmenu').click(function( event ) {
      //event.stopPropagation();
      event.preventDefault();
      // Do something
    });
    
	function currentStepFormatter (cellvalue, options, rowObject) {
	    var thisCellVal = '';
        if (cellvalue !== 0) { 
            var thisVal = cellvalue.replace(/<>/g, "");
            thisCellVal = '<strong>' + thisVal + '</strong><br />' + rowObject.TIME_COMPLETE + ' ' + rowObject.PERC_COMPLETE;
        }
        else {
            thisCellVal = 'Waiting to Start';
        }
        return thisCellVal;
    }
    
    function supportingInfoFormatter (cellvalue, options, rowObject) {
        var NCRButton = (rowObject.HAS_NCR  == 1) ? '<button class="btn support-btn btn-xs btn-warning" data-toggle="modal" data-target="#ncrModal" title="View NCR"><i class="fa fa-clipboard"></i>NCR</button>':'';
        var FileButton = (rowObject.HAS_FILE  == 1) ? '<button class="btn support-btn btn-xs btn-info" href="#" data-toggle="modal" data-target="#imageModal" title="View Photos"><i class="fa fa-file-image-o"></i>Photos</button>':'';
        var MonitorButton = (rowObject.HAS_MONITOR  == 1) ? '<button class="btn support-btn btn-xs btn-success" data-toggle="modal" data-target="#monitorModal"  title="View Monitors"><i class="fa fa-bar-chart "></i>Monitors</button>':'';
        thisCellVal = NCRButton + FileButton + MonitorButton;
        return thisCellVal;
    }
    function actionsFormatter (cellvalue, options, rowObject) {
        InstructionsCellVal = '<button id="add-button' + rowObject.ACTUAL_PART_ID + '" onclick="toggleInstructions(' + rowObject.ACTUAL_PART_ID + ')" class="btn support-btn btn-xs btn-danger show">Add Instructions</button><div id="instruction' + rowObject.ACTUAL_PART_ID + '" class="hidden" ><textarea style="width: 95%;" rows=4 placeholder="Add disposition Instructions"></textarea><button class="btn support-btn btn-xs btn-primary" onclick="toggleInstructions(' + rowObject.ACTUAL_PART_ID + ')">Save</button>';
        thisCellVal = (rowObject.HAS_NCR  == 1) ? InstructionsCellVal:'N/A';
        return thisCellVal;
    }
    
    //ajax emulation
    $.mockjax({
        url: '/post',
        responseTime: 200
    }); 
    
});