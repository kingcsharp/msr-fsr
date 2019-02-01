if ($.jgrid && $.jgrid.defaults) {
    $.extend($.jgrid.defaults, {
        ajaxGridOptions: {
            beforeSend: function (xhr) {
                $.blockUI({ message: '<h1><img src="/assets/img/nice_loader.gif" />  Loading. Please wait...</h1>' });
            },
            complete: function (xhr) {
                $.unblockUI();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $.unblockUI();
            }
        }
    });
}
