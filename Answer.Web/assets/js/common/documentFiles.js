$(document).ready(function () {
    $(".select").select2({
        placeholder: "Select",
        allowClear: true
    });
    $("form").submit(function (e) {
        $('.submitselect option').prop('selected', true);
        
    });
    $('#select-images').on('show.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var modal = $(this);

            $.ajax({
                type: "GET",
                url: '/Files/GetFiles?callBackId=' + callBackId,
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
            console.log("ok")
            $('#' + callBackId + ' option:selected').remove();
        }
        function optionalCancelCallback() {
            console.log("cancel")
        }

    })
});

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
                    // found it
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
    $('.closeClick').click();
}
