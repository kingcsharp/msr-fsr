

$(document).ready(function () {

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

    $("form").submit(function (e) {
        $('.submitselect option').prop('selected', true);
    });

    UpdateMonitor();
    AddMonitor();
});

function UpdateMonitor() {
    $('#submit-moniter-form').on('click',

        function () {

            var stepId = $('#AddMonitorForProcedureViewModel_Step_Id').val();
            var moniter = $('#moniter-form').serialize();

            eLoaderOpen();

            $.ajax({
                type: "POST",
                url: "/Procedures/SaveMonitor",
                dataType: 'HTML',
                data: moniter,
                success: function (data) {
                    $('#' + stepId + ' tbody').html('');
                    $('#' + stepId + ' tbody').append(data);
                    eLoaderClose();

                    $('#addMoniterModal').modal('toggle');
                },
                error: function (error) {
                    eLoaderError(error);
                }
            });
        });
}

function AddMonitor() {
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
}

function DeleteMonitor(target) {

    var clicked = $(target);

    eModal.confirm('Are you sure?').then(confirmCallback);

    function confirmCallback() {

        var id = clicked.data('id');
        var stepId = clicked.data('step-id');

        eLoaderOpen();

        $.ajax({
            type: "POST",
            url: "/Procedures/DeleteMonitor",
            dataType: 'html',
            data: { id: id, stepId: stepId },
            success: function(data) {
                $('#' + stepId + ' tbody').html('');
                $('#' + stepId + ' tbody').append(data);
                eLoaderClose();
            },
            error: function(error){
            eModal.alert(error.statusText);
            }
        });
    }

}