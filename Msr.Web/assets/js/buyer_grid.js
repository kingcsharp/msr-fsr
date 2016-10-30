
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
		    url: '/buyer/BuyerData',
		    mtype: "GET",
		    styleUI: 'Bootstrap',
        datatype: "json",
		    colNames:['WO Item #','Status','Supplier','Serial #','PO #', 'Qty', 'Product Name','Invoice', 'Amount', 'Date'],
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
                 name: 'InvoiceStatus',
                 index: 'InvoiceStatus',
                 colmenu: false, width: 180,
                 align: 'left'
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
		         name: 'ProductName',
		         index: 'ProductName',
		         colmenu: true,
		         coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         width: 250,
		         align: 'left'
		     },
		  
             {
                 name: 'InvoiceId',
                 index: 'InvoiceId',
                 colmenu: true,
                 coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                 searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                 width: 250,
                 align: 'left'
             },

              {
                  name: 'InvoiceAmount',
                  index: 'InvoiceAmount',
                  width: 50,
                  colmenu: false,
                  coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                  searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                  align: 'center'
              },
               {
                   name: 'InvoiceDate',
                   index: 'InvoiceDate',
                   colmenu: false,
                   sorttype: 'date',
                   coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                   searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                   formatter: 'date',
                   formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                   width: 90,
                   align: 'center'
               }
		    
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
    
    $('a.colmenu').click(function( event ) {
      //event.stopPropagation();
      event.preventDefault();
      // Do something
    });
    
   
    //ajax emulation
    $.mockjax({
        url: '/post',
        responseTime: 200
    }); 
    
});