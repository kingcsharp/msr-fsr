$(document).ready(function () {

    $('#select-doc-view').on('show.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var imageType = button.data('file-type');
            var modal = $(this);

            $.ajax({
                type: "GET",
                url: '/doc/GetFileView?callBackId=' + callBackId + '&type=' + imageType,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });

        });


    $('#select-referenceprocedures').on('show.bs.modal',
        function (event) {

            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var modal = $(this);

            $.ajax({
                type: "GET",
                url: '/Procedures/GetProcedures?callBackId=' + callBackId,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });

        });

    $('#select-referenceproceduresview').on('show.bs.modal',
                    function (event) {
                        var button = $(event.relatedTarget);
                        var callBackId = button.data('call-back-id');
                        var modal = $(this);

                        $.ajax({
                            type: "GET",
                            url: '/Procedures/GetProceduresView?callBackId=' + callBackId,
                            dataType: 'html',
                            success: function (data) {
                                modal.find('.modal-body').html(data);
                            },
                            error: function () {

                            }
                        });

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
    $('#select-objects').on('show.bs.modal',
        function (event) {

            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var modal = $(this);

            $.ajax({
                type: "GET",
                url: '/Objects/GetObjects?callBackId=' + callBackId,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });
        });

  
    $('#select-theory').on('show.bs.modal',
        function (event) {

            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var modal = $(this);

            $.ajax({
                type: "GET",
                url: '/TheoryParagraph/GetTheoryParagraphs?callBackId=' + callBackId,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });
        });
    $("form").submit(function (e) {
        $('.submitselect option').prop('selected', true);
    });
})
function SelectIds() {

    $(".selected-file").each(function (index) {

        if ($(this).is(":checked")) {
            var ids = $(this).val().split('|');

            var callBackId = $(this).closest(".modal-body").find('#target-control-id').val();
            var options = $('#' + callBackId + '')[0].options;
            console.log(callBackId);
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
