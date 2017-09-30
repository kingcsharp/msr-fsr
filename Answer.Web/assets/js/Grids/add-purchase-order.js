$(document).ready(function () {
    $('.datepick').datetimepicker();

    $('.dateSelecter').click(function (parameters) {
        $(this).closest('td').find('.datepick').focus();
    });


    $('#select-products').on('show.bs.modal',
        function (event) {

            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var modal = $(this);
            var supplierDepartmentValue = $('#SupplierDepartment option:selected').val();
            var clientValue = $('#Client option:selected').val();

            $.ajax({
                type: "GET",
                url: '/PurchaseOrder/GetProducts?callBackId=' + callBackId + '&client=' + clientValue + '&supplierdepartment=' + supplierDepartmentValue,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });
        });

    $('.deletefiles').click(function () {

        var callBackId = $(this).data('call-back-id');

        eModal.confirm('Do you really want to Remove ' + callBackId + ' ?', 'Confirmation delete')
            .then(confirmCallback, optionalCancelCallback);

        function confirmCallback() {
            $('#' + callBackId + ' option:selected').remove();
        }

        function optionalCancelCallback() {
        }

    });

});


function SaveCloseFun() {
    $('#SaveClose').val('true');
    $('#SaveWorkflow').val('');
    $('#Save').val('');
    return true;
}
function SaveFun() {
    $('#Save').val('true');
    $('#SaveClose').val('');
    $('#SaveWorkflow').val('');
    return true;
}
function SaveSubmitApprovalFun() {
    $('#SaveWorkflow').val('true');
    $('#Save').val('');
    $('#SaveClose').val('');
    return true;
}


function SelectIds() {

    $(".selected-file").each(function (index) {

        if ($(this).is(":checked")) {
            var ids = $(this).val().split('|');

            var callBackId = $(this).closest(".modal-body").find('#target-control-id').val();
            var options = $('#' + callBackId + '')[0].options;

            var optionsArray = $.map(options, function (elem) {
                return (elem.value);
            });

            if (optionsArray.length > 0) {

                if ($.inArray(ids[0], optionsArray) != -1) {
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

}

