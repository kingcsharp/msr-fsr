$(window).load(function () {
    // The slider being synced must be initialized first
    $('#carousel').flexslider({
        animation: "slide",
        controlNav: false,
        animationLoop: false,
        slideshow: false,
        itemWidth: 150,
        itemHeight: 60,
        itemMargin: 5,
        asNavFor: '#slider'
    });

    $('#slider').flexslider({
        animation: "fade",
        controlNav: false,
        animationLoop: false,
        slideshow: false,
        sync: "#carousel"
    });


});
//document ready
$(function () {

    if (parseInt($('#ncr-count').val()) > 0) {
        eModal.confirm('There are NCRs associated to this part. Would you like to view them?', 'NCR Check')
        .then(handleNCRButtonPush, null);
    }

    $('.selectpicker').selectpicker();

    $('#wip-item-select').on('changed.bs.select', function (e) {
        currVal = $(this).children('option:selected').data('content');

        window.location.href = '/wip/details/' + $(this).val();



        var term = /Complete/;
        var exists = term.test(currVal);
        if (!exists) {
            $('.wip-detail-item-controls').show();
            bootbox.confirm({
                message: "There is one or more NCR's related to this WO Item. would you like to view them now?",
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className: 'btn-success'
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn-danger'
                    }
                },
                callback: function (result) {
                    console.log('This was logged in the callback: ' + result);
                    if (result) {
                        $("#ncrModal").modal()
                    }
                }
            });
        }
        else {
            $('.wip-detail-item-controls').hide();
        }
    });

    //$('#fileupload').fileupload({
    // Uncomment the following to send cross-domain cookies:
    //xhrFields: {withCredentials: true},
    //url: 'server/php/'
    //});

    //init switch
    $('#showCompletedSwitch').bootstrapSwitch();

    $('#showCompletedSwitch').on('switchChange.bootstrapSwitch', function (e, state) {
        e.preventDefault();
        var containerUl = $('#wip-item-select').siblings('div.dropdown-menu').find('ul.dropdown-menu');

        if (state) {
            containerUl.find('li').each(function (index, value) {
                if ($(this).find('a span.label').html() === "FINISHED") {
                    $(this).show();
                }
            });
            console.log('on');
        } else {
            containerUl.find('li').each(function (index, value) {
                if ($(this).find('a span.label').html() === "FINISHED") {
                    $(this).hide();
                }
            });
            console.log('off');
        }
        //$('.selectpicker').selectpicker('refresh');
    });

    $('[data-toggle="tooltip"]').tooltip();

    $(".wip-button").on('doubletap', function () {
        console.log("closing modal and loading wip detail ");
        $('#wipListModal').modal("hide");
    });

    var hasTimer = false;
    // Init timer start
    $('.start-timer-btn').on('click', function () {
        hasTimer = true;
        $('.timer').timer({
            editable: true
        });
        $(this).addClass('hidden');
        $('.pause-timer-btn, .remove-timer-btn').removeClass('hidden');
    });

    // Init timer resume
    $('.resume-timer-btn').on('click', function () {
        $('.timer').timer('resume');
        $(this).addClass('hidden');
        $('.pause-timer-btn, .remove-timer-btn').removeClass('hidden');
    });

    // Init timer pause
    $('.pause-timer-btn').on('click', function () {
        $('.timer').timer('pause');
        $(this).addClass('hidden');
        $('.resume-timer-btn').removeClass('hidden');
    });

    // Remove timer
    $('.remove-timer-btn').on('click', function () {
        hasTimer = false;
        $('.timer').timer('remove');
        $(this).addClass('hidden');
        $('.start-timer-btn').removeClass('hidden');
        $('.pause-timer-btn, .resume-timer-btn').addClass('hidden');
    });

    // Additional focus event for this demo
    $('.timer').on('focus', function () {
        if (hasTimer) {
            $('.pause-timer-btn').addClass('hidden');
            $('.resume-timer-btn').removeClass('hidden');
        }
    });

    // Additional blur event for this demo
    $('.timer').on('blur', function () {
        if (hasTimer) {
            $('.pause-timer-btn').removeClass('hidden');
            $('.resume-timer-btn').addClass('hidden');
        }
    });

    $('#wipListModal').on('hidden.bs.modal', function (event) {
        $(this).data('bs.modal', null);
    });

    $('#wipListModal').on('show.bs.modal', function (event) {
        var modal = $(this);

        $.ajax({
            type: "GET",
            url: '/wip/WipListModel',
            dataType: 'html',
            success: function (data) {
                modal.find('.modal-body').html(data);
            },
            error: function () {

            }
        });
    });

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

    $('#display-report').on('click', function () {
        $('#print-other-content').html('<img src="/assets/img/loading.gif"  style="width:32px;height:32px;" />');
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
                $('#print-other-report').show();
                $('#display-report').hide();
            },
            error: function () {

            }
        });
    });

    $('.step-task').on('click', function () {

        loadStep(this);
    });

    var stepInProgress = $('#carousel ul.slides li.step').first(".step-inprogress");

    if (stepInProgress !== null) {
        loadStep(stepInProgress);
    } else {
        loadStep($('#carousel ul.slides li.step').first());
    }

    

    $('#relatedDocument').on('hidden.bs.modal', function (event) {
        $(this).data('bs.modal', null);
    });

    $('#relatedDocument').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var id = button.data('id');
        var type = button.data('type');
        var url = '';
        if (type == "theory") {
            url = '/wip/GetReferenceTheory?theoryId=' + id;
        } else {
            url = '/wip/GetReferenceDocument?documentId=' + id;
        }

        var modal = $(this);

        $(this).find('h4.modal-title').html(button.data('name'));

        $.ajax({
            type: "GET",
            url: url,
            dataType: 'html',
            success: function (data) {
                modal.find('.modal-body').html(data);
            },
            error: function () {
            }
        });
    });
});

function openNav() {
    document.getElementById("wip-side-nav").style.width = "250px";
}

/* Set the width of the side navigation to 0 */
function closeNav() {
    document.getElementById("wip-side-nav").style.width = "0";
}

$('#btn-take-over-task').on('click', function () {
    var id = $(this).data('id');

    $.ajax({
        type: "POST",
        url: "/wip/AssumeStepsClick?fillId=" + id,
        dataType: 'json',
        success: function (data) {
            location.reload();
        },
        error: function () {

        }
    });
});


$('#btn-cancel-steps').on('click', function () {
    var id = $(this).data('id');

    $.ajax({
        type: "POST",
        url: "/wip/CancelUnfinishedSteps?fillId=" + id,
        dataType: 'json',
        success: function (data) {
            location.reload();
        },
        error: function () {

        }
    });

});

function loadStep(step) {
    var stepId = $(step).data("stepid");
    var phStepId = $(step).data("phstepid");
    var fillId = $(step).data("fill-id");

    $('#step-' + stepId).html('<img src="/assets/img/loading.gif"  style="width:32px;height:32px;" />');

    $.ajax({
        type: "GET",
        url: "/wip/GetWipStepDetails?stepId=" + stepId + "&phStepId=" + phStepId + "&fillId=" + fillId,
        dataType: 'html',
        success: function (data) {
            $('#step-' + stepId).html(data);
        },
        error: function () {

        }
    });
}

function handleNCRButtonPush() {
    var id = $('#fill-id').val();

    $.ajax({
        type: "GET",
        url: '/wip/getncrmodel?id=' + id,
        dataType: 'html',
        success: function(data) {
            $('#ncrModal').modal("show");
            $('#ncrModal').find('.modal-body').html(data);
        },
        error: function() {

        }
    });
}