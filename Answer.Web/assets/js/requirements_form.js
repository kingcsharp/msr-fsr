$(document).ready(function () {
    // process specs table
    var i = 1;
    $("#add_row").click(function () {
        $('#row' + i).html("<td>" + (i + 1) + "</td> <td><input name='Process[" + i + "].Contaminents' type='text' placeholder='Contaminents By-Products' class='form-control input-md'  /> </td>");
        $('#process_steps').append('<tr id="row' + (i + 1) + '"></tr>');
        i++;
    });
    $("#delete_row").click(function () {
        if (i > 1) {
            $("#row" + (i - 1)).html('');
            i--;
        }
    });

    //kit table
    var j = 1;
    $("#add_rowa").click(function () {
        $('#rowa-' + j).html(
            "<td class='text-center'>" + (j + 1) + "</td>" +
            "<td class='text-center'><input name='Parts[" + j + "].NonCU' type='radio' value='Cu'/> " +
            "<input name='Parts[" + j + "].NonCU' type='radio' value='NonCu' /></td>" +
            "<td><input name='Parts[" + j + "].PartDescription' type='text' placeholder='Part Description' class='form-control input-md'  /> </td>" +
            "<td><input name='Parts[" + j + "].Substrate' placeholder='Substrate' class='form-control input-md'  /> </td>" +
            "<td><input name='Parts[" + j + "].CoatingSurface' type='text' placeholder='Coating/Surface Treatment'  class='form-control input-md'></td>" +
            "<td><input name='Parts[" + j + "].Dimensions' type='text' placeholder='Dimensions'  class='form-control input-md'></td>" +
            "<td><input name='Parts[" + j + "].CustPartNo' type='text' placeholder='Cust. Part No.'  class='form-control input-md'></td>" +
            "<td><input name='Parts[" + j + "].MfgPartNo' type='text' placeholder='Mfg. Part No.'  class='form-control input-md'></td>" +
            "<td><input name='Parts[" + j + "].PartsPerKit' type='text' placeholder='# Parts per Kit'  class='form-control input-md'></td>");

        $('#kit_parts').append('<tr id="rowa-' + (j + 1) + '"></tr>');
        j++;
    });
    $("#delete_rowa").click(function () {
        if (j > 1) {
            $("#rowa-" + (j - 1)).html('');
            j--;
        }
    });

    $("#quoteDate").datepicker({
        format: 'm/d/yyyy',
    }).on('changeDate', function () {
        $(this).datepicker('hide');
    });
});