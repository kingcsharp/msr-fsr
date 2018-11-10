var inVoiceTax = {}
inVoiceTax.values = [];
inVoiceTax.total = 0;

function CheckBoxSelectionOnEdit(id, element, value) {

    if (isNaN(value)) {
        value = 0;
    }
    if (id !== '') {
        //for edit
        var editTotalAmount = $('#totalAmount').val();

        var items = $('#itemId').val();

        if (items.indexOf(',') !== -1) {
            inVoiceTax.values = items.split(',');
		} else {
			if (items !== '' && $.inArray(items, inVoiceTax.values) === -1) {
				inVoiceTax.values.push(items);
			}
        }
        if ($(element).prop("checked") === true) {

            AddItems($(element).closest('tr').find('.getId').find('#poId').val());

            inVoiceTax.total = (parseFloat(editTotalAmount) + parseFloat(value)).toFixed(2);
            $('#totalAmount').val(inVoiceTax.total);

            CalculateTax($('#totalAmount').val(), $('#totalTaxAmount').val());
        }
        else {
            RemoveItems($(element).closest('tr').find('.getId').find('#poId').val());

            inVoiceTax.total = (parseFloat(editTotalAmount) - parseFloat(value)).toFixed(2);
            $('#totalAmount').val(inVoiceTax.total);

            CalculateTax($('#totalAmount').val(), $('#totalTaxAmount').val());
        }
    }
}


$('#totalTaxAmount').on('change', function () {

    if (isNaN($(this).val())) {
        $(this).val(0);
    } 

    $(this).val(parseFloat($(this).val()).toFixed(2));

    CalculateTax($('#totalAmount').val(), $(this).val());
});

function AddItems(id) {
    inVoiceTax.values.push(id);
	$('#itemId').val(inVoiceTax.values);
}

function RemoveItems(id) {
	inVoiceTax.values = inVoiceTax.values.filter(function (item) {
        return item !== id;
    });
	$('#itemId').val(inVoiceTax.values);
}

function CalculateTax(subTotal, tax) {
    if (tax !== '') {
        var totalTaxPerAmount = (subTotal * tax) / 100;
        var totalAmount = parseFloat(subTotal) + parseFloat(totalTaxPerAmount);
		$('#total').val(totalAmount.toFixed(2));
    } else {
		$('#total').val(subTotal);
    }
}