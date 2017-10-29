$(document).ready(function(){
     // process specs table
     var i=1;
     $("#add_row").click(function(){
         $('#row' + i).html("<td>" + (i + 1) + "</td> <td><input name='[" + i + "].Contaminents' type='text' placeholder='Contaminents By-Products' class='form-control input-md'  /> </td><td><input name='[" + i + "].NonCU' type='text' placeholder='Non-CU or CU Risk' class='form-control input-md'  /> </td><td><input  name='[" + i + "].Material' type='text' placeholder='Material'  class='form-control input-md'></td><td><input  name='[" + i + "].ApproxDimensions' type='text' placeholder='Approx. Dimensions'  class='form-control input-md'></td><td><input  name='[" + i +"].ExistingProcess' type='text' placeholder=''Process Spec. # or New'  class='form-control input-md'></td>");
          $('#process_steps').append('<tr id="row'+(i+1)+'"></tr>');
          i++; 
     });
     $("#delete_row").click(function(){
    	 if(i>1){
		    $("#row"+(i-1)).html('');
		    i--;
		 }
	 });
	 
	 //kit table
     var j=1;
     $("#add_rowa").click(function(){
      $('#rowa-'+j).html(
          "<td>" + (j + 1) + "</td><td><input name='[" + j + "].PartDescription' type='text' placeholder='Part Description' class='form-control input-md'  /> </td><td><input name='[" + j + "].Substrate' placeholder='Substrate' class='form-control input-md'  /> </td><td><input  name='[" + j + "].CoatingSurface' type='text' placeholder='Coating/Surface Treatment'  class='form-control input-md'></td><td><input  name='[" + j + "].CustPartNo' type='text' placeholder='Cust. Part No.'  class='form-control input-md'></td><td><input  name='[" + j + "].MfgPartNo' type='text' placeholder='Mfg. Part No.'  class='form-control input-md'></td><td><input  name='[" + j +"].PartsPerKit' type='text' placeholder='# Parts per Kit'  class='form-control input-md'></td>");

      $('#kit_parts').append('<tr id="rowa-'+(j+1)+'"></tr>');
      j++; 
  });
     $("#delete_rowa").click(function(){
       if(j>1){
     $("#rowa-"+(j-1)).html('');
     j--;
     }
   });
	 
     $("#quoteDate").datepicker({
         format: 'm/d/yyyy',
     }).on('changeDate', function () {
         $(this).datepicker('hide');
     });
});