
$.jgrid.defaults.responsive = true;

$(document).ready(function () {

		$("#jqGrid").jqGrid({
		url: '/assets/data/purchase_orders.json',
        mtype: "GET",
        datatype: "json",
		    colNames:['ID','Name', 'PO #','PO Name','Invoiced', 'Not Invoiced', 'Balance','Facility/Supplier', 'Cust/Co/Dept', 'Open Date', 'Close Date', 'Total Purchase Limit', 'Unused Amount','Type', 'Close/Cut Trigger','Revision', 'Status', 'Actions'],
		 colModel: [
			{ name: 'ID', index: 'ID', key: true, hidden: true, colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center' },
			{ name: 'NAME' , index: 'NAME', colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center' },
			{ name: 'REFERENCE_PO' , index: 'REFERENCE_PO', colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true, },
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center' },
			{ name: 'REFERENCE_NAME' , index: 'REFERENCE_NAME', colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'INVOICED_BALANCE', index: 'INVOICED_BALANCE', formatter: usdFormatter, colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center'  },
			{ name: 'UNINVOICED_BALANCE', index: 'UNINVOICED_BALANCE', formatter: usdFormatter, colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false, sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'BALANCE', index: 'BALANCE', formatter: usdFormatter, colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false, sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'SUPPLIER_NAME',  index: 'SUPPLIER_NAME', colmenu : true, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'left' },
			{ name: 'CUST_BILL_NAME',  index: 'CUST_BILL_NAME', colmenu : true, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'left' },
			{ name: 'OPEN_DATE' , index: 'ACTUAL_START_DATE', hidden: false, colmenu : false, sorttype:'date', coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},formatter: 'date', formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" }, align: 'center' },			
			{ name: 'CLOSE_DATE', index: 'CREATE_DATE', hidden: false, colmenu : false, sorttype:'date', coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},formatter: 'date', formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" }, align: 'center' },	
			{ name: 'TOTAL_PURCHASE_LIMIT' , index: 'PROC_NAME', formatter: usdFormatter, colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true}},
			{ name: 'UNUSED_AMOUNT',     index: 'UNUSED_AMOUNT', formatter: usdFormatter, colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true}},
			{ name: 'ACCT_TYPE', index: 'ACCT_TYPE', hidden: false, colmenu : false, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : true}}, 
			{ name: 'INVOICE_TRIGGER',  index: 'INVOICE_TRIGGER', colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center' },
			{ name: 'REV',  index: 'REV', colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'STATUS',  index: 'STATUS', colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'ACTION', index: 'ACTION', formatter: supportingInfoFormatter, colmenu : false, editable: false, width: 180, align: 'center'},	
			
		],
		viewrecords: true, // show the current page, data rang and total records on the toolbar
		rowNum: 15,
		loadonce: true, // this is just for the demo
		pager: "#jqGridPager",
		height: 'auto',
	    autowidth: true,
	    colMenu : true
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
	
	
    /*
    $('#jqGrid').setGroupHeaders({
        useColSpanStyle: true,
        groupHeaders: [
            //{ "numberOfColumns": 3, "titleText": "Outstanding", "startColumnName": "INVOICED_BALANCE" }//
            ]
    });*/
	
	$("#jqGrid").jqGrid().trigger('reloadGrid');
	
	$("#jqGrid").tooltip();

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
    
    function supportingInfoFormatter (cellvalue, options, rowObject) {
        var NCRButton = (rowObject.HAS_NCR  == 1) ? '<button class="btn support-btn btn-xs btn-warning" data-toggle="modal" data-target="#ncrModal" title="View NCR"><i class="fa fa-clipboard"></i>NCR</button>':'';
        var FileButton = (rowObject.HAS_FILE  == 1) ? '<button class="btn support-btn btn-xs btn-info" href="#" data-toggle="modal" data-target="#imageModal" title="View Photos"><i class="fa fa-file-image-o"></i>Photos</button>':'';
        var MonitorButton = (rowObject.HAS_MONITOR  == 1) ? '<button class="btn support-btn btn-xs btn-success" data-toggle="modal" data-target="#monitorModal"  title="View Monitors"><i class="fa fa-bar-chart "></i>Monitors</button>':'';
        thisCellVal = NCRButton + FileButton + MonitorButton;
        return thisCellVal;
    }
    
    function usdFormatter (cellvalue, options, rowObject) {
        thisAmount = parseFloat(cellvalue).toFixed(2);
        thisCellVal = '$ ' + thisAmount;
        return thisCellVal;
    }


    
});