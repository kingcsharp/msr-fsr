jQuery(document).ready(function ($) {
    $('.viewArchivesModal').click(function () {
        SetFields();
    });

    $('#viewArchivesModal').on('show.bs.modal', function (e) {
        SetFields();
    });

    function SetFields() {
        $('.modal-body').block({ message: '<h1><img src="/assets/img/nice_loader.gif" />  Loading. Please wait...</h1>' });
        $('.blockElement').css("width", "50%", "margin-left", "100px");
        $.ajax({
            url: "/api/Archive/CombinedFinancialData",
            dataType: 'json',
            success: function (data) {
                $.each(data, function (index, value) {
                    var date = new Date(value.createDate);
                    var newContent = '<tr><td><a href="/api/Archive/Download?downloadURL=' + value.downloadURL + '">' + value.fileName + '</a></td><td>' + (value.fileSize / 1024.0).toFixed(2) + ' MB </td><td>' + date.toLocaleDateString() + '</td></tr>';
                    $(".archiveTable tbody").append(newContent);
                });
                $('.modal-body').unblock();
            }
        });
    }
});