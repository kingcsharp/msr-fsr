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