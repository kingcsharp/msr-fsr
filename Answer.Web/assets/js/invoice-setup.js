var Invoice = function () {

    var loadInvoiceModal = function () {
        $('#invModal').on('show.bs.modal',
            function (event) {
                var modal = $(this);
                $.ajax({
                    type: "GET",
                    url: '/Invoices/LoadInvoice',
                    dataType: 'html',
                    cache: false,
                    success: function (data) {
                        eLoaderClose();
                        modal.find('.modal-body').html(data);
                    },
                    error: function (error) {
                        eLoaderError(error);
                    }
                });
            });
    };

    var editInvoiceModal = function () {
        $('#editInvModal').on('show.bs.modal',
            function (event) {

                var button = $(event.relatedTarget);
                var id = button.data('call-id');
                var modal = $(this);
                $.ajax({
                    type: "GET",
                    url: '/Invoices/LoadInvoiceById?id=' + id + '&hasValue=' + true,
                    dataType: 'html',
                    cache: false,
                    success: function (data) {
                        modal.find('.modal-body').html(data);
                    },
                    error: function (error) {
                        eLoaderError(error);
                    }
                });

            });
    };

    var custPoChangeForAdd = function () {

        $('#CustPo').on('change',
            function () {
                eLoaderOpen();
                var po = $(this).val();
                $('#addId').val("");
                $('#total-tax-amount').val("");
                $('#total-add').val("");
                $('#total-amount').val("0.00");

                $.ajax({
                    type: "POST",
                    url: "/Invoices/InvoiceModelGetPoItems?id=" + po + '&hasValue=' + false,
                    dataType: 'html',
                    success: function (data) {
                        eLoaderClose();
                        $('#invoice-po').html(data);
                    }
                });
            });
    };

    var editOnPageLoadCustPo = function (po, itemsId, hasEdit) {
        eLoaderOpen();
        $('#invoicePo form').clearQueue();
        $.ajax({
            type: "POST",
            url: "/Invoices/InvoiceModelEditGetPoItems?id=" + po + '&hasValue=' + hasEdit,
            dataType: 'html',
            success: function (data) {
                eLoaderClose();
                var values = itemsId;
                var items = values.split(",");
                $("input:checkbox[class=checkAmount]").each(function () {
                    if (jQuery.inArray($(this).val(), items) !== -1) {
                        $(this).prop('checked', true);
                    }
                });
            }
        });
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

    return {
        LoadInvoiceModal: loadInvoiceModal,
        EditInvoiceModal: editInvoiceModal,
        CustPoChange: custPoChangeForAdd,
        EditOnPageLoadCustPo: editOnPageLoadCustPo,
        SaveInvoice: saveInvoice,
        CustPoChangeForEdit: custPoChangeForEdit,
        UpdateInvoice: updateInvoice
    }
}

