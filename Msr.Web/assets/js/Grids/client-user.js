
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

    var roles = [
            { "ID": "ClientAdmin", "TITLE": "Client Admin" },
            { "ID": "ClientBuyer", "TITLE": "Client Buyer" },
            { "ID": "ClientEngineer", "TITLE": "Client Engineer" },
            { "ID": "SuperAdmin", "TITLE": "Super Admin" }
    ]

    $("#jqGrid").jqGrid({
        url: '/user/ClientUserData',
        mtype: "GET",
        datatype: "json",
        colNames: ['First Name', 'Last Name', 'Full Name', 'TimeZone', 'Created Date', 'Portal Role', 'Primary Phone', '2nd Phone', 'Email', 'User Name', 'Password', 'Status', 'Actions'],
        colModel: [
           {
               name: 'FirstName', index: 'FirstName', colmenu: true, editable: true, edittype: "text", coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
               editrules: { required: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, width: 90, align: 'center'
           },
           {
               name: 'LastName', index: 'LastName', editable: true, edittype: "text", colmenu: false, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true, },
               editrules: { required: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, width: 90, align: 'center'
           },

              {
                  name: 'FullName', index: 'FullName', editable: true, edittype: "text", colmenu: false, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true, },
                  searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, width: 90, align: 'center'
              },

           {
               name: 'TimeZone', index: 'TimeZone', editable: true, edittype: "select", editoptions: { value: "100:Mid-Atlantic;11436:International Date Line West;11437:Midway Island, Samoa" }, colmenu: false, formatter: timezoneFormatter, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
               editrules: { required: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, align: 'center'
           },
           {
               name: 'CreatedDate', index: 'CreatedDate', hidden: false, colmenu: false, sorttype: 'date', coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, formatter: 'date', formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" }, width: 90, align: 'center'
           },
           {
               name: 'RoleName', index: 'RoleName', editable: true, edittype: "select",
               editoptions: { value: "ClientAdmin:Client Admin;ClientBuyer:Client Buyer;ClientEngineer:Client Engineer" },
               editrules: { required: true },

               align: 'center'
           },

           { name: 'Phone', index: 'Phone', editable: true, edittype: "text", colmenu: false, coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: true }, align: 'center' },
           {
               name: 'Phone2', index: 'SECONDARY_PHONE_NUMBER', editable: true, edittype: "text", colmenu: false, coloptions: { sorting: true, columns: false, filtering: true, seraching: true, grouping: false, freeze: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, align: 'center'
           },
           {
               name: 'Email', index: 'Email', editable: true, edittype: "text", colmenu: false, coloptions: { sorting: true, columns: false, filtering: true, seraching: true, grouping: false, freeze: true },
               editrules: { required: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, align: 'center'
           },
           {
               name: 'UserName', index: 'UserName', colmenu: false, editable: true, edittype: "text", coloptions: { sorting: true, columns: false, filtering: true, seraching: true, grouping: false, freeze: true },
               editrules: { required: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, align: 'center'
           },
           {
               name: 'PasswordHash', index: 'PasswordHash', hidden: true, colmenu: false, editable: true, edittype: "password", editrules: { edithidden: true, required: true }, hidedlg: true

           },
           {
               name: 'IsActive', index: 'IsActive', editable: true, edittype: "select", editoptions: { value: "1:Active;0:Inactive" }, colmenu: false, coloptions: { sorting: true, columns: false, filtering: true, seraching: true, grouping: false, freeze: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, width: 90, align: 'center'
           },
         { name: 'ID', index: 'ID', key: true, hidden: false, editable: false, formatter: pwResetFormatter, width: 100, align: 'center' },

        ],
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 15,
        loadonce: true, // this is just for the demo
        pager: "#jqGridPager",
        height: 'auto',
        autowidth: true,
        colMenu: false,
        cellEdit: false,

    });


    $("#jqGrid").tooltip();

    //$("#jqGrid").jqGrid().trigger('reloadGrid');
    $("#jqGrid").trigger("reloadGrid", [{ page: 1 }]);

    // hax to stop propogation
    $('a.colmenu').click(function (event) {
        event.preventDefault();
    });

    function pwResetFormatter(cellvalue, options, rowObject) {
        thisCellVal = '<a href="/user/editclientuser/' + rowObject.Id + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Edit</a>';
        return thisCellVal;
    }

    function timezoneFormatter(cellvalue, options, rowObject) {
        var result = $.grep(timezones, function (e) { return e.ID == cellvalue; });
        return result[0]['DESCRIPTION'];
    }

    function roleFormatter(cellvalue) {

        var result = $.grep(roles, function (e) { return e.ID == cellvalue; });
        return result[0]['RoleName'];
    }


});
