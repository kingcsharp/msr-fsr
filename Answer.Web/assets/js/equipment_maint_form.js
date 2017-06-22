$( document ).ready(function() {
    $('#primaryLocation').on('change', function() {
         if ( this.value !== '') {
            $('#sublocation1').prop( "disabled", false );
         }
    })
    $('#sublocation1').on('change', function() {
         if ( this.value !== '') {
            $('#sublocation2').prop( "disabled", false );
         }
    })
    
    $('#dateTime').val(new Date().toJSON().slice(0,19));
});
