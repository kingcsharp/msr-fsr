(function () {
    $(document).ready(function () {
        $(document).ajaxStart(function () {
            $.blockUI({ message: '<h1><img src="/assets/img/nice_loader.gif" />  Loading. Please wait...</h1>' });
        });
        $(document).ajaxComplete(function () {
            $.unblockUI();
        });
        $(document).ajaxError(function (event, jqxhr, settings, thrownError) {
            //could handle errors here.
            $.unblockUI();
        });
    });
})();


