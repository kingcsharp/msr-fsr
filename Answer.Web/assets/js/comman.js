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