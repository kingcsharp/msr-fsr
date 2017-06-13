
$.jgrid.defaults.responsive = true;

$(document).ready(function () {

		$("#jqGrid").jqGrid({
		url: '/assets/data/wip-buyer.json',
        mtype: "GET",
        datatype: "json",
		    colNames:['WO Item #','Status', 'Supplier','Serial #','PO #', 'Qty', 'Start Date','Due Date', 'Product Name', 'Procedure', 'Current Step/Status', 'Supporting Info', 'Invoice #','Status', 'Price','Amount', 'Date'],
		 colModel: [
			{ name: 'PURCHASE_ITEM_ID', index: 'PURCHASE_ITEM_ID', key: true, colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 100, align: 'center' },
			{ name: 'STATUS' , index: 'STATUS', colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 90, align: 'center' },
			{ name: 'SUPPLIER_NAME' , index: 'SUPPLIER_NAME', width: 150, colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true, },
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 90, align: 'center' },
			{ name: 'SERIAL' , index: 'SERIAL', colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'PO_NUMBER', index: 'PO_NUMBER', colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center'  },
			{ name: 'QTY', index: 'QTY', width: 50 , colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:false, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false, sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'ACTUAL_START_DATE' , index: 'ACTUAL_START_DATE', hidden: true, colmenu : false, sorttype:'date', coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},formatter: 'date', formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" }, width: 90, align: 'center' },			
			{ name: 'DUE_DATE', index: 'DUE_DATE', hidden: true, colmenu : false, sorttype:'date', coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},formatter: 'date', formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" }, width: 90, align: 'center' },	
			{ name: 'PRODUCT_NAME',  index: 'PRODUCT_NAME', colmenu : true, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 250, align: 'left' },
			{ name: 'PROC_NAME' , index: 'PROC_NAME', hidden: true, colmenu : false, width: 180, align: 'left' },
			{ name: 'CUR_STEP_TEXT', index: 'CUR_STEP_TEXT', hidden: true, colmenu : false, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : true}, formatter: currentStepFormatter, align: 'center' },
			{ name: 'PURCHASE_ID', index: 'PURCHASE_ID', hidden: true, colmenu : false, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : true}, formatter: supportingInfoFormatter, align: 'center' },
			{ name: 'INVOICE_ID',  index: 'INVOICE_ID', colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center' },
			{ name: 'INVOICE_STATUS',  index: 'INVOICE_STATUS', colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center' },
			{ name: 'PRICE',  index: 'PRICE', colmenu : false, formatter: usdFormatter, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'INVOICE_AMOUNT',  index: 'INVOICE_AMOUNT', formatter: usdFormatter, colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'INVOICE_DUE_DATE', index: 'INVOICE_DUE_DATE', colmenu : false, sorttype:'date', coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},formatter: 'date', formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" }, width: 90, align: 'center' },	
			
		],
		viewrecords: true, // show the current page, data rang and total records on the toolbar
		rowNum: 15,
		loadonce: true, // this is just for the demo
		pager: "#jqGridPager",
		height: 'auto',
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
	
	
    $('#jqGrid').setGroupHeaders({
        useColSpanStyle: true,
        groupHeaders: [
            { "numberOfColumns": 10, "titleText": "Work Order Info", "startColumnName": "PURCHASE_ITEM_ID" },
            { "numberOfColumns": 5, "titleText": "Invoice Details", "startColumnName": "INVOICE_ID" }]
    });
	
	$("#jqGrid").jqGrid().trigger('reloadGrid');
	
	$("#jqGrid").tooltip();
	
	$('body').chardinJs('start')
	
	$("#save").click(function(){
		filter = $("#jqGrid").jqGrid('getGridParam', 'postData').filters;
		var perm = [ 1, 0, 2, 4, 3 ];
		$("#jqGrid").jqGrid('remapColumns', perm, true, false);
		console.log($("#jqGrid").jqGrid('getGridParam','colModel'));
	});
	$("#load").click(function(){
		console.log(filter);
		$("#jqGrid").jqGrid('refreshFilterToolbar', {
			filters : filter,
			onClearVal: function ( elem, name) {
				if(name === 'CustomerID') {
					console.log(elem);
					elem.multiselect('refresh');
				}
			},
			onSetVal: function ( elem, name) {
				if(name === 'CustomerID') {
					console.log(elem);
					elem.multiselect('refresh');
				}
			}
		});
	});
	var timer;
	$("#search_cells").on("keyup", function() {
		var self = this;
		if(timer) { clearTimeout(timer); }
		timer = setTimeout(function(){
			//timer = null;
			$("#jqGrid").jqGrid('filterInput', self.value);
		},0);
	});

	$('a.colmenu').click(function( event ) {
      //event.stopPropagation();
      event.preventDefault();
      // Do something
    });
    
	function currentStepFormatter (cellvalue, options, rowObject) {
	    var thisCellVal = '';
        if (cellvalue !== 0) {
            var allCellVals = cellvalue.split('<>');
            var thisVal = allCellVals[1];
            thisCellVal = '<strong>' + thisVal + '</strong><br />' + rowObject.TIME_COMPLETE + ' ' + rowObject.PERC_COMPLETE;
        }
        else {
            thisCellVal = 'Waiting to Start';
        }
        return thisCellVal;
    }
    
    function supportingInfoFormatter (cellvalue, options, rowObject) {
        thisCellVal = '<button class="btn btn-xs btn-warning" style="width:90px; margin:2px;font-size: .8em;" data-toggle="modal" data-target="#ncrModal"><i class="fa fa-clipboard"></i>View NCR</button>&nbsp;<button class="btn btn-xs btn-info" href="#" style="width:90px; margin:2px;font-size: .8em;" data-toggle="modal" data-target="#imageModal""><i class="fa fa-file-image-o"></i> View Photos</button>';
        return thisCellVal;
    }
    
    function usdFormatter (cellvalue, options, rowObject) {
        thisAmount = parseFloat(cellvalue).toFixed(2);
        thisCellVal = '$ ' + thisAmount;
        return thisCellVal;
    }


    
});