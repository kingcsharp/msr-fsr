var Msr = Msr || {};

Msr.SaveOrPartUpdate = Msr.SaveOrPartUpdate ||
    {
        AddSubPart: function () {

            $("#add-sub-part-row").click(function () {
                var index = 0;
                var rowsLength = $('#sub-parts tbody tr').length;
                if (rowsLength > 0) {
                    index = rowsLength;
                }

                var dropdownProcess = $('#defaultProcessList').html();

                $('#sub-parts').append("<tr>" +
                    "<td><select class='form-control default-template-id selectSubPart' data-val='true' name='SubPartList[" + index + "].PartId' data-val-required='The Part field is required.'>" + dropdownProcess + "</select><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "SubPartList[" + index + "].PartId" + " data-valmsg-replace=" + "true" + "></span></td>" +
                    "<td><input type='text' name='SubPartList[" + index + "].Qty'  class='form-control' data-val='true' onkeyup = 'javascript:checkType(this)' data-val-required='The Qty field is required.' data-val-number='The qty must be a number.'/><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "SubPartList[" + index + "].Qty" + " data-valmsg-replace=" + "true" + "></span></td>" +
                    "<td><input type='text' name='SubPartList[" + index + "].NickName' class='form-control' /><span class=" + "text-danger field-validation-error" + " data-valmsg-for=" + "AddSubPartViewModels[" + index + "].NickName" + " data-valmsg-replace=" + "true" + "></span></td>" +
                    "<td><button type='button'  class='btn btn-xs btn-danger' onclick = 'javascript: DeleteSubPart(this);' title=''><i class='fa fa-trash-o'></i></button></td></tr>");
                $(".selectSubPart").select2();

                var form = $("#part-form");
                form.unbind();
                form.data("validator", null);
                $.validator.unobtrusive.parse(form);
            });
        },

        DeleteSubPartData: function (e, value) {
            if (value != null && value !== '') {
                eModal.confirm('Are you sure?')
                    .then(confirmCallback, optionalCancelCallback);

                function confirmCallback() {
                    eLoaderOpen();
                    $.ajax({
                        type: "POST",
                        url: '/Parts/SubPartDelete/',
                        data: {
                            id: value
                        },
                        dataType: 'Json',
                        success: function (data) {
                            if (data === 'OK') {
                                eLoaderClose();
                                location.reload();
                            } else {
                                eLoaderError(data);
                            }
                        },
                        error: function (error) {
                            eLoaderError(error);
                        }
                    });
                }

                function optionalCancelCallback() { }

            } else {
                $(e).closest("tr").remove();
            }
        }
   }
