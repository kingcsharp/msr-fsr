
$.jgrid.defaults.responsive = true;

$(document).ready(function () {

		$("#custReqsGrid").jqGrid({
		url: '/assets/data/customer-requirements.json',
        mtype: "GET",
        datatype: "json",
		    colNames:['Submitted Date','Company','Division/Fab#', 'Submitted By','Part/Kit No.','Description', 'Status','Representative'],
		 colModel: [
			{ name: 'CREATED_DATE', index: 'CREATED_DATE', title: false, colmenu : false, sorttype:'date', coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},formatter: 'date', formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" }, width: 90, align: 'center' },	
			{ name: 'CUSTOMER_NAME' , index: 'COMPANY',  title: false, colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true, },
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 80, align: 'center' },
			{ name: 'DIVISION_FAB', index: 'DIVISION_FAB',  title: false, width: 70, colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center'  },
			{ name: 'SUBMITTER_NAME' , index: 'SUBMITTER_NAME',  title: false, width: 80, colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true, },
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'PRODUCT_NAME',  index: 'PRODUCT_NAME',  title: false, colmenu : true, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 120, align: 'left' },
			{ name: 'PRODUCT_DESC' , index: 'PRODUCT_DESC',  title: false, width: 200, colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true, },
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'STATUS' , index: 'STATUS', title: false, colmenu :  false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 70, align: 'center' },
			{ name: 'REP_NAME' , index: 'REP_NAME',  title: false, colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 80, align: 'center', formatter: repFormatter, editable:true }
		],
		viewrecords: true, // show the current page, data rang and total records on the toolbar
		rowNum: 10, rowList: [10, 20, 50, 100],
		loadonce: true, // this is just for the demo
		pager: "#custReqsGrid",
		height: 'auto',
	    autowidth: true,
	    colMenu : true,
	    sortableRows: true,
	    ondblClickRow: function(id){
	        console.log("ID: " + id);
	        var columnIndex = 4;
	        var cellValue = $("#" + id).find('td').eq(columnIndex).text();
	        $('#partNumTitle').html(cellValue);
            $('#reqDetail').removeClass('hidden'); //here's where you get the step detail
        },
        loadComplete: function () {
            //generating doubleclick tootip for rows
            var rows = $(this).getDataIDs();
            
            for (var i = 0; i < rows.length; i++) {
                var row = $(this).getRowData(rows[i]);
                this.rows[i + 1].className = this.rows[i + 1].className + ' ui-state-highlight';
                $(this.rows[i + 1]).attr('title', 'Double click row to view or create step details');
                $(this.rows[i + 1]).attr('data-toggle', 'tooltip');
                $(this.rows[i + 1]).attr('data-placement', 'bottom');
            }
            $("[data-toggle=tooltip]").tooltip();
        }
	});
	
	$('#custReqsGrid').navGrid("#custReqsGridPager", {                
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
    $('#custReqsGrid').jqGrid('filterToolbar',{
		stringResult: true,
		//searchOnEnter: false,
		searchOperators : true
	});
	//$('#custReqsGrid').jqGrid('sortableRows');
	
	
    $('#custReqsGrid').setGroupHeaders({
        useColSpanStyle: true,
        groupHeaders: [
            { "numberOfColumns": 10, "titleText": "Work Order Info", "startColumnName": "PURCHASE_ITEM_ID" },
            { "numberOfColumns": 5, "titleText": "Invoice Details", "startColumnName": "INVOICE_ID" }]
    });
	
	$("#custReqsGrid").jqGrid().trigger('reloadGrid');
	
	$("#custReqsGrid").tooltip();
	
	//$('body').chardinJs('start')
	
	$("#save").click(function(){
		filter = $("#custReqsGrid").jqGrid('getGridParam', 'postData').filters;
		var perm = [ 1, 0, 2, 4, 3 ];
		$("#custReqsGrid").jqGrid('remapColumns', perm, true, false);
		console.log($("#custReqsGrid").jqGrid('getGridParam','colModel'));
    });

	$("#load").click(function(){
		console.log(filter);
		$("#custReqsGrid").jqGrid('refreshFilterToolbar', {
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
			$("#custReqsGrid").jqGrid('filterInput', self.value);
		},0);
	});

	$('a.colmenu').click(function( event ) {
      //event.stopPropagation();
      event.preventDefault();
      // Do something
    });
    
    function repFormatter (cellvalue, options, rowObject) {
        var StartButton = (rowObject.REP_NAME  == "") ? '<button id="process-start-btn" class="btn support-btn btn-xs btn-warning" data-toggle="tooltip" data-placement="left" title="Start Process Definition"><i class="fa fa-play-circle"></i>Start</button>': rowObject.REP_NAME;
        thisCellVal = StartButton;
        return thisCellVal;
    }

    $('#process-start-btn').click(function( event ) {
        event.preventDefault();
        var rowData = $('#custReqsGrid').jqGrid('getRowData', rowId);
        console.log("ROW ID: " + rowId);
        $('#custReqsGrid').jqGrid("setCell", rowid, 'REP_NAME', 'Emily Hart');
    });
    
         // process specs table
     var i=1;
     $("#add_row").click(function(){
          $('#row'+i).html("<td><select class='form-control' name='process_step" + (i+1) +"'><option value=''>Select a Step...</option><option value='INCOMING INSPECTION'>INCOMING INSPECTION</option><option value='SERIALIZE'>SERIALIZE</option><option value='BEAD BLAST'>BEAD BLAST</option><option value='SOAK - EXHAUSTED'>SOAK - EXHAUSTED</option></td><td><input type='text' name='a"+i+"'  class='form-control' value='" + (i+1) +"'/></td><td><input type='text' name='b"+i+"' class='form-control labor-mins' value='10'/><td><input type='text' name='c0' class='form-control machine-mins' value='10'/></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='d"+i+"' class='form-control' value='500'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><input type='text' name='e"+i+"' class='form-control' value='15'><span class='input-group-addon'>%</span></div></td><td class='text-center hidden hide-col'><input type='text' name='f"+i+"' class='form-control' value='7'></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='g"+i+"' class='form-control' value='0.01'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='h"+i+"' class='form-control' value='0.05'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='i"+i+"' class='form-control' value='0.01'><span class='input-group-addon'>.00</span></div></td>");
          $('#process_steps').append('<tr id="row'+(i+1)+'" ></tr>');
          i++;
          recalculateTotals();
     });

     $("#delete_row").click(function(){
    	 if(i>1){
		    $("#row"+(i-1)).html('');
		    i--;
		 }
		 recalculateTotals();
	 });
	 
        //init switch
    $('#showHiddenColumns').bootstrapSwitch();
    
    $('#showHiddenColumns').on('switchChange.bootstrapSwitch', function (e, state) {
        e.preventDefault();
        if(state){
            $(".hide-col").removeClass('hidden');
            $(".hide-col").removeClass('hidden');
        }else{
            $(".hide-col").addClass('hidden');
            $(".hide-col").addClass('hidden');
        }
    });
    
    //calculations
    $(document).on("keyup", ".labor-mins", function() {
        recalculateTotals();
    });
    
    $(document).on("keyup", ".machine-mins", function() {
        recalculateTotals();
    });

    recalculateTotals();
});

function recalculateTotals() {
    var sum = 0;

        $(".labor-mins").each(function(){
            if($(this).val() != "")
            sum += parseInt($(this).val());  
        });

        $("#total-direct-mins").val(parseFloat(sum).toFixed(2));
        
        var rate = 3.76; 
        var sum2 = (sum * rate).toFixed(2);
        $("#total-direct-dollars").val(parseFloat(sum2).toFixed(2));
        
        var machine = parseInt($("#standard-machine-dollars").val());
        var newTotal = (sum2 + machine);
        $("#total-sale-price").val(parseFloat(newTotal).toFixed(2));
    3

         var sum1 = 0;
        $(".machine-mins").each(function(){
            if($(this).val() != "")
            sum1 += parseInt($(this).val());  
        });
        $("#total-machine-mins").val(parseFloat(sum1).toFixed(2));
        
        var rate2 = .44; 
        var sum3 = (sum1 * rate2);
        $("#standard-machine-dollars").val(parseFloat(sum3).toFixed(2));
        var labor1 = parseInt($("#total-direct-dollars").val());
        var newTotal1 = (labor1 + sum3);
        $("#total-sale-price").val(parseFloat(newTotal1).toFixed(2));

  
}

