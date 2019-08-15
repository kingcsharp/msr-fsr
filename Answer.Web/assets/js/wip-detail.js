(function () {
    $(document).ready(function () {
        var currStepId = -1;
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

        $('.selectpicker').selectpicker();

        $('.dropdown-menu li').on('click', function (e) {
            var fillId = $(this).find('strong').data('fill-id');
            var content = $(this).html();

            if (!isNaN(fillId)) {

                window.location.href = '/wip/details/' + fillId;

                var term = /Complete/;
                var exists = term.test(content);

                if (!exists) {
                    $('.wip-detail-item-controls').show();
                } else {
                    $('.wip-detail-item-controls').hide();
                }
            }

        });

        //init switch
        $('#showCompletedSwitch').bootstrapSwitch();

        $('#showCompletedSwitch').on('switchChange.bootstrapSwitch', function (e, state) {
            e.preventDefault();
            LoadMyItems(state);
        });

        $('[data-toggle="tooltip"]').tooltip();

        $(".wip-button").on('doubletap', function () {
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

        });

        // Init timer resume
        $('.resume-timer-btn').on('click', function () {
            $('.timer').timer('resume');
            $(this).addClass('hidden');

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

        });

        // Additional focus event for this demo
        $('.timer').on('focus', function () {
            if (hasTimer) {

                $('.resume-timer-btn').removeClass('hidden');
            }
        });

        // Additional blur event for this demo
        $('.timer').on('blur', function () {
            if (hasTimer) {

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
            if (reportType === "WORK_REPORT") {
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

            var hasCompleted = $(this).hasClass('step-complete');

            if (!hasCompleted) {
                $('#carousel ul.slides li').each(function (i, obj) {
                    var hasRequested = $(this).hasClass('requested');
                    if (hasRequested) {
                        $(this).removeClass('requested');
                        $(this).addClass('waiting');
                    }
                });

                $(this).removeClass('waiting');
                $(this).addClass('requested');
            }

            if ($(this).data("stepid") === currStepId) {
                return;
            } else {
                currStepId = $(this).data("stepid");
                loadStep(this);
            }
        });

        $('#relatedDocument').on('hidden.bs.modal', function (event) {
            $(this).data('bs.modal', null);
        });

        $('#relatedDocument').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget);
            var id = button.data('id');
            var type = button.data('type');
            var url = '';
            if (type === "theory") {
                url = '/wip/GetReferenceTheory?theoryId=' + id;
            } else if (type === "document") {
                url = '/documents/GetDocumentById?Id=' + id;
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

        $('#ncr-notification-button').on('click', function () {
            var id = $('#fill-id').val();

            $.ajax({
                type: "GET",
                url: '/wip/getncrmodel?id=' + id,
                dataType: 'html',
                success: function (data) {
                    $('#ncrModal').modal("show");
                    $('#ncrModal').find('.modal-body').html(data);
                },
                error: function () { }
            });
        });


    });

    function init() {
        if (parseInt($('#ncr-count').val()) > 0) {
            $('#ncr-notification-button').removeClass('hidden');
        }

        LoadMyItems(true);

        var stepInProgress = $('#carousel ul.slides').find(".step-inprogress, .waiting").first();

        if (stepInProgress.length > 0) {
            $(stepInProgress).removeClass('waiting');
            $(stepInProgress).addClass('requested');
            $(stepInProgress).trigger("click");
        } else {
            var step = $('#carousel ul.slides li.step').first();
            if ($(step).data("stepid") === currStepId) {
                return;
            } else {
                currStepId = $(step).data("stepid");
                loadStep($('#carousel ul.slides li.step').first());
            }
        }
    }

    function loadStep(step) {
        var stepId = $(step).data("stepid");
        var phStepId = $(step).data("phstepid");
        var fillId = $(step).data("fill-id");
        //$('#step-' + stepId).html('<img src="/assets/img/loading.gif"  style="width:32px;height:32px;" />');
        $('#step-' + stepId).empty().html('');
        $('.step-container').block({ message: '<h1><img src="/assets/img/nice_loader.gif" />  Loading. Please wait...</h1>' });
        $.ajax({
            type: "GET",
            url: "/wip/GetWipStepDetails?stepId=" + stepId + "&phStepId=" + phStepId + "&fillId=" + fillId,
            dataType: 'html',
            cache: false,
            success: function (data) {
                $('.step-container').unblock();
                $('#step-' + stepId).html(data);
            },
            error: function (error) {
                $('.step-container').unblock();
            }
        });
    }

    function LoadMyItems(state) {

        var containerUl = $('#wip-item-select').siblings('div.dropdown-menu').find('ul.dropdown-menu');

        if (state) {

            containerUl.find('li').each(function (index, value) {
                if ($(this).find('a span.label').html() === "FINISHED") {
                    $(this).hide();
                }
            });

        } else {
            containerUl.find('li').each(function (index, value) {
                if ($(this).find('a span.label').html() === "FINISHED" || $(this).find('a span.label').html() === "CLOSED") {
                    $(this).show();
                }
            });
        }
    }
})();

function openNav() {
    document.getElementById("wip-side-nav").style.width = "250px";
}

/* Set the width of the side navigation to 0 */
function closeNav() {
    document.getElementById("wip-side-nav").style.width = "0";
}