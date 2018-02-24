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
  