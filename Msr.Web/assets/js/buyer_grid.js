
$.jgrid.defaults.responsive = true;

function toggleInstructions (theID) {
	event.preventDefault();
    var addButton = '#add-button' + theID;
    var textareaDiv = '#instruction' + theID;
    $(addButton).toggleClass('hidden show');
    $(textareaDiv).toggleClass('hidden show');
}

$(document).ready(function () {
    var _true = true;

    $("#jqGrid").jqGrid({
		    url: '/buyer/BuyerData',
		    mtype: "GET",
		    styleUI: 'Bootstrap',
        datatype: "json",
        colNames: ['WO Item #', 'Status', 'Supplier', 'Serial #', 'PO #', 'Qty', 'Start Date', 'Due Date', 'Product Name', 'Procedure', 'Current Step/Status', 'Supporting Info', 'Invoice', 'Price', 'Amount', 'Date'],
		 colModel: [
		     {
		         name: 'PurchaseItemId',
		         index: 'PurchaseItemId',
		         key: _true,
		         colmenu: false,
		         coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
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
		         coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         width: 90,
		         align: 'center'
		     },
		     {
		         name: 'Serial',
		         index: 'Serial',
		         colmenu: false,
		         coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         align: 'center'
		     },
		     {
		         name: 'CustPurchNum',
		         index: 'CustPurchNum',
		         colmenu: false,
		         coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         align: 'center'
		     },
		     {
		         name: 'Qty',
		         index: 'Qty',
		         width: 50,
		         colmenu: false,
		         coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         align: 'center'
		     },
		   {
		       name: 'ActualStartDate',
		       index: 'ActualStartDate',
		       colmenu: false,
		       sorttype: 'date',
		       coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
		       searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		       formatter: 'date',
		       formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
		       width: 90,
		       align: 'center',
		       hidden: _true
		   },
		     {
		         name: 'DueDate',
		         index: 'DueDate',
		         colmenu: false,
		         sorttype: 'date',
		         coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         formatter: 'date',
		         formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
		         width: 90,
		         align: 'center',
		         hidden: _true
		     },
		     {
		         name: 'ProductName',
		         index: 'ProductName',
		         colmenu: _true,
		         coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
		         searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
		         width: 250,
		         align: 'left',
                 hidden :  true
		     },
                {
                    name: 'ProcName',
                    index: 'ProcName',
                    colmenu: _true,
                    coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 250,
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
                     align: 'center',
                     hidden: true
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
                 name: 'InvoiceId',
                 index: 'InvoiceId',
                 colmenu: _true,
                 coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
                 searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                 width: 250,
                 align: 'left'
             },

               {
                   name: 'Price',
                   index: 'Price',
                   width: 50,
                   colmenu: false,
                   coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
                   searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                   align: 'center'
               },

              {
                  name: 'InvoiceAmount',
                  index: 'InvoiceAmount',
                  width: 50,
                  colmenu: false,
                  coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
                  searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                  align: 'center'
              },
               {
                   name: 'InvoiceDate',
                   index: 'InvoiceDate',
                   colmenu: false,
                   sorttype: 'date',
                   coloptions: { sorting: false, columns: _true, filtering: false, seraching: false, grouping: false, freeze: false },
                   searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                   formatter: 'date',
                   formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                   width: 90,
                   align: 'center'
               }
		    
		],
		viewrecords: _true, // show the current page, data rang and total records on the toolbar
		rowNum: 10,
		loadonce: false, // this is just for the demo
		pager: "#jqGridPager",
		height: 'auto',
		gridview: _true,
		sortname: 'PurchaseItemId',
		sortable: _true,
		sortorder: 'asc',
		cellEdit: _true,
        cellsubmit : 'clientArray',
        editurl: 'clientArray',
	    autowidth: _true,
	    colMenu : _true,
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
                refresh: _true
            },
            {}, // edit options
            {}, // add options
            {}, // delete options
            { multipleSearch: _true } 
    );
    $('#jqGrid').jqGrid('filterToolbar',{
		stringResult: _true,
		searchOnEnter: _true,
		searchOperators : _true
	});
	
	
	$("#jqGrid").jqGrid().trigger('reloadGrid');
	
	$("#jqGrid").tooltip();
    
    $('a.colmenu').click(function( event ) {
      //event.stopPropagation();
      event.preventDefault();
      // Do something
    });
    
    function currentStepFormatter(cellvalue, options, rowObject) {
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


    //ajax emulation
    $.mockjax({
        url: '/post',
        responseTime: 200
    });


    $('#ncrModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var id = button.data('id');
        var modal = $(this);

        $.ajax({
            type: "GET",
            url: '/wip/getncrmodel?id=' + id,
            dataType: 'html',
            success: function (data) {
                modal.find('.modal-body').html(data);
            },
            error: function () {

            }
        });
    })


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


    
});