var selItems = {}
selItems.values = [];
selItems.total = 0;

function CheckBoxSelection(id, element, value) {
console.log('CheckBoxSelection');

    //for add
    if ($(element).prop("checked") === true) {
        AddItems(value);

        selItems.total = 0;
        $('#total-amount').val(selItems.total);

        CalculateTax($('#total-amount').val(), $('#total-tax-amount').val());
    }
    else {
        RemoveItems(value);

        selItems.total = 0;
        $('#total-amount').val(selItems.total);

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
    let data = JSON.parse(id);
    selItems.values.push(data);
    $('#addId').val(JSON.stringify(selItems.values));
}

function RemoveItems(id) {
    let data = JSON.parse(id);
    selItems.values = selItems.values.filter(function (item) {
        return !(
            item.REFERENCEPO == data.REFERENCEPO &&
            item.name == data.name &&
            item.OpenDate == data.OpenDate
        );
    });
    $('#addId').val(selItems.values);
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
