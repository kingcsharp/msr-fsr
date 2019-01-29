if ($.jgrid && $.jgrid.defaults) {
    $.extend($.jgrid.defaults, {
        ajaxGridOptions: {
            beforeSend: function (xhr) {
                console.log('beforeSend');
                $.blockUI({ message: '<h1><img src="/assets/img/nice_loader.gif" />  Loading. Please wait...</h1>' });
            },
            complete: function (xhr) {
                console.log('complete');
                $.unblockUI();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('error');
                $.unblockUI();
            }
        }
    });
}
