$(function() {
    
    $('#wioDetailPrintTraveler').on('hidden.bs.modal', function (event) {
        $(this).data('bs.modal', null);
    });

    $('#wioDetailPrintTraveler').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var id = button.data('id');
        var modal = $(this);

        $.ajax({
            type: "GET",
            url: '/wip/printTraveler?id=' + id,
            dataType: 'html',
            success: function (data) {
                modal.find('.modal-body').html(data);
            },
            error: function () {

            }
        });
    });

	$('#wioDetailPrintOther').on('hidden.bs.modal', function (event) {
	    $(this).data('bs.modal', null);
	});

	$('#wioDetailPrintOther').on('show.bs.modal', function (event) {
	    var button = $(event.relatedTarget);
	    var id = button.data('id');
	    var modal = $(this);

	    $.ajax({
	        type: "GET",
	        url: '/wip/printOther?id=' + id,
	        dataType: 'html',
	        success: function (data) {
	            modal.find('.modal-body').html(data);
	        },
	        error: function () {

	        }
	    });
	});

    $('#display-report').on('click', function() {
        var id = $('.print-other-fill-id').val();
        var reportType = $('#PrintOtherId').val();
        
        var url = '/PrintOther/';
        if (reportType == "WORK_REPORT") {
            var purchaseItemId = $('#PURCH_ITEM_ID').val();
            var tsrType = $('#TSR_TYPE').val();
            var showSteps = $('#SHOW_STEPS').val();
            var showShipping = $('#SHOW_SHIPPING').val();
            url += 'WorkReportTsr?id=' + id + '&purchaseItemId=' + purchaseItemId + '&tsrType=' + tsrType + '&showSteps=' + showSteps + '&showShipping=' + showShipping;
        } else {
            url += 'PrintReport?id=' + id + '&reportType=' + reportType;
        }
        
        $.ajax({
            type: "GET",
            url: url,
            dataType: 'html',
            success: function (data) {
                $('#print-other-content').html(data);
            },
            error: function () {

            }
        });
    });

   
});

