$(document).ready(function () {
    $('#success_message').hide();
    
    $('#sendit').click(function( event ) {
        event.preventDefault();

        var dataForm = $("form").serialize();

        $.ajax({
            type: "POST",
            data: dataForm ,
            url: '/help/SupportRequest',
            dataType: 'json',
            success: function (data) {

                if (data==="OK") {
                    $('.modal-body').find('input, textarea, button, select').val('').attr('disabled','disabled');
                    $('#success_message').show();
                }

            },
            error: function () {

            }
        });


          
    });
});