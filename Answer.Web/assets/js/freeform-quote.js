$(document).ready(function () {
     // freeform quote items table
     var i=1;
     $("#add_row").click(function(){
          $('#row'+i).html("<td>" + i +"</td><td><input type='text' name='a"+i+"' class='form-control' /></td><td><input type='text' name='b"+i+"' class='form-control'/></td><td><input type='text' name='c"+i+"' class='form-control'/></td><td><input type='text' name='d"+i+"' class='form-control'/></td><td><input type='text' name='e"+i+"' class='form-control'/></td><td><input type='text' name='f"+i+"' class='form-control'/></td>");
          $('#quote_items').append('<tr id="row'+(i+1)+'" ></tr>');
          i++;
          //recalculateTotals();
     });
     $("#delete_row").click(function(){
      	 if(i>1){
  		    $("#row"+(i-1)).html('');
  		    i--;
  		 }
  	 });
});