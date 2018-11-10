

$(document).ready(function () {

    $("#sortable").sortable({
        handle: '.grippy',
        delay: 100,
        cancel: "input,textarea,select",

        placeholder: "highlight",
        start: function (event, ui) {
        },
        change: function (event, ui) {
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

        function (e) {

            var stepId = $('#AddMonitorForProcedureViewModel_Step_Id').val();
            var moniter = $('#moniter-form').serialize();

            e.preventDefault();
            var form = $('#moniter-form').closest("form");
            var isvalid = form.valid();
            if (isvalid) {
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

                        $('.monitor-close').click();
                    },
                    error: function (error) {
                        eLoaderError(error);
                    }
                });
            }
        });

    $('#edit-submit-moniter-form').on('click',

        function (e) {

            var stepId = $('#AddMonitorForProcedureViewModel_Step_Id').val();
            var moniter = $('#moniter-form').serialize();

            e.preventDefault();
            var form = $('#moniter-form').closest("form");
            var isvalid = form.valid();

            if (isvalid) {
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

                        $('.monitor-close').click();
                    },
                    error: function (error) {
                        eLoaderError(error);
                    }
                });
            }
        });
}

function AddMonitor() {
    $('#addMoniterModal').on('show.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var relatedObject = button.data('related-object');
            var stepId = button.data('step-id');
            var modal = $(this);
            $.ajax({
                type: "GET",
                url: '/Procedures/AddMonitor?relatedObject=' + relatedObject + '&stepId=' + stepId,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html('').append(data);
                },
                error: function (error) {
                    eLoaderError(error);

                }
            });
        });
}
function EditMonitor() {

    $('#editMoniterModal').on('show.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var objectId = button.data('object-id');
            var modal = $(this);
            $.ajax({
                type: "GET",
                url: '/Procedures/EditMonitor?objectId=' + objectId,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html('').append(data);
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
            success: function (data) {
                $('#' + stepId + ' tbody').html('');
                $('#' + stepId + ' tbody').append(data);
                eLoaderClose();
            },
            error: function (error) {
                eModal.alert(error.statusText);
            }
        });
    }

}