
function LoadSelectProcedureGrid(parentId, fillId, procedureType, customerEmailForNotification) {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    $("#jqGridFiles").jqGrid({
        url: '/wip/AddNcrModelData',
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            {
                label: ' #',
                name: 'Name',
                index: 'Name',
                key: true,
                colmenu: false,

                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 150,
                align: 'center',

            },
            {
                label: 'Company',
                name: 'Creating_Co_Name',
                index: 'Creating_Co_Name',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'center',
            },
            {
                label: 'Root',
                name: 'Root',
                index: 'Root',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'center'
            },
            {
                label: 'Procedure Type',
                name: 'Verb_Name',
                index: 'Verb_Name',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { defaultValue: procedureType },
                align: 'center'
            },
            { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: ActionFormatter, width: 200, align: 'center' }
        ],
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPagerFiles",
        height: 'auto',
        gridview: true,
        sortname: 'Name',
        sortable: true,
        sortorder: 'asc',
        cellEdit: false,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: true,
        key: true,
        ajaxCellOptions: {},
        gridComplete: function () {
            $('.ui-jqgrid').unblock();
            $(".showLoadingSpinner").on('click', function (arg) {

                
                if (arg.target.parentNode.dataset['addprocedure'] != null) {
                    var addProcedureLink = arg.target.parentNode.dataset['addprocedure'];
                    

                    var customerEmail = arg.target.parentNode.dataset['customeremailfornotification'];

                    if (customerEmail === "") {

                        $.ajax({
                            type: "GET",
                            url: "/wip/GetNcrNotificationModal",
                            dataType: 'html',
                            success: function (data) {
                                $('#addNcrModal').modal('hide');
                                $("#notifyCustomerNcrWasAddedModal  div.modal-body").html(data);
                                $('#notifyCustomerNcrWasAddedModal').modal('show');
                                setTimeout(function() {
                                        $('#addProcedureWithEmailNotification').on('click',
                                            function(args) {
                                                window.location = addProcedureLink;
                                            });
                                    },
                                    300);
                            },
                            error: function (error) {
                                
                            }
                        });
                        

                    } else {

                        window.location = addProcedureLink;

                    }


                } else {
                    $('#addNcrModal').modal('hide');
                }

            });

        },
        beforeRequest: function () {
            $('.ui-jqgrid').block({ message: '<h1><img src="/assets/img/nice_loader.gif" />  Loading. Please wait...</h1>' });
        }
    });
    $('#jqGridFiles').navGrid("#jqGridPagerFiles", {
        refresh: true,
        search: false, // show search button on the toolbar
        add: false,
        edit: false,
        del: false,

    },
        {}, // edit options
        {}, // add options
        {}, // delete options
        { multipleSearch: true }
    );
    $('#jqGridFiles').jqGrid('filterToolbar', {
        stringResult: true,
        searchOnEnter: true,
        searchOperators: true
    });

    function ActionFormatter(cellvalue, options, rowObject) {
        var attachStep = '<a title="Add Procedure as a Sub Task" data-customeremailfornotification="'+customerEmailForNotification+'" data-addprocedure="/wip/AddProcedureToTask?objId=' + rowObject.Id + '&parentId=' + parentId + '&fillId=' + fillId + '" class="btn btn-xs btn-success showLoadingSpinner" style="margin:2px;font-size: .8em;"><i class="fa fa-link"></i></a>';

        return attachStep;
    }
}

