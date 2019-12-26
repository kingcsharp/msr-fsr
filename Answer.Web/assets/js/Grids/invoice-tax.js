var selItems = {}
selItems.values = [];
selItems.total = 0;

function CheckBoxSelection(id, element, value) {
console.log('CheckBoxSelection');

    //for add
    if ($(element).prop("checked") === true) {
        AddItems(value);
    }
    else {
        RemoveItems(value);
    }
}

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

