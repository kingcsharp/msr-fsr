$(document).ready(function () {
    $('#success_message').hide();
    
    $('#sendit').click(function( event ) {
      event.preventDefault();
          $('.modal-body').find('input, textarea, button, select').attr('disabled','disabled');
          $('#success_message').show();
    });
});