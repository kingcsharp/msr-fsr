
function LoadSteps(i, myOptions) {

    if (i === 0) {
        manageFirstStep(myOptions);
    }
    function manageFirstStep(myOptions) {
        $('#row' + i).html("<td><select class='form-control default-template-id' name='steps[" + i + "].Process' required>" + myOptions + "</select><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "steps[" + i + "].Process" + " data-valmsg-replace=" + "true" + "></span></td><td><input type='text' name='steps[" + i + "].Step'  class='form-control' value='" + (i + 1) + "' onkeyup = 'javascript:checkType(this)' required/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "steps[" + i + "].Step" + " data-valmsg-replace=" + "true" + "></span></td><td><input type='text' name='steps[" + i + "].StandardDirectLaborMinutes' class='form-control labor-mins' onkeyup = 'javascript:checkType(this)' required='' data-val-required='The Standard Direct Labor Minutes field is required.'/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "steps[" + i + "].StandardDirectLaborMinutes" + " data-valmsg-replace=" + "true" + "></span><td><input type='texct' name='steps[" + i + "].StandardMachineMinutes' class='form-control machine-mins' onkeyup = 'javascript:checkType(this)' required/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "steps[" + i + "].StandardMachineMinutes" + " data-valmsg-replace=" + "true" + "></span></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='steps[" + i + "].ReplacementCost' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><input type='text' name='steps[" + i + "].Utilization' value='0' class='form-control numbersOnly' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>%</span></div></td><td class='text-center hidden hide-col'><input type='text' name='steps[" + i + "].UsefulLife' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='steps[" + i + "].EquipExpensePerMinute' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='steps[" + i + "].AnnualRM' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='steps[" + i + "].RMPerMinute' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td>");
        $('#process_steps').append('<tr id="row' + (i + 1) + '" data-value="" ></tr>');
        i++;

        recalculateTotals();
        manageSwitchColum();
    }

    $("#add_row").click(function () {
        var templateId = $('#defaultProcessList').val();
        getTemplateDetails(templateId);

    });

    function getTemplateDetails(value) {
        $.ajax({
            type: "GET",
            url: '/PreProSearch/GetDetailsById/' + value,
            dataType: 'Json',
            success: function (data) {
                addStep(data.ReplacementCost, data.Utilization, data.UsefulLife);
            },
            error: function () {
            }
        });
    }
    function addStep(replacementCost, utilization, usefulLife) {
        var dropdownProcess = $('#defaultProcessList').html();
        $('#row' + i).html("<td><select class='form-control default-template-id' name='steps[" + i + "].Process' required>" + dropdownProcess + "</select><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "steps[" + i + "].Process" + " data-valmsg-replace=" + "true" + "></span></td>" +
            "<td><input type='text' name='steps[" + i + "].Step'  class='form-control' value='" + (i + 1) + "' onkeyup = 'javascript:checkType(this)' required/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "steps[" + i + "].Step" + " data-valmsg-replace=" + "true" + "></span></td>" +
            "<td><input type='text' name='steps[" + i + "].StandardDirectLaborMinutes' class='form-control labor-mins' onkeyup = 'javascript:checkType(this)' required='' data-val-required='The Standard Direct Labor Minutes field is required.'/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "steps[" + i + "].StandardDirectLaborMinutes" + " data-valmsg-replace=" + "true" + "></span><td><input type='texct' name='steps[" + i + "].StandardMachineMinutes' class='form-control machine-mins' onkeyup = 'javascript:checkType(this)' required/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "steps[" + i + "].StandardMachineMinutes" + " data-valmsg-replace=" + "true" + "></span></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='steps[" + i + "].ReplacementCost' value='" + replacementCost + "' class='form-control replacement-cost' readonly='readonly' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><input type='text' name='steps[" + i + "].Utilization' value='" + utilization + "' class='form-control numbersOnly utilization' readonly='readonly' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>%</span></div></td><td class='text-center hidden hide-col'><input type='text' name='steps[" + i + "].UsefulLife' value='" + usefulLife + "' class='form-control useful-life' readonly='readonly' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='steps[" + i + "].EquipExpensePerMinute' value='0' class='form-control equip-expense-per-minute' readonly='readonly' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='steps[" + i + "].AnnualRM' value='0' class='form-control annual-rm' readonly='readonly' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='steps[" + i + "].RMPerMinute' value='0' class='form-control rm-per-minute' readonly='readonly' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td>");

        $('#process_steps').append('<tr id="row' + (i + 1) + '" data-value="" ></tr>');
        i++;
        recalculateTotals();
        manageSwitchColum();
    }

    $("#delete_row").click(function () {
        if (i >= 1) {
            var $row = $("#row" + (i - 1));
            var rowValue = $row.data('value');
            if (rowValue.length == 0) {
                $("#row" + (i - 1)).html('');
                i--;
            } else {
                eModal.confirm('Existing step can not be deleted');
            }

            if (i === 0) {
                manageFirstStep();
            }
            recalculateTotals();

        }
    });

    //init switch
    $('#showHiddenColumns').bootstrapSwitch();

    $('#showHiddenColumns').on('switchChange.bootstrapSwitch', function (e, state) {
        e.preventDefault();

        if (state) { //switch on
            $("#colSwitch").addClass('bootstrap-switch-on');
            $("#colSwitch").removeClass('bootstrap-switch-off');

            $(".hide-col").removeClass('hidden');
            $(".hide-col").removeClass('hidden');

        } else {

            $("#colSwitch").removeClass('bootstrap-switch-on');
            $("#colSwitch").addClass('bootstrap-switch-off');

            $(".hide-col").addClass('hidden');
            $(".hide-col").addClass('hidden');
        }
    });

    $(document).on("change", ".default-template-id", function () {
        var templateId = $(this).val();
        var row = $(this).closest('tr').attr('id');
        $.ajax({
            type: "GET",
            url: '/PreProSearch/GetDetailsById/' + templateId,
            dataType: 'Json',
            success: function (data) {
                $('#' + row).find('.replacement-cost').val(data.ReplacementCost);
                $('#' + row).find('.utilization').val(data.Utilization);
                $('#' + row).find('.useful-life').val(data.UsefulLife);
                recalculateTotals();
            },
            error: function () {
                addStep(0, 0, 0);
                recalculateTotals();
            }
        });
    });
    //calculations
    $(document).on("keyup", ".labor-mins", function () {
        recalculateTotals();
    });

    $(document).on("keyup", ".machine-mins", function () {
        recalculateTotals();
    });
    recalculateTotals();
    $('#userRequirementsModal').on('show.bs.modal',
        function (event) {
            var button = $(event.relatedTarget);
            var callBackId = button.data('call-back-id');
            var modal = $(this);

            $.ajax({
                type: "GET",
                url: '/Quote/ViewRequirements/' + callBackId,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });

        });

    $('#quoteModal').on('show.bs.modal',
        function (event) {

            var button = $(event.relatedTarget);
            var id = button.data('id');

            var modal = $(this);
            $.ajax({
                type: "GET",
                url: '/Quote/ViewQuote/' + id,
                dataType: 'html',
                success: function (data) {
                    modal.find('.modal-body').html(data);
                },
                error: function () {

                }
            });

        });

}
function recalculateTotals() {

    var yearsHours = parseInt($('.admin-cost-settings-years-hours').val());
    var hourMinutes = parseInt($('.admin-cost-settings-hour-minutes').val());
    var rmAnnualRate = parseFloat($('.admin-cost-settings-rm-annual-rate').val());

    $('.equip-expense-per-minute').each(function () {
        var replacementCost = parseFloat($(this).closest('tr').find('.replacement-cost').val());
        var utilization = parseFloat($(this).closest('tr').find('.utilization').val());
        var usefulLife = parseInt($(this).closest('tr').find('.useful-life').val());
        var total = (replacementCost / usefulLife) / (yearsHours * hourMinutes * utilization);
        if (isNaN(total)) {
            total = 0.00;
        }
        
        $(this).val(toFixedNumber(total));
    });

    $('.annual-rm').each(function () {
        var replacementCost = parseFloat($(this).closest('tr').find('.replacement-cost').val());
        
        var total = (replacementCost * rmAnnualRate);
        $(this).val(toFixedNumber(total));
    });

    $('.rm-per-minute').each(function () {
        var replacementCost = parseFloat($(this).closest('tr').find('.replacement-cost').val());
        var utilization = parseFloat($(this).closest('tr').find('.utilization').val());

        var total = replacementCost / (yearsHours * hourMinutes * utilization);
        if (isNaN(total)) {
            total = 0;
        }
        $(this).val(toFixedNumber(total));
    });

    var sum = 0;

    $(".labor-mins").each(function () {
        if ($(this).val() != "")
            sum += parseInt($(this).val());
    });

    $("#totalDirectMins").val(parseFloat(sum).toFixed(2));


    var rate = 2.92;
    var sum2 = (sum * rate).toFixed(2);
    $("#totalDirectDollar").val(parseFloat(sum2).toFixed(2));

    var machine = parseInt($("#standardMachineDollar").val());
    var newTotal = (sum2 + machine);
    $("#totalSalePrice").val(parseFloat(newTotal).toFixed(2));

    var sum1 = 0;
    $(".machine-mins").each(function () {
        if ($(this).val() != "")
            sum1 += parseInt($(this).val());
    });
    $("#totalMachineMins").val(parseFloat(sum1).toFixed(2));

    var rate2 = .44;
    var sum3 = (sum1 * rate2);
    $("#standardMachineDollar").val(parseFloat(sum3).toFixed(2));

    var labor1 = parseFloat($("#totalDirectDollar").val());
    var newTotal1 = (labor1 + sum3);
    $("#totalSalePrice").val(parseFloat(newTotal1).toFixed(2));


}
function manageSwitchColum() {
    if ($('#colSwitch').hasClass('bootstrap-switch-off')) {
        $(".hide-col").addClass('hidden');
        $(".hide-col").addClass('hidden');
    }
    else {
        $(".hide-col").removeClass('hidden');
        $(".hide-col").removeClass('hidden');
    }
}
function checkType(e) {
    if (e.value != e.value.replace(/[^0-9\.]/g, '')) {
        e.value = e.value.replace(/[^0-9\.]/g, '');
    }
}
function checkBlank(e) {
    if (e.value == "") {
        e.value = 0.00;
    }
}

////TODO this is hack, need to redo
function toFixedNumber(value) {
    if (value.toString().length > 4) {
        return value.toString().substring(0, 4);
    } else {
        return value;
    }
}

