
$.jgrid.defaults.responsive = true;

$(document).ready(function () {

    //read  in timezones.json to map for each user in formatter below
    var timezones = {};
    $.ajax({
        url: "/assets/data/timezones.json",
        async: false,
        dataType: 'json',
        success: function (data) {
            timezones = data.rows;
        }
    });

 

    $("#jqGrid").jqGrid({
        url: '/user/MasterUserData', 
        mtype: "GET",
        datatype: "json",
        colNames: ['First Name', 'Last Name', 'TimeZone', 'CompanyName', 'RoleName', 'Primary Phone', 'Email', 'User Name', 'Status',"Actions"],
        colModel: [
           {
               name: 'FirstName', index: 'FirstName', colmenu: true, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
           },
           {
               name: 'LastName', index: 'LastName', colmenu: true, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
           },
             {
                 name: 'TimeZone', index: 'TimeZone', colmenu: true, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
                 formatter: timezoneFormatter, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
             },
             {
                 name: 'CompanyName', index: 'CompanyName', colmenu: true, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
             },
              {
                  name: 'RoleName', index: 'RoleName', colmenu: true, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
              },
            
             {
                 name: 'PrimaryPhone', index: 'PrimaryPhone', colmenu: true, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
             },
             {
                 name: 'Email', index: 'Email', colmenu: true, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
             },
             {
                 name: 'UserName', index: 'UserName', colmenu: true, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
             },
             {
                 name: 'Status', index: 'Status', colmenu: true, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
             },
             { name: 'ID', index: 'ID', key: true, hidden: false, editable: false, formatter: pwResetFormatter, width: 100, align: 'center' }
          
        ],
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 15,
        loadonce: true, // this is just for the demo
        pager: "#jqGridPager",
        height: 'auto',
        autowidth: true,
        colMenu: false,
        cellEdit: false,
        grouping: true,
        groupingView: {
            groupField: ['CompanyName'],
            groupColumnShow: [false],
            groupText: ['<b>{0} - {1} Item(s)</b>'],
            groupSummary: [false],
            groupCollapse: false
        },
       
    });


    $("#jqGrid").tooltip();

    //$("#jqGrid").jqGrid().trigger('reloadGrid');
    $("#jqGrid").trigger("reloadGrid", [{ page: 1 }]);

    // hax to stop propogation
    $('a.colmenu').click(function (event) {
        event.preventDefault();
    });

    function pwResetFormatter(cellvalue, options, rowObject) {
        thisCellVal = '<a href="/user/edituser/' + rowObject.Id + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Edit</a>';
        return thisCellVal;
    }

    function timezoneFormatter(cellvalue, options, rowObject) {
        var result = $.grep(timezones, function (e) { return e.ID == cellvalue; });
        return result[0]['DESCRIPTION'];
    }



    //ajax emulation
    $.mockjax({
        url: '/post',
        responseTime: 200
    });



});