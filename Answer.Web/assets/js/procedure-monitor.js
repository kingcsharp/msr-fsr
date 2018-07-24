var ProcedureMonitor = function () {

    var initialize = function () {

        if ($('#AddMonitorForProcedureViewModel_Monitor_Type').val() !== 'NUMBER') {
            $('.if-number').hide();
        }
        else {
            $('.if-number').show();
        }

        if ($('#AddMonitorForProcedureViewModel_Should_Be').val() === 'BETWEEN' && $('#AddMonitorForProcedureViewModel_Monitor_Type').val() === 'NUMBER') {
            $('.if-not-between').hide();
            $('.if-between').show();
        }

        $('#AddMonitorForProcedureViewModel_Monitor_Type').on('change', function () {

            if (this.value === 'NUMBER') {
                $('.if-number').show();
                $('.if-not-number').show();

            }
            else if (this.value === 'EQUIPMENT' || this.value === 'YES_NO' || this.value === 'TEXT' || this.value === 'PASS_FAIL') {
                $('.if-between').hide();
                $('.if-not-between').show();
                $('.if-number').hide();
            }
            else {
                $('.if-number').hide();
                $('.if-not-number').show();
            }
        });

        $('#AddMonitorForProcedureViewModel_Should_Be').on('change', function () {

            if (this.value === 'BETWEEN') {
                $('.if-between').show();
                $('.if-not-between').hide();
            }
            else {
                $('.if-between').hide();
                $('.if-not-between').show();
            }
        });

    };

    return {
        Initialize: initialize
    }

}

