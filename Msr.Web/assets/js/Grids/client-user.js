
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

    var roles =  [
            { "ID": "ClientAdmin", "TITLE": "Client Admin" },
            { "ID": "ClientBuyer", "TITLE": "Client Buyer" },
            { "ID": "ClientEngineer", "TITLE": "Client Engineer" },
    ]

    $("#jqGrid").jqGrid({
        url: '/user/ClientUserData',
        mtype: "GET",
        datatype: "json",
        colNames: ['First Name', 'Last Name', 'Full Name', 'TimeZone', 'Created Date', 'Portal Role', 'Primary Phone', '2nd Phone', 'Email', 'User Name','Password', 'Status', 'Actions'],
        colModel: [
           {
               name: 'FirstName', index: 'FirstName', colmenu: true, editable: true, edittype: "text", coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, width: 90, align: 'center'
           },
           {
               name: 'LastName', index: 'LastName', editable: true, edittype: "text", colmenu: false, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true, },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, width: 90, align: 'center'
           },

              {
                  name: 'FullName', index: 'FullName', editable: true, edittype: "text", colmenu: false, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true, },
                  searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, width: 90, align: 'center'
              },

           {
               name: 'TimeZone', index: 'TimeZone', editable: true, edittype: "select", editoptions: { value: "100:Mid-Atlantic;11436:International Date Line West;11437:Midway Island, Samoa" }, colmenu: false, formatter: timezoneFormatter, coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, align: 'center'
           },
           {
               name: 'CreatedDate', index: 'CreatedDate', hidden: false, colmenu: false, sorttype: 'date', coloptions: { sorting: true, columns: true, filtering: true, seraching: true, grouping: false, freeze: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, formatter: 'date', formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" }, width: 90, align: 'center'
           },
           {
               name: 'RoleName', index: 'RoleName', editable: true, edittype: "select",
               editoptions: { value: "ClientAdmin:Client Admin;ClientBuyer:Client Buyer;ClientEngineer:Client Engineer" },
               formatter: roleFormatter,
               align: 'center'
           },
           
           { name: 'Phone', index: 'Phone', editable: true, edittype: "text", colmenu: false, coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: true }, align: 'center' },
           {
               name: 'Phone2', index: 'SECONDARY_PHONE_NUMBER', editable: true, edittype: "text", colmenu: false, coloptions: { sorting: true, columns: false, filtering: true, seraching: true, grouping: false, freeze: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, align: 'center'
           },
           {
               name: 'Email', index: 'Email', editable: true, edittype: "text", colmenu: false, coloptions: { sorting: true, columns: false, filtering: true, seraching: true, grouping: false, freeze: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, align: 'center'
           },
           {
               name: 'UserName', index: 'UserName', colmenu: false, editable: true, edittype: "text", coloptions: { sorting: true, columns: false, filtering: true, seraching: true, grouping: false, freeze: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, align: 'center'
           },
           {
               name: 'PasswordHash', index: 'PasswordHash', hidden: true, colmenu: false, editable: true, edittype: "password", editrules: { edithidden: true }, hidedlg: true
           },
           {
               name: 'IsActive', index: 'IsActive', editable: true, edittype: "select", editoptions: { value: "1:Active;0:Inactive" }, colmenu: false, coloptions: { sorting: true, columns: false, filtering: true, seraching: true, grouping: false, freeze: true },
               searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] }, width: 90, align: 'center'
           },
           { name: 'ID', index: 'ID', key: true, hidden: false, colmenu: false, editable: false, formatter: pwResetFormatter, width: 100, align: 'center' },

        ],
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 15,
        loadonce: true, // this is just for the demo
        pager: "#jqGridPager",
        height: 'auto',
        autowidth: true,
        colMenu: true,
        cellEdit: true,
        cellsubmit: 'clientArray',
        editurl: 'clientArray'
    });

    $('#jqGrid').navGrid("#jqGridPager", {
        search: true, // show search button on the toolbar
        add: true,
        edit: true,
        del: true,
        refresh: true
    },
        {
            editCaption: "The Edit Dialog",
            recreateForm: true,
            checkOnUpdate : true,
            checkOnSubmit : true,
            closeAfterEdit: true,
            errorTextFormat: function (data) {
                return 'Error: ' + data.responseText
            }
        },
                // options for the Add Dialog
                {
                    closeAfterAdd: true,
                    recreateForm: true,
                    url: '/user/addUser',
                    errorTextFormat: function (data) {
                        return 'Error: ' + data.responseText
                    }
                },
                // options for the Delete Dailog
                {
                    errorTextFormat: function (data) {
                        return 'Error: ' + data.responseText
                    }
                },
            { multipleSearch: true }
    );

    $("#jqGrid").tooltip();

    //$("#jqGrid").jqGrid().trigger('reloadGrid');
    $("#jqGrid").trigger("reloadGrid", [{ page: 1 }]);

    // hax to stop propogation
    $('a.colmenu').click(function (event) {
        event.preventDefault();
    });

    function pwResetFormatter(cellvalue, options, rowObject) {
        thisCellVal = '<button class="btn btn-xs btn-danger" style="margin:2px;font-size: .8em;" data-toggle="modal" data-target="#pwResetConfirmModal"><i class="fa fa-key"></i> Reset Password</button>';
        return thisCellVal;
    }

    function timezoneFormatter(cellvalue, options, rowObject) {
        var result = $.grep(timezones, function (e) { return e.ID == cellvalue; });
        return result[0]['DESCRIPTION'];
    }

    function roleFormatter(cellvalue) {
        
        var result = $.grep(roles, function (e) { return e.ID == cellvalue; });
        return result[0]['TITLE'];
    }
    

    //ajax emulation
    $.mockjax({
        url: '/post',
        responseTime: 200
    });



});