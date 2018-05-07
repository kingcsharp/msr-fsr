var FileUploaderGrid = function () {

    var setupSelectImage = function(parameters) {

        //$('#select-images').on('show.bs.modal',
        //    function (event) {

        //        var button = $(event.relatedTarget);
        //        var callBackId = button.data('call-back-id');
        //        var targetCallBackId = button.data('file-select-target-id');
        //        var targetSection = button.data('target-section');
        //        var modal = $(this);

        //        $.ajax({
        //            type: "GET",
        //            url: '/Files/GetFiles?callBackId=' + callBackId + '&targetCallBackId=' + targetCallBackId + '&targetSection=' + targetSection,
        //            dataType: 'html',
        //            success: function (data) {
        //                modal.find('.modal-body').html(data);
        //            },
        //            error: function () {

        //            }
        //        });

        //    });
    }

    var selectIdsForEdit = function () {

        var array = [];
        var objectId = $('#target-control-id').val();
        var elementId = $('#file-select-target-id').val();
        var section = $('#target-section').val();
        var uplaodUrl = $('#target-upload-url').val();

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
                var $el = $('#' + elementId);
                $el.fileinput('destroy');
                
                new FileUploader().InitEditUploader(elementId, uplaodUrl, data.initialPreview, data.initialPreviewConfig, objectId, true, elementId);
            },
            error: function () {
            }
        });
    }

    var selectIdsForAdd = function (fileUploaderUrl) {
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

                new FileUploader().InitUploader($el, fileUploaderUrl, data.initialPreview, data.initialPreviewConfig);
            },
            error: function () {

            }
        });
    }

    return {
        SetupSelectImage: setupSelectImage,
        SelectIds: selectIdsForEdit,
        SelectIdsForAdd: selectIdsForAdd
    }
}

