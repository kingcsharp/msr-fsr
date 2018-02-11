$('#addMoniterModal').on('show.bs.modal',
    function (event) {
        var button = $(event.relatedTarget);
        //var id = button.data('id');
        var moniterType = button.data('moniter-type');
        var inputType = button.data('input-type');
        var failAction = button.data('fail-action');
        var description = button.data('description');
        var objectId = button.data('object-id');
        var relatedObject = button.data('related-object');
        var stepId = button.data('step-id');
        var procedureName = button.data('procedure-name');

        var shouldBe = button.data('step-shouldbe');
        var highestThreshold = button.data('step-highestthreshold');
        var highThreshold = button.data('step-highthreshold');
        var target = button.data('step-target');
        var lowThreshold = button.data('step-lowthreshold');
        var lowestThreshold = button.data('step-lowestthreshold');
        var targetObject = button.data('step-Target-Object');

        var modal = $(this);
        modal.find('.modal-body').html('');
        $.ajax({
            type: "GET",
            url: '/Procedures/AddMonitor?moniterType=' + moniterType + '&inputType=' + inputType + '&failAction=' + failAction + '&description=' + description + '&objectId=' + objectId + '&relatedObject=' + relatedObject + '&stepId=' + stepId + '&procedureName=' + procedureName + '&shouldBe=' + shouldBe + '&highestThreshold=' + highestThreshold + '&highThreshold=' + highThreshold + '&target=' + target + '&lowThreshold=' + lowThreshold + '&lowestThreshold=' + lowestThreshold + '&targetObject=' + targetObject,
            dataType: 'html',
            success: function (data) {
                modal.find('.modal-body').html(data);
            },
            error: function (error) {
                eLoaderError(error);

            }
        });
    });

$(document).ready(function () {
    $('#submit-moniter-form').on('click',
        function () {
            $('#moniter-form').submit();
        });

    $("#sortable").sortable({
        handle: '.sortable-fa',
        delay: 100,
        cancel: "input,textarea,select",

        placeholder: "highlight",
        start: function (event, ui) {
            console.log("Start");
            //alert("Start");
        },
        change: function (event, ui) {
            console.log("Change");
        },
        update: function (event, ui) {
            console.log("Update");

            var productOrder = $(this).sortable('toArray');
            console.log(productOrder);

            var procObjectId = $(this).find('li').data("mainobjectid");
            eLoaderOpen();
            $.ajax({
                type: "POST",
                url: "/Procedures/ReorderSteps",
                dataType: 'json',
                data: {
                    array: productOrder, procObjectId: procObjectId

                },
                success: function (data) {
                    location.reload();
                },
                error: function (error) {
                    eLoaderError(error);
                }
            });
        }

    }).disableSelection();
    $('#monitor1-switch').bootstrapSwitch();
    $('#monitor1-switch').on('switchChange.bootstrapSwitch', function (e, state) {
        e.preventDefault();
        if (state) {
            $("#1a tr.on-off-row").show();
            console.log('on');
        } else {
            $("#1a tr.on-off-row").hide();
            console.log('off');
        }
    });

    $('#monitor2-switch').bootstrapSwitch();
    $('#monitor2-switch').on('switchChange.bootstrapSwitch', function (e, state) {
        e.preventDefault();
        if (state) {
            $("#2a tr.on-off-row").show();
            console.log('on');
        } else {
            $("#2a tr.on-off-row").hide();
            console.log('off');
        }
    });

    $(".nav-tabs").on("click", "a", function (e) {
        e.preventDefault();
        if (!$(this).hasClass('add-monitor')) {
            $(this).tab('show');
        }
    });
    $(".deleteMoniter").on("click", function () {
        var clicked = $(this);
        eModal.confirm(
            'Are you sure?')
            .then(confirmCallback, optionalCancelCallback);

        function confirmCallback() {

            var id = clicked.data('id');
            var stepId = clicked.data('procid');
            var ProdecureName = clicked.data('ProcedureName');
            if (stepId == null && ProdecureName == null) {
                location.reload();
            }
            else {
                eLoaderOpen();
                $.ajax({
                    type: "POST",
                    url: "/Procedures/DeleteStep",
                    dataType: 'json',
                    data: { id: id, procStepId: stepId, ProdecureName: ProdecureName },
                    success: function (data) {
                        location.reload();
                    },
                    error: function (error) {
                        eLoaderError(error);
                    }
                });
                eLoaderClose();
            }


        }

        function optionalCancelCallback() {
            console.log("Cancel");
        }

    });
    $("form").submit(function (e) {
        $('.submitselect option').prop('selected', true);
    });

    $('.deleteMoniter').click(function (e) {

    });
});