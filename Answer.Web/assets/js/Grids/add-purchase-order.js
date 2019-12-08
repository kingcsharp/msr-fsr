var Msr = Msr || {};

Msr.AddPurchaseOrder = Msr.AddPurchaseOrder ||
    {
        SetUp: function (productJson) {

            $('.datepick').datetimepicker();

            $('.dateSelecter').click(function () {
                $(this).closest('td').find('.datepick').focus();
            });

            $(".select").select2({
                allowClear: false
            });

            $('#Client').on('change',
                function () {

                    var clientValue = $('#Client').val();
                    var supplierCoValue = "2";

                    $.ajax({
                        type: "GET",
                        url: '/PurchaseOrder/ProductsList/' + clientValue + '?supplierCo=' + supplierCoValue,
                        dataType: 'JSON',
                        success: function (data) {

                            $("#Products").select2("val", "");
                            $('#Products').html('');
                            $.each(data,
                                function (index, item) {
                                    var products = $('#Products');
                                    products.append("<option value='" + item.Value + "'>" + item.Text + "</option>");

                                    $('#product-message').html('');
                                });

                            if (data.length === 0) {
                                $('#product-message').html('No products found');
                            }

                        },
                        error: function () {
                            $('#product-message').html('There is an error with the request.');
                        }
                    });
                });

            var selectProducts = $(".select-products").select2({
                tags: [],
                tokenSeparators: [','],
                multiple: true,
                allowClear: true,
                placeholder: "Select Products",
                formatNoMatches: function () {
                    return '';
                }
            });

            var selectedProducts = productJson.split(",");

            selectProducts.val(selectedProducts).trigger("change");
        }
    }

function SaveCloseFun() {
    $('#SaveClose').val('true');
    $('#SaveWorkflow').val('');
    $('#Save').val('');
    return true;
}
function SaveFun() {
    $('#Save').val('true');
    $('#SaveClose').val('');
    $('#SaveWorkflow').val('');
    return true;
}
function SaveSubmitApprovalFun() {
    $('#SaveWorkflow').val('true');
    $('#Save').val('');
    $('#SaveClose').val('');
    return true;
}
