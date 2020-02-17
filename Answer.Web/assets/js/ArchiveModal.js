jQuery(document).ready(function ($) {
    $('.viewArchivesModal').click(function () {
        SetFields();
    });

    $('#viewArchivesModal').on('show.bs.modal', function (e) {
        SetFields();
    });

    function SetFields() {
        $.ajax({
            url: "/api/Archive/CombinedFinancialData",
            dataType: 'json',
            success: function (data) {
                $.each(data, function (index, value) {
                    var newContent = '<tr><td><a href="' + value.downloadURL + '">' + value.fileName + '</a></td><td>' + (value.fileSize / 1024.0) + ' MB </td><td>' + value.createDate + '</td></tr>';
                    $("#archiveTable tbody").append(newContent);
                });
            }
        });
    }
});