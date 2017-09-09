function LoadShowPartsProvidedTakeBackGrid(url, Pid, RELATIONSHIP) {
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
                label: 'TYPE',
                name: 'OBJ_TABLE',
                index: 'OBJ_TABLE',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 100,
                align: 'center'
            },
            {
                label: 'NAME',
                name: 'OBJ_DESC',
                index: 'OBJ_DESC',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 200,
                align: 'center'
            },
            {
                label: 'QTY',
                name: 'QTY',
                index: 'QTY',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 200,
                align: 'center'
            },
            {
                label: 'ORDERING UNIT',
                name: 'QTY_TYPE',
                index: 'QTY_TYPE',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 200,
                align: 'center'
            },

            { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: procedureObjectsEditFormatter, width: 100, align: 'center' }
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
        sortname: 'APPROVED_OBJECT_ID',
        sortable: true,
        sortorder: 'asc',
        cellEdit: false,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: true,
        gridComplete: function () {
            $('.deleteprocedureobject').on('click',
                function (e) {
                    e.preventDefault();

                    var callBackId = $(this).data('call-back-id');
                    var callBackName = $(this).data('call-back-name');

                    eModal.confirm(
                        'Do you really want to delete ' + callBackName + ' ? ', 'Confirmation delete')
                        .then(confirmCallback, optionalCancelCallback);

                    function confirmCallback() {
                        console.log("ok");
                        window.location.href = "/Procedures/DeleteProcedureObject/" + callBackId;
                    }

                    function optionalCancelCallback() {
                        console.log("cancel");
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
    $('#jqGrid').jqGrid('filterToolbar', {
        stringResult: true,
        searchOnEnter: true,
        searchOperators: true
    });
    function procedureObjectsEditFormatter(cellvalue, options, rowObject) {
        var thisCellVal = '<a href="/Procedures/EditPartsProvideTakeBack/' + rowObject.ID + "," + RELATIONSHIP + "," + Pid + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Edit</a>';
        thisCellVal = thisCellVal + '<a href="/Procedures/DeleteProcedureObject/' + rowObject.ObjectId + '" data-call-back-id ="' + rowObject.ID + '"  class="btn btn-xs btn-danger deleteprocedureobject" style="margin:2px;font-size: .8em;"><i class="fa fa-delete"></i>DELETE</a>';
        return thisCellVal;
    }

    $('#search').click(function () {
        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");
    });
}