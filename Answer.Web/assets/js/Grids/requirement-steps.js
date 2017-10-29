$(document).ready(function () {

    var i = parseInt($('#totalRecords').val());
    if (i == 0) {
        manageFirstStep();
    }
    function manageFirstStep()
    {
        $('#row' + i).html("<td><select class='form-control' name='requirementSteps[" + i + "].Process' required><option value=''>Select a Step...</option><option value='INCOMING_INSPECTION'>INCOMING INSPECTION</option><option value='SERIALIZE'>SERIALIZE</option><option value='BEAD_BLAST'>BEAD BLAST</option><option value='SOAK_EXHAUSTED'>SOAK - EXHAUSTED</option></select><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "requirementSteps[" + i + "].Process" + " data-valmsg-replace=" + "true" + "></span></td><td><input type='text' name='requirementSteps[" + i + "].Step'  class='form-control' value='" + (i + 1) + "' onkeyup = 'javascript:checkType(this)' required/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "requirementSteps[" + i + "].Step" + " data-valmsg-replace=" + "true" + "></span></td><td><input type='text' name='requirementSteps[" + i + "].StandardDirectLaborMinutes' class='form-control labor-mins' onkeyup = 'javascript:checkType(this)' required='' data-val-required='The Standard Direct Labor Minutes field is required.'/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "requirementSteps[" + i + "].StandardDirectLaborMinutes" + " data-valmsg-replace=" + "true" + "></span><td><input type='texct' name='requirementSteps[" + i + "].StandardMachineMinutes' class='form-control machine-mins' onkeyup = 'javascript:checkType(this)' required/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "requirementSteps[" + i + "].StandardMachineMinutes" + " data-valmsg-replace=" + "true" + "></span></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='requirementSteps[" + i + "].ReplacementCost' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><input type='text' name='requirementSteps[" + i + "].Utilization' value='0' class='form-control numbersOnly' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>%</span></div></td><td class='text-center hidden hide-col'><input type='text' name='requirementSteps[" + i + "].UsefulLife' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='requirementSteps[" + i + "].EquipExpensePerMinute' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='requirementSteps[" + i + "].AnnualRM' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='requirementSteps[" + i + "].RMPerMinute' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td>");
        $('#process_steps').append('<tr id="row' + (i + 1) + '" ></tr>');
        i++;
        recalculateTotals();
        manageSwitchColum();
    }
    $("#add_row").click(function () {
        $('#row' + i).html("<td><select class='form-control' name='requirementSteps[" + i + "].Process' required><option value=''>Select a Step...</option><option value='INCOMING_INSPECTION'>INCOMING INSPECTION</option><option value='SERIALIZE'>SERIALIZE</option><option value='BEAD_BLAST'>BEAD BLAST</option><option value='SOAK_EXHAUSTED'>SOAK - EXHAUSTED</option></select><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "requirementSteps[" + i + "].Process" + " data-valmsg-replace=" + "true" + "></span></td><td><input type='text' name='requirementSteps[" + i + "].Step'  class='form-control' value='" + (i + 1) + "' onkeyup = 'javascript:checkType(this)' required/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "requirementSteps[" + i + "].Step" + " data-valmsg-replace=" + "true" + "></span></td><td><input type='text' name='requirementSteps[" + i + "].StandardDirectLaborMinutes' class='form-control labor-mins' onkeyup = 'javascript:checkType(this)' required='' data-val-required='The Standard Direct Labor Minutes field is required.'/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "requirementSteps[" + i + "].StandardDirectLaborMinutes" + " data-valmsg-replace=" + "true" + "></span><td><input type='texct' name='requirementSteps[" + i + "].StandardMachineMinutes' class='form-control machine-mins' onkeyup = 'javascript:checkType(this)' required/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "requirementSteps[" + i + "].StandardMachineMinutes" + " data-valmsg-replace=" + "true" + "></span></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='requirementSteps[" + i + "].ReplacementCost' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><input type='text' name='requirementSteps[" + i + "].Utilization' value='0' class='form-control numbersOnly' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>%</span></div></td><td class='text-center hidden hide-col'><input type='text' name='requirementSteps[" + i + "].UsefulLife' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='requirementSteps[" + i + "].EquipExpensePerMinute' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='requirementSteps[" + i + "].AnnualRM' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td><td class='text-center hidden hide-col'><div class='input-group'><span class='input-group-addon'>$</span><input type='text' name='requirementSteps[" + i + "].RMPerMinute' value='0' class='form-control' onkeyup='javascript:checkType(this)' onblur = 'javascript:checkBlank(this)'><span class='input-group-addon'>.00</span></div></td>");

        $('#process_steps').append('<tr id="row' + (i + 1) + '" ></tr>');
        i++;
        recalculateTotals();
        manageSwitchColum();
    });
    $("#delete_row").click(function () {
        if (i >= 1) {
            var sid = $("#row" + (i - 1) + " #stepId").val();
            $("#row" + (i - 1)).html('');
            if (sid != 0) {
                $.ajax({
                    type: "GET",
                    url: "/ProductionPlanning/DeleteStep?id=" + sid,
                    dataType: 'html',
                    success: function (data) {
                    },
                    error: function () {
                    }
                });
                i--;
                if (i == 0) {
                    manageFirstStep();
                }
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

    //calculations
    $(document).on("keyup", ".labor-mins", function () {
        recalculateTotals();
    });

    $(document).on("keyup", ".machine-mins", function () {
        recalculateTotals();
    });
    recalculateTotals();
});

function recalculateTotals() {
    var sum = 0;
    $(".labor-mins").each(function () {
        if ($(this).val() != "")
            sum += parseInt($(this).val());
    });


    $("#totalDirectMins").val(parseFloat(sum).toFixed(2));


    var rate = 3.76; //dont know the calcualtion math from hidden columns
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

    var rate2 = .44; //dont know the calcualtion math from hidden columns
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
function saveSubmit() {
    $('#postType').val('Save & Submit');
}
function draft() {
    $('#postType').val('Save Draft');
}
