$(document).ready(function () {

    $(".select").select2({
        placeholder: "Select",
        allowClear: true
    });

    $("form").submit(function (e) {
        $('.submitselect option').prop('selected', true);
        
    });

    $('#select-pic').on('show.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var modal = $(this);

            $.ajax({
                type: "GET",
                url: '/Files/GetFile?callBackId=' + callBackId,
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

