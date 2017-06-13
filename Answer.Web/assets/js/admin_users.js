
$.jgrid.defaults.responsive = true;

$(document).ready(function () {
    
        //read  in timezones.json to map for each user in formatter below
        var timezones = {};
        $.ajax({
        	url: "/assets/data/timezones.json",
        	async: false,
        	dataType: 'json',
        	success: function(data) {
        		timezones = data.rows;
        	}
        });

		$("#jqGrid").jqGrid({
		url: '/assets/data/portal_users.json',
        mtype: "GET",
        datatype: "json",
		    colNames:['First Name','Last Name','Full Name','TimeZone','Created Date','Company','Portal Role','Title','Primary Phone','2nd Phone','Email','User Name','Status','Actions'],
		 colModel: [
			{ name: 'NAME' , index: 'NAME', colmenu : true, editable: true, edittype: "text", coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 90, align: 'center' },
			{ name: 'LAST_NAME' , index: 'LAST_NAME', editable: true, edittype: "text", colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true, },
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 90, align: 'center' },
			{ name: 'FULL_NAME' , index: 'FULL_NAME', hidden: true, editable: true, edittype: "text", colmenu : false, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'TIME_ZONE', index: 'TIME_ZONE', editable: true, edittype: "select", editoptions: { value: "1:Atlantic Time (Canada);2:Central Time (US & Canada);3:Eastern Time (US & Canada);4:Greenwhich Mean Time;5:Mountain Time (US & Canada);6:Pacific Time (US & Canada)"}, colmenu : false, formatter: timezoneFormatter, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center'  },
			{ name: 'CREATED_DATE' , index: 'CREATED_DATE', hidden: true, colmenu : false, sorttype:'date', coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},formatter: 'date', formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" }, width: 90, align: 'center' },			
			{ name: 'COMPANY_NAME',  index: 'COMPANY_NAME', hidden: false, editable: true, edittype: "select", editoptions: { value: "1:Applied Materials, 2:ATA,3:Intel D1C / D1D / D1X;4:INTEL F11X;5:Intel F11X Billing Department;6:INTEL F17;7:INTEL F20;8:INTEL F24;9:INTEL F28;10:INTEL F32;11:INTEL F68;12:INTEL IMR ABQ;13:INTEL IMR PHX;14:INTEL VF;15:MATERION;16:MERSEN;17:SEAGATE;18VWR"}, coloptions : {sorting:true, columns: true, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center' },
			{ name: 'USER_ROLE' , index: 'USER_ROLE', editable: true, edittype: "select", editoptions: { value: "1:Client Admin;2:Client Buyer;3:Client Engineer"}, align: 'center'},
			{ name: 'TITLE', index: 'TITLE', editable: true, edittype: "text", colmenu : false, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : true}, align: 'center' },
			{ name: 'PRIMARY_PHONE_NUMBER', index: 'PRIMARY_PHONE_NUMBER', editable: true, edittype: "text", colmenu : false, coloptions : {sorting:false, columns: true, filtering: false, seraching:false, grouping:false, freeze : true}, align: 'center' },
			{ name: 'SECONDARY_PHONE_NUMBER',  index: 'SECONDARY_PHONE_NUMBER', hidden: true, editable: true, edittype: "text", colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},align: 'center' },
			{ name: 'WORK_EMAIL_ADDRESS',  index: 'WORK_EMAIL_ADDRESS', editable: true, edittype: "text", colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'LOGIN',  index: 'LOGIN', colmenu : false, editable: true, edittype: "text", coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']}, align: 'center' },
			{ name: 'SYSTEM_STATUS', index: 'SYSTEM_STATUS', editable: true, edittype: "select", editoptions: { value: "1:Active;2:Inactive"}, colmenu : false, coloptions : {sorting:true, columns: false, filtering: true, seraching:true, grouping:false, freeze : true},
						searchoptions : {searchOperMenu : false,sopt : ['eq','gt','lt','ge','le']},width: 90, align: 'center' },	
			{ name: 'ID', index: 'ID', key: true, hidden: false, colmenu : false, editable: false, formatter: pwResetFormatter, width: 100, align: 'center' },
			
		],
		viewrecords: true, // show the current page, data rang and total records on the toolbar
		rowNum: 15,
		loadonce: true, // this is just for the demo
		pager: "#jqGridPager",
		height: 'auto',
	    autowidth: true,
	    colMenu : true,
	    cellEdit: true,
        cellsubmit : 'clientArray',
        editurl: 'clientArray',
	    sortname: 'COMPANY_NAME',
   	    grouping:true,
       	groupingView : {
       		groupField : ['COMPANY_NAME'],
   		    groupColumnShow : [false],
   		    groupText : ['<b>{0} - {1} Item(s)</b>'],
            groupOrder: ["asc"],
            groupSummary: [false],
            groupCollapse: false
       	},
	});
	
	$('#jqGrid').navGrid("#jqGridPager", {                
                search: true, // show search button on the toolbar
                add: true,
                edit: false,
                del: true,
                refresh: true
            },
            {}, // edit options
            {}, // add options
            {}, // delete options
            { multipleSearch: true } 
    );
	
	$("#jqGrid").tooltip();
	
	//$("#jqGrid").jqGrid().trigger('reloadGrid');
	$("#jqGrid").trigger("reloadGrid",[{page:1}]);
	
    $("#jqGrid").promise().done(function() {
        // Start the tour!
        hopscotch.startTour(tour);
    });
	
	
	// hax to stop propogation
	$('a.colmenu').click(function( event ) {
      event.preventDefault();
     });

    function pwResetFormatter (cellvalue, options, rowObject) {
        thisCellVal = '<button class="btn btn-xs btn-danger" style="margin:2px;font-size: .8em;" data-toggle="modal" data-target="#pwResetConfirmModal"><i class="fa fa-key"></i> Reset Password</button>';
        return thisCellVal;
    }
    
    function timezoneFormatter (cellvalue, options, rowObject) {
        var result = $.grep(timezones, function(e){ return e.ID == cellvalue; });
        return result[0]['DESCRIPTION'];
    }

    //ajax emulation
    $.mockjax({
        url: '/post',
        responseTime: 200
    }); 


    
});