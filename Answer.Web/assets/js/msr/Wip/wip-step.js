var StepControls = function () {

    var stepStart = function (stepId, fillId, parentId) {

        $.ajax({
            type: "POST",
            url: "/wip/StepStartClick?stepId=" + stepId + "&fillId=" + fillId + "&parentId=" + parentId,
            dataType: 'json',
            success: function (data) {
                if (data.ErrorMessage !== '') {
                    eLoaderError(data.ErrorMessage);
                } else {
                    eLoaderClose();
                    var currentStep = $(".slides li[data-stepid='" + stepId + "']");
                    $(currentStep).removeClass('waiting');
                    $(currentStep).addClass('requested');
                    $(currentStep).trigger("click");
                }
            },
            error: function (error) {
                eLoaderError(error);
            }
        });
    };

    function stepDone(stepId, fillId, parentPartId, isSerilizeStep) {

        eLoaderOpen();

        $.ajax({
            type: "POST",
            url: '/wip/StepDoneClick?stepId=' + stepId + '&fillId=' + fillId + '&parentPartId=' + parentPartId +'&isSerilizeStep='+ isSerilizeStep,
            dataType: 'json',
            success: function (data) {
                if (data.ErrorMessage !== '') {
                    eLoaderError(data.ErrorMessage);
                }
                else {

                    var stepInProgress = $('#carousel ul.slides').find(".waiting").first();

                    if (stepInProgress.length > 0) {
                        ////Get next step
                        $(stepInProgress).removeClass('waiting');
                        $(stepInProgress).addClass('requested');
                        $(stepInProgress).trigger("click");
                    }

                    var stepRequested = $('[data-stepid=' + stepId + ']');

                    if (stepRequested.length > 0) {
                        $(stepRequested).addClass('step-complete');

                        ////When last step done
                        if (stepInProgress.length === 0) {
                            $(stepRequested).trigger("click");
                        }
                    }

                    eLoaderClose();
                }
            },
            error: function (error) {
                eLoaderError(error);
            }
        });
    };

    var updateSerialNumber = function (stepId, fillId, parentPartId) {

        var isValid = true;

        $('.serialize-serial-num').each(function () {
            if ($(this).val() === '') {
                isValid = false;
            }
        });

        if (!isValid) {
            eModal.alert('Please enter a Serial Number for all parts and sub-parts in this screen before continuing.');
        } else {
            $.ajax({
                type: "POST",
                url: "/wip/UpdateRootParts/?fillId=" + fillId,
                data: $('#Update-root-part').serialize(),
                dataType: 'json',
                success: function (data) {
                    stepDone(stepId, fillId, parentPartId,true);
                },
                error: function (error) {
                    eLoaderError(error);
                }
            });
        }
    };

    var stepAssume = function (stepId, fillId) {

        $.ajax({
            type: "POST",
            url: "/wip/AssumeTaskClick?fillId=" + fillId,
            dataType: 'json',
            success: function (data) {
                if (data === 'OK') {
                    window.location.href = '/wip/details/' + fillId;
                } else {
                    eLoaderError(data);
                }
            },
            error: function (error) {
                eLoaderError(error);
            }
        });
    };

    var addNewEquipmentModel = function () {

        $('#add-new-equipment-model').on('show.bs.modal',
            function (event) {
                var button = $(event.relatedTarget);
                //var id = button.data('id');
                var fillId = button.data('fill-id');
                var modal = $(this);

                $.ajax({
                    type: "GET",
                    url: '/Wip/AddEquipmentMaintenance?id=' + fillId,
                    dataType: 'html',
                    success: function (data) {
                        modal.find('.modal-body').html(data);
                    },
                    error: function (error) {
                        eLoaderError(error);
                    }
                });
            });
    };

    var addNcrModel = function () {

        $('#addNcrModal').on('show.bs.modal',
            function (event) {
                var button = $(event.relatedTarget);
                var id = button.data('id');
                var fillId = button.data('fill-id');
                var modal = $(this);

                $.ajax({
                    type: "GET",
                    url: '/Wip/AddNcrModel?parentId=' + id + '&fillid=' + fillId,
                    dataType: 'html',
                    success: function (data) {
                        modal.find('.modal-body').html(data);
                    },
                    error: function (error) {
                        eLoaderError(error);
                    }
                });
            });
    };

    function timerVisibility(data, taskId) {

        if (data.ErrorMessage !== '') {
            eModal.alert(data.ErrorMessage);
            return false;
        } else {

            $('.timer-btn').attr('data-task-log-id', data.Entity.Id);

            if (data.Entity.StatusId === 2) {
                $('#' + taskId + ' #resume-step').removeClass('hidden');
                $('#' + taskId + ' #pause-step').addClass('hidden');
            } else if (data.Entity.StatusId === 1) {
                $('#' + taskId + ' #done-step').removeClass('hidden');
                $('#' + taskId + ' #pause-step').removeClass('hidden');
                $('#' + taskId + ' #resume-step').addClass('hidden');
                $('#' + taskId + ' #start-step').addClass('hidden');
            }
        }
        return true;
    };

    var stepPause = function (id, fillId, taskId) {
        console.log(id, fillId, taskId);
        $.ajax({
            type: "POST",
            url: "/wip/StepPause?taskLogId=" + id + "&fillId=" + fillId,
            dataType: 'json',
            success: function (data) {
                timerVisibility(data, taskId);
                eLoaderClose();
            },
            error: function (error) {
                eLoaderError(error);
            }
        });
    };

    var stepResume = function (id, taskId, fillId) {

        $.ajax({
            type: "POST",
            url: "/wip/stepResume?taskLogId=" + id + "&stepId=" + taskId + "&fillId=" + fillId,
            dataType: 'json',
            success: function (data) {

                if (id === null || id === '') {

                    $('#resume-step,#pause-step,#done-step').attr('data-task-log-id', data.Entity.Id);

                    $('.timer').timer({
                        seconds: '' + data.Entity.TotalSeconds + '',
                        duration: data.Entity.Duration
                    });
                }

                timerVisibility(data, taskId);
                eLoaderClose();
            },
            error: function (error) {
                eLoaderError(error);
            }
        });
    }

    return {
        StepStartClick: stepStart,
        StepDone: stepDone,
        UpdateSerialNum: updateSerialNumber,
        StepAssumeClick: stepAssume,
        AddNewEquipmentModel: addNewEquipmentModel,
        AddNcrModel: addNcrModel,
        StepPauseClick: stepPause,
        StepResumeClick: stepResume
    }
}