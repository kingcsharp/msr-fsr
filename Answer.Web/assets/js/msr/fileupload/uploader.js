var FileUploader = function () {

    var initEditUploader = function (element, url, initialPreview, initialPreviewConfig, objectId, showSelect = true) {

        element.fileinput({
            showSelect: showSelect,
            showClose: false,
            uploadUrl: url,
            uploadAsync: true,
            minFileCount: 1,
            maxFileCount: 10,
            overwriteInitial: false,
            initialPreview: initialPreview,
            initialPreviewAsData: true,
            initialPreviewFileType: 'image',
            initialPreviewConfig: initialPreviewConfig,
            uploadExtraData: {
                objectId: objectId
            }
        }).on('filesorted',

            function (e, params) {
                console.log('File sorted params', params);
            }).on('fileuploaded',
            function (e, params) {
                console.log('File uploaded params', params);
            }).on('filebeforedelete',
            function () {
                return new Promise(function (resolve) {

                    $.confirm({
                        title: 'Confirmation!',
                        content: 'Are you sure you want to delete this file?',
                        type: 'red',
                        buttons: {
                            ok: {
                                btnClass: 'btn-primary text-white',
                                keys: ['enter'],
                                action: function () {
                                    resolve();
                                }
                            },
                            cancel: function () {
                                //$.alert('File deletion was aborted! ' + getCount(element));
                            }
                        }
                    });
                });
            }).on("filebatchselected", function () {
                //element.fileinput("upload");
            });

    };

    var initUploader = function (element, url, initialPreview, initialPreviewConfig) {
        element.fileinput({
            showSelect: true,
            showClose: false,
            uploadUrl: url,
            uploadAsync: true,
            minFileCount: 1,
            maxFileCount: 10,
            overwriteInitial: false,
            initialPreview: initialPreview,
            initialPreviewAsData: true,// defaults markup
            initialPreviewFileType: 'image',// image is the default and can be overridden in config below
            initialPreviewConfig: initialPreviewConfig
        }).on('filesorted',
            function (e, params) {
                console.log('File sorted params', params);
            }).on('fileuploaded',
            function (e, params) {
                console.log('File uploaded params', params.response);
                var array = [];
                var preSelected = [];
                if ($('#referenceFiles').val().length > 0) {
                    preSelected = $('#referenceFiles').val().split(',').map(Number);
                }
                array.push(parseInt(params.response.Id));
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
                $('#referenceFiles').val(finalArray);
            }).on('filebeforedelete', function (event, key) {
                return new Promise(function (resolve) {
                    $.confirm({
                        title: 'Confirmation!',
                        content: 'Are you sure you want to delete this file?',
                        type: 'red',
                        buttons: {
                            ok: {
                                btnClass: 'btn-primary text-white',
                                keys: ['enter'],
                                action: function () {
                                    var array = $('#referenceFiles').val().split(',').map(Number);

                                    console.log(array);
                                    // Remove item Key from array
                                    var filteredAry = array.filter(function (item) {
                                        return item !== key;
                                    });
                                    console.log(filteredAry);
                                    $('#referenceFiles').val(filteredAry);
                                    resolve();
                                }
                            },
                            cancel: function () {
                                //$.alert('File deletion was aborted! ' + getCount(element));
                            }
                        }
                    });
                });
            }).on("filebatchselected", function () {
                //element.fileinput("upload");
            }).on('filepredelete', function (event, key) {
                console.log('Key = ' + key);
            });;

    };

    var setupSelectImage = function (parameters) {

        $('#select-images').on('show.bs.modal',
            function (event) {

                var button = $(event.relatedTarget);
                var callBackId = button.data('call-back-id');
                var modal = $(this);

                $.ajax({
                    type: "GET",
                    url: '/Files/GetFiles?callBackId=' + callBackId,
                    dataType: 'html',
                    success: function (data) {
                        modal.find('.modal-body').html(data);
                    },
                    error: function () {

                    }
                });

            });
    }

    var getCount = function (id) {
        var cnt = id.fileinput('getFilesCount');
        return cnt === 0 ? 'You have no files remaining.' :
            'You have ' + cnt + ' file' + (cnt > 1 ? 's' : '') + ' remaining.';
    };

    return {
        InitEditUploader: initEditUploader,
        InitUploader: initUploader,
        SetupSelectImage: setupSelectImage
    }
}

// This will be called on select image in a dialog

function SelectIds(parameters) {

    var array = [];
    var objectId = $('#ObjectId').val();

    $(".selected-file").each(function (index) {

        if ($(this).is(":checked")) {
            var ids = $(this).val().split('|');
            array.push(ids[0]);
        }
    });

    $('.closeClick').click();

    $.ajax({
        type: "GET",
        url: '/Documents/AddsingleReference?linkDocId=' + objectId + "&files=" + array,
        dataType: 'html',
        success: function (data) {
            location.reload();
        },
        error: function () {

        }
    });
}
