
function LoadFilesGrid() {
        $.jgrid.defaults.styleUI = 'Bootstrap';        
        $("#jqGridFile").jqGrid({
            url: '/Files/FilesData',
            mtype: "GET",
            styleUI: 'Bootstrap',
            datatype: "json",
            colModel: [
                {
                    label: ' #',
                    name: 'Id',
                    index: 'Id',
                    key: true,
                    colmenu: false,
                    search:false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 30,
                    align: 'center',
                    formatter: selectFormatter2
                },
                {
                    label: ' #',
                    name: 'Id',
                    index: 'Id',
                    key: true,
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 30,
                    align: 'center',
                },
                {
                    label: 'Name',
                    name: 'Name',
                    index: 'Name',
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    width: 150,
                    align: 'center'
                },
                {
                    label: 'Keywords',
                    name: 'Description',
                    index: 'Description',
                    colmenu: false,
                    coloptions: { sorting: false, columns: true, filtering: false, seraching: false, grouping: false, freeze: false },
                    searchoptions: { searchOperMenu: false, sopt: ['eq', 'gt', 'lt', 'ge', 'le'] },
                    align: 'center'
                }
            ],
            viewrecords: true, // show the current page, data rang and total records on the toolbar
            rowNum: 10,
            loadonce: false, // this is just for the demo
            pager: "#jqGridPagerFile",
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
        $('#jqGridFile').navGrid("#jqGridPagerFile", {
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
        $('#jqGridFile').jqGrid('filterToolbar', {
            stringResult: true,
            searchOnEnter: true,
            searchOperators: true
        });

    }

function selectFormatter2(cellvalue, options, rowObject) {
    var html = "";

    html = '<input class="selected-file" type="checkbox" onclick="getRecord(this)" value="' + cellvalue + '|' + rowObject.Name + '" />';

       return html;
}
function removeOldFile(e) {
    
    var control = $(e).closest(".modal-body").find('#target-control-id').val();
    $('#' + control).empty();
};
function getRecord(e) {
    removeOldFile(e);
    SelectId();
    $('.closeClick').click();
}
    
function SelectId() {

    $(".selected-file").each(function (index) {

        if ($(this).is(":checked")) {
            var ids = $(this).val().split('|');

            var callBackId = $(this).closest(".modal-body").find('#target-control-id').val();
            var options = $('#' + callBackId + '')[0].options;

            var optionsArray = $.map(options, function (elem) {
                return (elem.value);
            });

            if (optionsArray.length > 0) {

                if ($.inArray(ids[0], optionsArray) != -1) {
                    // found it
                }
                else {
                    $('#' + callBackId + '').append($('<option></option>').val(ids[0]).html(ids[1]));
                    $('#' + callBackId + ' option').prop('selected', true);
                }

            }
            else {
                $('#' + callBackId + '').append($('<option></option>').val(ids[0]).html(ids[1]));
                $('#' + callBackId + ' option').prop('selected', true);
            }
        }

    });
    $('.closeClick').click();
}
