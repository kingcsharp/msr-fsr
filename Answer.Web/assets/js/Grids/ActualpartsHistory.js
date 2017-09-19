function LoadActualPartViewHistoryGrid(url) {

    //$.jgrid.defaults.responsive = true;
    $.jgrid.defaults.styleUI = 'Bootstrap';

    

    $("#jqGrid").jqGrid({
        url: url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        emptyrecords: 'No records to display',
        datatype: "json",
        colModel: [
            {
                label: 'ID',
                name: 'Id',
                index: 'Id',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 150,
                align: 'left'
            },
            {
                label: 'Description',
                name: 'Description',
                index: 'Description',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 350,
                align: 'left'
            },
          
            {
                label: 'Cur Plan Start Date',
                name: 'CurPlannedStopDate',
                index: 'CurPlannedStopDate',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 130,
                formatter: 'date',
                formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                align: 'left'
            },
            {
                label: 'Actual Start Date',
                name: 'ActualStartDate',
                index: 'ActualStartDate',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 130,
                formatter: 'date',
                formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                align: 'left'
            },
            {
                label: 'Actual Stop Date',
                name: 'ActualStopDate',
                index: 'ActualStopDate',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 130,
                formatter: 'date',
                formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                align: 'left',
            },
           
         
            {
                label: 'Request Date',
                name: 'RequestDate',
                index: 'RequestDate',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 130,
                formatter: 'date',
                formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                align: 'left'
            },
          
     
      
          
        ],
        ajaxRowOptions: {
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        },
        serializeRowData: function (postdata) {
            return JSON.stringify(postdata);
        },
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPager",
        height: 'auto',
        gridview: true,
        sortname: 'Description',
        sortable: true,
        sortorder: 'asc',
        cellEdit: false,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: true,
        loadComplete: function () {
            $('.deleteobject').on('click',
                function (e) {
                    e.preventDefault();

                    var callBackId = $(this).data('call-back-id');
                    var callBackName = $(this).data('call-back-name');

                    eModal.confirm('You will be marking this task with Id ' + callBackName + ' Closed ?', 'Confirmation delete')
                        .then(confirmCallback, optionalCancelCallback);

                    function confirmCallback() {
                        console.log("ok");
                        window.location.href = "/ActualParts/ViewHistoryClose/" + callBackId;
                    }

                    function optionalCancelCallback() {
                        console.log("cancel");
                    }

                });
            $('.deletehistory').on('click',
                function (e) {
                    e.preventDefault();

                    var callBackId = $(this).data('call-back-id');
                    var callBackName = $(this).data('call-back-name');

                    eModal.confirm('Do you really want to delete ' + callBackName + ' ?', 'Confirmation delete')
                        .then(confirmCallback, optionalCancelCallback);

                    function confirmCallback() {
                        window.location.href = "/ActualParts/DeleteHistory/" + callBackId;

                    }

                    function optionalCancelCallback() {
                    }

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
        {},  // edit options
        {}, // add options
        {}, // delete options
        { multipleSearch: true }
    );

    function actualPartVIewHistoryFormatter(cellvalue, options, rowObject) {
        var thisCellVal = '<a href="/ActualParts/ViewHistoryClose/' + rowObject.Id + '" data-call-back-name="' + rowObject.Id + '" data-call-back-id="' + rowObject.Id + '" title="Close" class="btn btn-xs btn-danger deleteobject" style="margin:2px;font-size: .8em;"><i class="fa fa-check" aria-hidden="true"></i></a>';
        thisCellVal = thisCellVal + '<a href="/ActualParts/ReassignTask/' + rowObject.Id + '" class="btn btn-xs btn-success" title="Reassign" style="margin:2px;font-size: .8em;"><i class="fa fa-hand-o-right" aria-hidden="true"></i></a>';
        thisCellVal = thisCellVal + '<a href="#/' + rowObject.Id + '" class="btn btn-xs btn-success" title="View Procedure" style="margin:2px;font-size: .8em;"><i class="fa fa-book" aria-hidden="true"></i></a>';
        thisCellVal = thisCellVal + '<a href="/ActualParts/DeleteHistory/' + rowObject.Id + '" data-call-back-name="' + rowObject.Id + '" data-call-back-id="' + rowObject.Id + '" class="btn btn-xs btn-danger deletehistory" title="Delete" style="margin:2px;font-size: .8em;"><i class="fa fa-trash" aria-hidden="true"></i></a>';
        thisCellVal = thisCellVal + '<a href="#/' + rowObject.Id + '" class="btn btn-xs btn-success" title="View Hierarchy" style="margin:2px;font-size: .8em;"><i class="fa fa-chain-broken" aria-hidden="true"></i></a>';
        thisCellVal = thisCellVal + '<a href="/ShowPurchaseStatus/Index/' + rowObject.Id + '" class="btn btn-xs btn-success" title="show Purchase Status" style="margin:2px;font-size: .8em;"><i class="fa fa-money" aria-hidden="true"></i></a>';
        thisCellVal = thisCellVal + '<a href="#/' + rowObject.Id + '" class="btn btn-xs btn-success" title="View Details" style="margin:2px;font-size: .8em;"><i class="fa fa-eye" aria-hidden="true"></i></a>';
        thisCellVal = thisCellVal + '<a href="#/' + rowObject.Id + '" class="btn btn-xs btn-success" title="Vie a Service Report for this Task" style="margin:2px;font-size: .8em;"><i class="fa fa-flag" aria-hidden="true"></i></a>';

        return thisCellVal;
    }
    function linkFormatter(cellvalue, options, rowObject) {

        var thisCellVal = '<a href="#/' + rowObject.RequesteeId + '">' + cellvalue + '</a>';

        return thisCellVal;
    }
    $('#search').click(function () {
        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");
    });
}