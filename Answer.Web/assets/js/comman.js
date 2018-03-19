function eLoaderOpen() {
    $("#emodel").modal({

        backdrop: 'static',
        keyboard: false
    });
}

function eLoaderClose() {
    $("#emodel").modal('hide');
}

function eLoaderError(value) {
    var model = $("#emodel");
    model.on('shown.bs.modal', function () {
        model.find('.modal-body').html('').append(value);
    });
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


function UnLockWorkflow(returnUrl) {

    $('.unlock').on('click',
        function (e) {
            e.preventDefault();

            var callBackId = $(this).data('call-back-id');

            eModal.confirm('If you proceed you will lose any edits you made.  Are you sure?')
                .then(confirmCallback, optionalCancelCallback);

            function confirmCallback() {
                window.location.href =
                    "/workflow/UnlockAndDelete?objId=" + callBackId + '&returnUrl=' + returnUrl;
            }

            function optionalCancelCallback() {

            }
        });
}

function SelectOldIds() {

    $(".selected-file").each(function (index) {

        if ($(this).is(":checked")) {
            var ids = $(this).val().split('|');

            var callBackId = $(this).closest(".modal-body").find('#target-control-id').val();
            var options = $('#' + callBackId + '')[0].options;
            console.log(callBackId);
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
