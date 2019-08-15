function eLoaderOpen() {
    var model = $("#emodel");
    model.find('.modal-body').empty().append('<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4"> Operations are in progress, please wait</span>');
    model.modal({
        backdrop: 'static',
        keyboard: false
    });
}

function eLoaderClose() {
    $("#emodel").modal('hide');
}

function eLoaderError(value) {
    var model = $("#emodel");
    model.find('.modal-body').empty().append(value);
}

$('.page-help').on('click', function () {

    var pageUrl = $(this).data('page-url');

    var options = {
        url: '/Help/GetHelpDetails?pageUrl=' + pageUrl,
        title: 'Help',
        size: eModal.size.lg,
    };

    eModal.ajax(options);
}
);

function SelectOldIds() {

    $(".selected-files").each(function (index) {

        if ($(this).is(":checked")) {
            var ids = $(this).val().split('|');

            var callBackId = $(this).closest(".modal-body").find('#target-control-id').val();
            var options = $('#' + callBackId + '')[0].options;

            var optionsArray = $.map(options, function (elem) {
                return (elem.value);
            });

            if (optionsArray.length > 0) {

                if ($.inArray(ids[0], optionsArray) !== -1) {
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

} function UpdateDocumentIds() {

    $(".selected-document-id").each(function (index) {

        if ($(this).is(":checked")) {
            var ids = $(this).val().split('|');

            var callBackId = $(this).closest(".modal-body").find('#target-control-id').val();
            var options = $('#' + callBackId + '')[0].options;

            var optionsArray = $.map(options, function (elem) {
                return (elem.value);
            });

            if (optionsArray.length > 0) {
                if ($.inArray(ids[0], optionsArray) !== -1) {
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

$('#select-companies').on('show.bs.modal',

    function (event) {

        var button = $(event.relatedTarget);
        var callBackId = button.data('call-back-id');
        var modal = $(this);

        $.ajax({
            type: "GET",
            url: '/Companies/GetCompanies?callBackId=' + callBackId,
            dataType: 'html',
            success: function (data) {
                modal.find('.modal-body').html(data);
            },
            error: function (error) {
                eLoaderError(error);
            }
        });
    });

$('.deletefiles').click(function () {

    var callBackId = $(this).data('call-back-id');

    eModal.confirm('Do you really want to delete ' + callBackId + ' ?', 'Confirmation delete')
        .then(confirmCallback, optionalCancelCallback);

    function confirmCallback() {
        console.log("ok");
        $('#' + callBackId + ' option:selected').remove();
    }

    function optionalCancelCallback() {
        console.log("cancel");
    }
});

function SelectCompaniesIds() {

    $(".selected-company").each(function () {

        if ($(this).is(":checked")) {
            var ids = $(this).val().split('|');

            var callBackId = $(this).closest(".modal-body").find('#target-control-id').val();
            var options = $('#' + callBackId + '')[0].options;

            var optionsArray = $.map(options, function (elem) {
                return (elem.value);
            });

            if (optionsArray.length > 0) {

                if ($.inArray(ids[0], optionsArray) !== -1) {
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

$(document).ready(function () {

    $('#select-doc-view').on('show.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var imageType = button.data('file-type');
            var modal = $(this);

            $.ajax({
                type: "GET",
                url: '/doc/GetFileView?callBackId=' + callBackId + '&type=' + imageType,
                dataType: 'html',
                cache: false,
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });

        });

    $('#select-doc_prepro_ref_file-view').on('show.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var imageType = button.data('file-type');
            var modal = $(this);
        });

    $('#select-doc_prepro-view').on('show.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var imageType = button.data('file-type');
            var modal = $(this);

            $.ajax({
                type: "GET",
                url: '/doc/GetFileProcedureView?callBackId=' + callBackId,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });

        });

    $('#select-referenceprocedures').on('show.bs.modal',
        function (event) {

            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var modal = $(this);

            $.ajax({
                type: "GET",
                url: '/Procedures/GetProcedures?callBackId=' + callBackId,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });

        });

    $('#select-referenceproceduresview').on('show.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var modal = $(this);

            $.ajax({
                type: "GET",
                url: '/Procedures/GetProceduresView?callBackId=' + callBackId,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });

        });

    $('#select-images').on('shown.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var targetCallBackId = button.data('file-select-target-id');
            var targetSection = button.data('target-section');
            var uplaodUrl = button.data('select-url');
            var modal = $(this);
            $.ajax({
                type: "GET",
                url: '/Files/GetFiles?callBackId=' + callBackId + '&targetSection=' + targetSection + '&targetUplaodUrl=' + uplaodUrl + '&targetCallBackId=' + encodeURIComponent(targetCallBackId),
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {
                }
            });
        });

    $('#select-images').on('show.bs.modal', function (event) {
        clearModal($(this));
    });

    $('#select-objects').on('shown.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var modal = $(this);
            $.ajax({
                type: "GET",
                url: '/Objects/GetObjects?callBackId=' + callBackId,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {
                }
            });
        });

    $('#select-objects').on('show.bs.modal', function (event) {
        clearModal($(this));
    });


    $('#select-theory').on('shown.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var modal = $(this);
            $.ajax({
                type: "GET",
                url: '/TheoryParagraph/GetTheoryParagraphs?callBackId=' + callBackId,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });
        });

    $('#select-theory').on('show.bs.modal', function (event) {
        clearModal($(this));
    });

    function clearModal(modal) {
        modal.find('.modal-body').empty();
    }

    $("form").submit(function (e) {
        $('.submitselect option').prop('selected', true);
    });

    $('#support-model').on('show.bs.modal',
        function (event) {

            var modal = $(this);

            $.ajax({
                type: "Get",
                url: '/Help/SupportTicket',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });
        });

    $("#support-ticket").click(function (e) {

        e.preventDefault();
        var form = $(this).closest("form");
        var isvalid = form.valid();

        if (isvalid) {
            eLoaderOpen();
            $.ajax({
                type: "POST",
                url: '/Help/SupportRequest/',
                dataType: 'Json',
                data: $('.support-form').serialize(),
                success: function (data) {
                    if (data === 'OK') {
                        $('#support-model').modal("hide");
                        eLoaderClose();
                        $('.close').click();
                    } else {
                        eLoaderError(data);
                    }
                },
                error: function (error) {
                    eLoaderError(error);
                }
            });
        }
    });

});
