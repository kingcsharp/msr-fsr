function SelectTheory() {

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
}

