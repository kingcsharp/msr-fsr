var Invoice = function () {

    var loadInvoiceModal = function () {
        $('#invModal').on('show.bs.modal', function (event) {
            clearModal($(this));
        });

        $('#invModal').on('shown.bs.modal',
            function (event) {
                var modal = $(this);
                modal.find('.modal-body').block({ message: '<h1><img src="/assets/img/nice_loader.gif" />  Loading. Please wait...</h1>' });
                $.ajax({
                    type: "GET",
                    url: '/Invoices/LoadInvoice',
                    dataType: 'html',
                    cache: false,
                    success: function (data) {
                        modal.find('.modal-body').html(data);
                        modal.find('.modal-body').unblock();
                    },
                    error: function (error) {
                        modal.find('.modal-body').unblock();
                    }
                });
            });
    };

    var editInvoiceModal = function () {
        $('#editInvModal').on('show.bs.modal', function (event) {
            clearModal($(this));
        });

        $('#editInvModal').on('shown.bs.modal',
            function (event) {
                var button = $(event.relatedTarget);
                var id = button.data('call-id');
                var num = button.data('call-number');
                var modal = $(this);
                modal.find('.modal-body').block({ message: '<h1><img src="/assets/img/nice_loader.gif" />  Loading. Please wait...</h1>' });
                $.ajax({
                    type: "GET",
                    url: '/Invoices/LoadInvoiceById?id=' + id + '&num=' + num,
                    dataType: 'html',
                    cache: false,
                    success: function (data) {
                        modal.find('.modal-body').html(data);
                        modal.find('.modal-body').unblock();
                    },
                    error: function (error) {
                        modal.find('.modal-body').unblock();
                    }
                });

            });
    };

    var custChangeForAdd = function () {
        var reloadTable = function () {
            $('.list-content').empty().html('');
            $('.list-content').block({ message: '<h1><img src="/assets/img/nice_loader.gif" />  Loading. Please wait...</h1>' });
            var po = $('#Client').val();
            var fac = $('#InvoiceClass').val();

            if (po.length == 0) {
                $('.list-content').unblock();
                return;
            }

            $('#addId').val("");

            $.ajax({
                type: "POST",
                url: "/Invoices/InvoiceModelGetPoList" +
                    "?id=" + po +
                    "&fac=" + fac,
                dataType: 'html',
                success: function (data) {
                    $('.list-content').unblock();
                    $('.list-content').html(data);
                }
            });
        }
        $('#Client').on('change', reloadTable);
        $('#InvoiceClass').on('change', reloadTable);
    };

    var saveInvoice = function () {

        $('#invoice-submit').click(function (e) {
            $(this).attr('disabled', 'disabled');
            $('#status-message').html('<i class="fa fa-refresh fa-spin"></i><span class="danger"> Updating invoice as requested. Please wait...</span>');
            $('#FormId').submit();
        });
    };

    var updateInvoice = function () {

        $('#invoice-update').click(function (e) {
            $(this).attr('disabled', 'disabled');
            $('#status-message').html('<i class="fa fa-refresh fa-spin"></i><span class="danger"> Updating invoice as requested. Please wait...</span>');
            $('#invoice-edit').submit();
        });
    };
    var custPoChangeForEdit = function () {

        $('#cust-po-edit').on('change',
            function () {
                $('.list-content').empty().html('');
                $('.list-content').block({ message: '<h1><img src="/assets/img/nice_loader.gif" />  Loading. Please wait...</h1>' });
                var po = $(this).val();
                $('#itemId').val("");
                $('#totalTaxAmount').val("");
                $('#total').val("");
                $('#totalAmount').val("0.00");

                $.ajax({
                    type: "POST",
                    url: "/Invoices/InvoiceModelEditGetPoItems?id=" + po + '&hasValue=' + false,
                    dataType: 'html',
                    success: function (data) {
                        $('.list-content').unblock();
                        $('.list-content').html(data);
                    }
                });
            });
    };

    function clearModal(modal) {
        modal.find('.modal-body').empty();
    }

    return {
        LoadInvoiceModal: loadInvoiceModal,
        EditInvoiceModal: editInvoiceModal,
        CustChange: custChangeForAdd,
        SaveInvoice: saveInvoice,
        CustPoChangeForEdit: custPoChangeForEdit,
        UpdateInvoice: updateInvoice
    }
}

