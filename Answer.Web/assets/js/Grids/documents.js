function LoadDocumentsGrid(url, returnUrl) {
    $("#jqGrid").jqGrid({
        url: url,
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
                align: 'center',
            },
            {
                label: 'Name',
                name: 'Name',
                index: 'Name',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                width: 250,
                align: 'center'
            },
            {
                label: 'Creating Co',
                name: 'CreatingCoName',
                index: 'CreatingCoName',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'center'
            },
            {
                label: 'Creating Dept Name',
                name: 'DeptName',
                index: 'DeptName',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'center'
            },
            {
                label: 'Security Level',
                name: 'SecurityLevel',
                index: 'SecurityLevel',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'center'
            },
            {
                label: 'Revision',
                name: 'Rev',
                index: 'Rev',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'center'
            }
            ,
            {
                label: 'Approval Status',
                name: 'Status',
                index: 'Status',
                colmenu: false,
                stype: "select",
                searchoptions: { value: ":[All];CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Creating or Approved;CREATING, DENIED:Creating;IN_WORKFLOW:In Approval Workflow;APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;DENIED:Denied;APPROVED_BUT_REVISING:Approved But Being Revised;APPROVED_BUT_DELETING:Approved But Being Deleted;DENIED:Denied;DELETED:Deleted;OLD:Obsolete" },
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                align: 'center'
            },
            {
                label: 'Approval Date',
                name: 'ApprovalDate',
                index: 'ApprovalDate',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                formatter: 'date',
                formatoptions: { srcformat: "m/d/Y H:i", newformat: "m/d/Y" },
                align: 'center'
            },
            {
                label: 'Checked Out To',
                name: 'LockedByName',
                index: 'LockedByName',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                align: 'center'
            },
            {
                label: 'Reference Files',
                name: 'ReferenceFiles',
                index: 'ReferenceFiles',
                colmenu: false,
                coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                formatter: documentRefFilesFormatter,
                align: 'center'
            },
            { name: 'Actions', index: 'ID', key: true, search: false, hidden: false, colmenu: false, editable: false, formatter: documentEditFormatter, width: 100, align: 'center' }
        ],
        viewrecords: true, // show the current page, data rang and total records on the toolbar
        rowNum: 10, rowList: [10, 20, 50, 100],
        loadonce: false, // this is just for the demo
        pager: "#jqGridPager",
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
        ajaxCellOptions: {}

    });
    $('#jqGrid').navGrid("#jqGridPager", {
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
    $('#jqGrid').jqGrid('filterToolbar', {
        stringResult: true,
        searchOnEnter: true,
        searchOperators: true
    });

    function documentEditFormatter(cellvalue, options, rowObject) {

        var editButton = '<a  title="Edit" href="/Documents/Edit/' + rowObject.ObjectId + '" class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';

        var deleteButton = '';
        var buttonWorkflowLeft = '';
        var buttonWorkflowRight = '';
        var url = '';

        if (rowObject.Status === 'CREATING') {

            url = '/workflow/submit?objId=' + rowObject.ObjectId + '&returnUrl=' + returnUrl;
            buttonWorkflowLeft = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '"  data-call-back-id="' + rowObject.ObjectId + '" class="btn btn-xs btn-success unlock" title="Cancel Creation. Edit will be lost" style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-left"></i></a>';

            buttonWorkflowRight = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-success" title="Proceed to approval workflow for release." style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-right"></i></a>';
        } else {
            url = '/workflow/delete?objId=' + rowObject.ObjectId + '&returnUrl=' + returnUrl;
            deleteButton = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-danger" title="Proceed to delete." style="margin:2px;font-size: .8em;"><i class="fa fa fa-trash-o"></i></a>';
        }

        return editButton + deleteButton + buttonWorkflowLeft + buttonWorkflowRight;

    };
    function documentRefFilesFormatter(cellvalue, options, rowObject) {
        var imageUrl = "";
        var imageUrls = "";
        if (cellvalue != null) {

            var items = cellvalue.split(',');

            for (var i = 0; i <= items.length - 1; i++) {

                var valueId = items[i].split('|')

                switch (valueId[0].split('.').pop()) {

                    case 'xls': imageUrl = '<a  title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/icon-xls.png" /></a>&nbsp';
                        break;

                    case 'jpg':
                    case 'png':
                    case 'jpeg':
                    case 'gif':
                        imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/jpg.png" /></a>&nbsp';
                        break;

                    case 'docx':
                        imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/icon-doc.png" /></a>&nbsp';
                        break;

                    case 'xlsx':
                        imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/icon-xls.png" /></a>&nbsp';
                        break;

                    case 'ppt':
                        imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/icon-ppt.png" /></a>&nbsp';
                        break;

                    case 'pdf':
                        imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/icon-pdf.png" /></a>&nbsp';
                        break;

                    case 'txt':
                        imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/txt.png" /></a>&nbsp';
                        break;

                    case 'zip':
                        imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/icon-zip.png" /></a>&nbsp';
                        break;

                    default: imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/default.png" /></a>&nbsp';
                        break;

                }

                imageUrls += imageUrl;
            }
            return imageUrls;

        }
        else {
            imageUrls = '';
            return imageUrls;
        }

    };

}
