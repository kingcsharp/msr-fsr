(function () {
    $(document).ready(function () {
        
        $('.select').select2();
        $('#error').hide();
        $('.datepick').datetimepicker();

        $('#troubleStateSwitch').bootstrapSwitch();

        if ($('input#troubleStateSwitch').bootstrapSwitch('state')) $('#preventative').hide();
        else $('#preventative').show();

        $('#troubleStateSwitch').on('switchChange.bootstrapSwitch', function () {
            if ($("#troubleStateSwitch").is(':checked')) {
                $('#preventative').hide();
                $('#PemLastCompletedDate, #FrequencyField').val('').attr("disabled", true);
            }
            else {
                $('#preventative').show();
                $('#PemLastCompletedDate, #FrequencyField').attr("disabled", false);
                $('#EquipmentMaintenance').bootstrapValidator('revalidateField', 'PemLastCompletedDate');
                $('#EquipmentMaintenance').bootstrapValidator('revalidateField', 'FrequencyField');
            }
        });

        $("#DateTime").on("dp.change", function (e) {
            $('#EquipmentMaintenance').bootstrapValidator('revalidateField', 'DateTime');
        });
        $("#PemLastCompletedDate").on("dp.change", function (e) {
            $('#EquipmentMaintenance').bootstrapValidator('revalidateField', 'PemLastCompletedDate');
        });

        $('#EquipmentMaintenance')
            .find('[name="RoomEquipmentId"]')
            .select2()
            .change(function () {
                $('#EquipmentMaintenance').bootstrapValidator('revalidateField', 'RoomEquipmentId');
            })
            .end()
            .bootstrapValidator({
                excluded: [':disabled', ':hidden', ':not(:visible)'],
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                live: 'enabled',
                fields: {
                    RoomEquipmentId: {
                        validators: {
                            notEmpty: {
                                message: 'The Room/Equipment field must have a value.'
                            }
                        }
                    },
                    DateTime: {
                        validators: {
                            notEmpty: {
                                message: 'The Creation Date is required.'
                            },
                            date: {
                                format: 'MM/DD/YYYY hh:mm a',
                                message: 'The Creation Date is not valid (MM/DD/YYYY hh:mm a).'
                            }
                        }
                    },
                    PemLastCompletedDate: {
                        validators: {
                            notEmpty: {
                                message: 'The PM Last Completed Date is required.'
                            },
                            date: {
                                format: 'MM/DD/YYYY hh:mm a',
                                message: 'The PM Last Completed Date is not valid (MM/DD/YYYY hh:mm a).'
                            }
                        }
                    },
                    FrequencyField: {
                        validators: {
                            notEmpty: {
                                message: 'The Frequency in days is required.'
                            },
                            between: {
                                min: 1,
                                max: 730,
                                message: 'The Frequency must be between 1 and 730 days'
                            }
                        }
                    }
                }
            });

        $('#ScanBarcode').on('keyup', function () {
            if ($(this).val().length > 3) {
                var id = $(this).val();
                eLoaderOpen();
                $.ajax({
                    type: "POST",
                    url: "/EquipmentMaintenance/GetScanBarCodeLocations/" + id,
                    success: function (data) {
                        if (data.locationId == null) {
                            $('#error').html('Location not found by barcode provided. You should manually select the Location below');
                            $('#barcode-info').hide();
                            $('#error').show();
                            $("#RoomEquipmentId").val([]).trigger('change'); //reset on fail
                            $('#EquipmentMaintenance').bootstrapValidator('revalidateField', 'RoomEquipmentId');
                        } else {
                            data.locationId
                            if (data.locationId != null) {
                                $("#RoomEquipmentId").val(data.locationId).trigger('change');
                                $('#EquipmentMaintenance').bootstrapValidator('revalidateField', 'RoomEquipmentId');
                                $('#barcode-info').show();
                                $('#error').hide();
                                $("#DateTime").focus();
                            }
                        }
                        eLoaderClose();
                    },
                    error: function (error) {
                        eLoaderError(error);
                    }
                });
            }
        });
    });
})();