function LoadActualPartsGrid(url, search) {
    //$.jgrid.defaults.responsive = true;
    $.jgrid.defaults.styleUI = 'Bootstrap';

    Smooch.init({ appToken: '9wxoxi2wbcbymhjf1ex1a0dux' });

    $("#jqGrid").jqGrid({
        url: url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        emptyrecords: 'No records to display',
        datatype: "json",
        colModel: [
            {
                label: 'Database ID',
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
                label: 'Serial Num',
                name: 'Serial',
                index: 'Serial',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { defaultValue: search },
                width: 200,
                align: 'left',
                formatter: serialFormatter
            },
            {
                label: 'Part Description',
                name: 'PartDesc',
                index: 'PartDesc',
                colmenu: true,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 350,
                align: 'left'
            },
            {
                label: 'Company Part Number',
                name: 'CompanyPartNumber',
                index: 'CompanyPartNumber',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 300,
                align: 'left'
            },
            {
                label: 'Qty',
                name: 'Qty',
                index: 'Qty',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 80,
                align: 'left'
            },
            {
                label: 'Location Name',
                name: 'LocationName',
                index: 'LocationName',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 250,
                align: 'left',
                formatter: locationFormatter
            },
            {
                label: 'Current Owner',
                name: 'CurrentOwnerName',
                index: 'CurrentOwnerName',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 150,
                align: 'left'
            },
            {
                label: 'Part Status',
                name: 'ApStatus',
                index: 'ApStatus',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 150,
                align: 'left'
            },
            {
                label: 'Revision',
                name: 'Rev',
                index: 'Rev',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 80,
                align: 'left'
            },
            {
                label: 'Approval Status',
                name: 'Status',
                index: 'Status',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                stype: "select",
                searchoptions: { value: ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Creating or Approved;CREATING, DENIED:Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete" },
                width: 150,
                align: 'left'
            },
            {
                label: 'Checked out to',
                name: 'CreatingCoName',
                index: 'CreatingCoName',
                colmenu: false,
                editable: true, // must set editable to true if you want to make the field editable
                editrules: { required: true },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 200,
                align: 'left'
            },
            { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: actualPartsEditFormatter, width: 200, align: 'center' }
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
        rowNum: 10,
        loadonce: false, // this is just for the demo
        pager: "#jqGridPager",
        height: 'auto',
        gridview: true,
        sortname: 'PartDesc',
        sortable: true,
        sortorder: 'asc',
        cellEdit: false,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: true,
        loadComplete: function () {
            console.log(search);
            $('#jqGrid').jqGrid('filterToolbar', {
                stringResult: true,
                searchOnEnter: true,
                searchOperators: true
            });
            $('.deleteactualPart').on('click',
                function (e) {
                    e.preventDefault();

                    var callBackId = $(this).data('call-back-id');
                    var callBackName = $(this).data('call-back-name');

                    eModal.confirm('Do you really want to delete ' + callBackName + ' ?', 'Confirmation delete')
                        .then(confirmCallback, optionalCancelCallback);

                    function confirmCallback() {
                        console.log("ok");
                        window.location.href = "/ActualParts/ActualPartDelete/" + callBackId;
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

    function actualPartsEditFormatter(cellvalue, options, rowObject) {
        var thisCellVal = '<a href="/ActualParts/Edit/' + rowObject.ObjectId + '" class="btn btn-xs btn-success" title="Edit" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i> Edit</a>';
        thisCellVal = thisCellVal + '<a href="/ProductsActualParts/Index/' + rowObject.ObjectId + '" class="btn btn-xs btn-success" title="Show me Products for this part" style="margin:2px;font-size: .8em;"><i class="fa fa-money" aria-hidden="true"></i> Parts</a>';
        thisCellVal = thisCellVal + '<a href="/ActualParts/ViewHistory/' + rowObject.ObjectId + '" class="btn btn-xs btn-success" title="Edit" style="margin:2px;font-size: .8em;"><i class="fa fa-history" aria-hidden="true"></i> History</a>';
        thisCellVal = thisCellVal + '<a href="/ActualParts/ActualPartDelete/' + rowObject.ObjectId + '" data-call-back-name="' + rowObject.Serial + '" data-call-back-id="' + rowObject.ObjectId + '" title="Delete" class="btn btn-xs btn-danger deleteactualPart" style="margin:2px;font-size: .8em;"><i class="fa fa-trash"></i> Delete</a>';
        return thisCellVal;
    }
    function serialFormatter(cellvalue, options, rowObject) {

        var thisCellVal = '<a href="/ActualParts/Index/?serial=' + cellvalue + '">' + cellvalue + '</a>';

        return thisCellVal;
    }
    function locationFormatter(cellvalue, options, rowObject) {

        var thisCellVal = '<a href="/Locations/edit/' + rowObject.LocationObjectId + '">' + cellvalue + '</a>';

        return thisCellVal;
    }
    $('#search').click(function () {
        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");
    });
}
