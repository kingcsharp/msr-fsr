jQuery(document).ready(function ($) {
    $('.viewArchivesModal').click(function () {
        SetFields();
    });

    $('#viewArchivesModal').on('show.bs.modal', function (e) {
        SetFields();
    });

    function SetFields() {
        $('#table-container').block({
            centerX: true,
            centerY: true,
            message: '<h3><img src="/assets/img/nice_loader.gif" />  Loading. Please wait...</h3>',
            css: { 'width': '50%', 'margin-top': '120px', 'margin-left':'220px' }
        });

        $.ajax({
            url: "/api/Archive/CombinedFinancialData",
            dataType: 'json',
            success: function (data) {
                $.each(data, function (index, value) {
                    var date = new Date(value.createDate);
                    var newContent = '<tr><td><a href="/api/Archive/Download?downloadURL=' + value.downloadURL + '">' + value.fileName + '</a></td><td>' + (value.fileSize / 1024.0).toFixed(2) + ' MB </td><td>' + date.toLocaleDateString() + '</td></tr>';
                    $(".archiveTable tbody").append(newContent);
                });
                $('#table-container').unblock();
            }
        });
    }

});