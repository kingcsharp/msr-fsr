var inVoiceTax = {}
inVoiceTax.values = [];
inVoiceTax.total = 0;

function CheckBoxSelection(id, element, value) {

    if (isNaN(value)) {
        value = 0;
    }
    //for add
    if ($(element).prop("checked") === true) {
        AddItems($(element).closest('tr').find('.get-id').find('#po-id').val());

        inVoiceTax.total = (parseFloat(inVoiceTax.total) + parseFloat(value)).toFixed(2);
        $('#total-amount').val(inVoiceTax.total);

        CalculateTax($('#total-amount').val(), $('#total-tax-amount').val());
    }
    else {
        RemoveItems($(element).closest('tr').find('.get-id').find('#po-id').val());

        inVoiceTax.total = (parseFloat(inVoiceTax.total) - parseFloat(value)).toFixed(2);
        $('#total-amount').val(inVoiceTax.total);

        CalculateTax($('#total-amount').val(), $('#total-tax-amount').val());
    }
}

$('#total-tax-amount').on('change', function () {

    if (isNaN($(this).val())) {
        $(this).val(0);
    }

    $(this).val(parseFloat($(this).val()).toFixed(2));

    CalculateTax($('#total-amount').val(), $(this).val());
});

function AddItems(id) {
    inVoiceTax.values.push(id);
    $('#addId').val(inVoiceTax.values);
}

function RemoveItems(id) {
    inVoiceTax.values = inVoiceTax.values.filter(function (item) {
        return item !== id;
    });
    $('#addId').val(inVoiceTax.values);
}

function CalculateTax(subTotal, tax) {
    if (tax !== '') {
        var totalTaxPerAmount = (subTotal * tax) / 100;
        var totalAmount = parseFloat(subTotal) + parseFloat(totalTaxPerAmount);
        $('#total-add').val(totalAmount.toFixed(2));
    } else {
        $('#total-add').val(subTotal);
    }
}