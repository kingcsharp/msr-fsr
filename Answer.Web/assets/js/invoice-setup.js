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
                        eLoaderClose();
                        modal.find('.modal-body').html(data);
                        modal.find('.modal-body').unblock();
                    },
                    error: function (error) {
                        eLoaderError(error);
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
                console.log('XXXX ' + id);
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
                        eLoaderError(error);
                        modal.find('.modal-body').unblock();
                    }
                });

            });
    };

    var custChangeForAdd = function () {
        var reloadTable = function () {
            eLoaderOpen();
            var po = $('#Client').val();
            var fac = $('#InvoiceClass').val();

            if (po.length == 0) {
                eLoaderClose();
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
                    eLoaderClose();
                    $('#invoice-po').html(data);
                }
            });
        }
        $('#Client').on('change', reloadTable);
        $('#InvoiceClass').on('change', reloadTable);
    };

    var saveInvoice = function () {

        $('#invoice-submit').click(function (e) {
            $('#FormId').submit();

        });
    };

    var updateInvoice = function () {

        $('#invoice-update').click(function (e) {
            $('#invoice-edit').submit();
        });
    };
    var custPoChangeForEdit = function () {

        $('#cust-po-edit').on('change',
            function () {
                eLoaderOpen();
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
                        eLoaderClose();
                        $('#invoice-po-edit').html(data);
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

