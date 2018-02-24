function SetUpGrid(returnUrl) {

    $("#jqGrid").jqGrid({
        url: '/Help/HelpsData',
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            {
                label: 'Id',
                name: 'Id',
                index: 'Id',
                key: true,
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 100,
                align: 'left'
            },
            {
                label: "Title",
                name: 'Title',
                index: 'Title',
                colmenu: true,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 300,
                align: 'left'
            },
            {
                label: "Friendly Url",
                name: 'FriendlyUrl',
                index: 'FriendlyUrl',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: true, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left',
                hidedlg: false
            },
           
            {
                label: 'Roles',
                name: 'RoleName',
                index: 'RoleName',
                colmenu: false,
                width: 150,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            {
                label: 'Category',
                name: 'Category',
                index: 'Category',
                colmenu: false,
                width: 100,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'left'
            },
            
            { name: 'Actions', index: 'Id', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: HelpEditFormatter, width: 200, align: 'center' }
        ],

        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPager",
        height: 'auto',
        gridview: true,
        sortname: 'Id',
        sortable: true,
        sortorder: 'asc',
        cellEdit: true,
        cellsubmit: 'clientArray',
        editurl: 'clientArray',
        autowidth: true,
        colMenu: true,
        gridComplete: function () {
            $('.helpDelete').on('click',
                function (e) {
                    e.preventDefault();

                    var callBackId = $(this).data('call-back-id');
                    var callBackName = $(this).data('call-back-name');

                    eModal.confirm(
                            'Pressing OK will delete this?')
                        .then(confirmCallback, optionalCancelCallback);

                    function confirmCallback() {
                        window.location.href = "/Help/Delete/" + callBackId;
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
        {}, // edit options
        {}, // add options
        {}, // delete options
        { multipleSearch: true }
    );
    $('#jqGrid').jqGrid('filterToolbar', {
        stringResult: true,
        searchOnEnter: true,
        searchOperators: true
    });

    function HelpEditFormatter(cellvalue, options, rowObject) {

        var editButton = '<a  title="Edit" href="/help/Edit/' + rowObject.Id + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';

        var deleteButton = '<a href="/Help/Delete/' + rowObject.Id + '" data-call-back-name="' + rowObject.Title + '"  data-call-back-id="' + rowObject.Id + '" class="btn btn-xs btn-danger helpDelete" title="Cancel Creation. Edit will be lost" style="margin:2px;font-size: .8em;"><i class="fa fa fa-trash-o"></i></a>';
      
        return editButton + deleteButton;
    }


    $('#search').click(function () {

        jQuery("#jqGrid").setGridParam({
            page: 1
        }).trigger("reloadGrid");

    });


}
