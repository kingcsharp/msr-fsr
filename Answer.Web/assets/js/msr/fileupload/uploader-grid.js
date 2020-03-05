var FileUploaderGrid = function () {
    var service = {
        SelectIds: selectIdsForEdit,
        SelectIdsForAdd: selectIdsForAdd
    }
    return service;

    function selectIdsForEdit() {

        var array = [];
        var objectId = $('#target-control-id').val();
        var elementId = $('#file-select-target-id').val();
        var section = $('#target-section').val();
        var uplaodUrl = $('#target-upload-url').val();

        if (elementId == "") {
            var btnData = $('#' + section + ' .select-file-button').data();
            if (btnData !== null && btnData.selectUrl == "/Doc/FileUploaderForWipTask") {
                objectId = btnData.callBackId;
                elementId = btnData.fileSelectTargetId;
                uplaodUrl = btnData.selectUrl;
                section = 'WIP_TASK_STEP';
            }
        }

        $(".selected-file").each(function (index) {
            if ($(this).is(":checked")) {
                var ids = $(this).val().split('|');
                array.push(ids[0]);
            }
        });

        $('.closeClick').click();

        $.ajax({
            type: "GET",
            url: '/Documents/AddsingleReference?linkDocId=' + objectId + '&files=' + array + '&section=' + section,
            dataType: 'JSON',
            success: function (data) {
                debugger;
                //var $el = $('#' + elementId);
                //$el.fileinput('destroy');
                //$el.off("filebeforedelete");
                var elem = $('#' + elementId.split(' ')[0]);
                elem.empty();
                elem.append(`<div class="file-loading">
                            <input id = "input-files" name = "input-files[]" type = "file" multiple />
                            </div>
                            <input type="hidden" id="referenceFiles" name="referenceFiles" />`)
                new FileUploader().InitEditUploader(elementId, uplaodUrl, data.initialPreview, data.initialPreviewConfig, objectId, true, elementId);

            },
            error: function () {
            }
        });
    }

    function selectIdsForAdd(fileUploaderUrl) {
        var array = [];
        var preSelected = [];

        if ($('#referenceFiles').val().length > 0) {
            preSelected = $('#referenceFiles').val().split(',').map(Number);
        }

        $(".selected-file").each(function (index) {

            if ($(this).is(":checked")) {
                var ids = $(this).val().split('|');
                if ($.inArray(ids[0], array) !== -1) {
                    // found it
                } else {
                    array.push(parseInt(ids[0]));
                }
            }
        });

        Array.prototype.unique = function () {
            var a = this.concat();
            for (var i = 0; i < a.length; ++i) {
                for (var j = i + 1; j < a.length; ++j) {
                    if (a[i] === a[j])
                        a.splice(j--, 1);
                }
            }

            return a;
        };

        var finalArray = array.concat(preSelected).unique();

        $('.closeClick').click();

        $('#referenceFiles').val(finalArray);

        $.ajax({
            type: "GET",
            url: '/Doc/RefillUploader?ids=' + finalArray,
            dataType: 'JSON',
            success: function (data) {
                var $el = $('#input-files');
                $el.fileinput('destroy');
                $el.off("filebeforedelete");
                new FileUploader().InitUploader($el, fileUploaderUrl, data.initialPreview, data.initialPreviewConfig);
            },
            error: function () {

            }
        });
    }
}

