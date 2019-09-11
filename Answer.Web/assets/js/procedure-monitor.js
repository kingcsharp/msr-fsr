var ProcedureMonitor = function () {

    var initialize = function () {

        if ($('#AddMonitorForProcedureViewModel_Monitor_Type').val() === 'EQUIPMENT') {
            $('.if-number').hide();
            $('.text-target').hide();
            $('.yes-no').hide();
        }
        if ($('#AddMonitorForProcedureViewModel_Monitor_Type').val() === 'NUMBER') {
            $('.if-number').show();
            $('.text-target').hide();
            $('.yes-no').hide();
        }
        if ($('#AddMonitorForProcedureViewModel_Monitor_Type').val() === 'YES_NO') {
            $('.if-number').hide();
            $('.text-target').hide();
            $('.yes-no').show();
            $('.target-obj').hide();
        }
        if ($('#AddMonitorForProcedureViewModel_Monitor_Type').val() === 'TEXT') {
            $('.if-number').hide();
            $('.text-target').show();
            $('.yes-no').hide();
            $('.target-obj').hide();
        }
        if ($('#AddMonitorForProcedureViewModel_Monitor_Type').val() === 'PASS_FAIL') {
            $('.if-number').hide();
            $('.text-target').hide();
            $('.yes-no').hide();
            $('.target-obj').show();
        }
        if ($('#AddMonitorForProcedureViewModel_Monitor_Type').val() === 'SELECT') {

            $('.list-source').show();
            $('.if-number').hide();
            $('.text-target').hide();
            $('.yes-no').hide();
            $('.target-obj').hide();

        }

        if ($('#AddMonitorForProcedureViewModel_Should_Be').val() === 'BETWEEN' && $('#AddMonitorForProcedureViewModel_Monitor_Type').val() === 'NUMBER') {
            $('.if-not-between').hide();
            $('.if-between').show();
            $('.target-obj').hide();
        }

        $('#AddMonitorForProcedureViewModel_Monitor_Type').on('change', function () {

            if (this.value === 'NUMBER' && $('#AddMonitorForProcedureViewModel_Should_Be').val() === 'BETWEEN') {
                $('.if-number').show();
                $('.if-not-number').show();
                $('.yes-no').hide();
                $('.if-between').show();
                $('.target-obj').hide();
                $('.if-not-between').hide();
            }
            if (this.value === 'NUMBER' && $('#AddMonitorForProcedureViewModel_Should_Be').val() !== 'BETWEEN') {
                $('.if-number').show();
                $('.if-not-number').show();
                $('.yes-no').hide();
                $('.text-target').hide();
                $('.target-obj').show();
            }
            if (this.value === 'EQUIPMENT') {
                $('.if-between').hide();
                $('.if-not-between').show();
                $('.if-number').hide();
                $('.text-target').hide();
                $('.yes-no').hide();

            }
            if (this.value === 'YES_NO') {
                $('.if-between').hide();
                $('.if-not-between').show();
                $('.if-number').hide();
                $('.text-target').hide();
                $('.yes-no').show();
                $('.target-obj').hide();

            }
            if (this.value === 'TEXT') {
                $('.if-between').hide();
                $('.if-not-between').show();
                $('.if-number').hide();
                $('.text-target').show();
                $('.target-obj').hide();
                $('.yes-no').hide();
            }
            if (this.value === 'PASS_FAIL') {
                $('.if-number').hide();
                $('.if-between').hide();
                $('.if-not-number').show();
                $('.text-target').hide();
                $('.target-obj').show();
                $('.yes-no').hide();
            }

            if (this.value === 'SELECT') {
                $('.if-number').hide();
                $('.if-between').hide();
                $('.if-not-number').show();
                $('.text-target').hide();
                $('.target-obj').show();
            }


            $('#AddMonitorForProcedureViewModel_Target_Object').val('');
        });

        $('#AddMonitorForProcedureViewModel_Should_Be').on('change', function () {

            $('#AddMonitorForProcedureViewModel_Target_Object').val('');

            if (this.value === 'BETWEEN') {
                $('.if-between').show();
                $('.if-not-between').hide();
                $('.text-target').hide();
                $('.yes-no').hide();
            }
            else {
                $('.if-between').hide();
                $('.if-not-between').show();
                $('.text-target').hide();
                $('.yes-no').hide();
            }
        });

    };

    return {
        Initialize: initialize
    };

};

