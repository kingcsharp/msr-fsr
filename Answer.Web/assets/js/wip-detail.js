$(window).load(function() {
  // The slider being synced must be initialized first
  $('#carousel').flexslider({
    animation: "slide",
    controlNav: false,
    animationLoop: false,
    slideshow: false,
    itemWidth: 150,
    itemHeight: 60,
    itemMargin: 5,
    asNavFor: '#slider'
  });
 
  $('#slider').flexslider({
    animation: "fade",
    controlNav: false,
    animationLoop: false,
    slideshow: false,
    sync: "#carousel"
  });


});
//document ready
$(function() {
    $('.selectpicker').selectpicker();
    
    $('#wip-item-select').on('changed.bs.select', function (e) {
        currVal = $(this).children('option:selected').data('content');
        console.log("fired select change" + currVal);
        var term = /Complete/;
        var exists = term.test(currVal);
        if(!exists) {
            $('.wip-detail-item-controls').show();
            bootbox.confirm({
                message: "There is one or more NCR's related to this WO Item. would you like to view them now?",
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className: 'btn-success'
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn-danger'
                    }
                },
                callback: function (result) {
                    console.log('This was logged in the callback: ' + result);
                    if (result) {
                        $("#ncrModal").modal()
                    }
                }
            });
        }
        else {
         $('.wip-detail-item-controls').hide();
        }
    });
    
    //$('#fileupload').fileupload({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},
        //url: 'server/php/'
    //});
    
    //init switch
    $('#showCompletedSwitch').bootstrapSwitch();
    
    $('#showCompletedSwitch').on('switchChange.bootstrapSwitch', function (e, state) {
        e.preventDefault();
        if(state){
            $("#wip-item-select > optgroup > option[data-status='Complete']").hide();
            console.log('on');
        }else{
            $("#wip-item-select > optgroup > option[data-status='Complete']").show();
            console.log('off');
        }
        $('.selectpicker').selectpicker('refresh');
    });

    $('[data-toggle="tooltip"]').tooltip();
    $( ".wip-button" ).on('doubletap', function() {
        console.log("closing modal and loading wip detail ");
        $('#wipListModal').modal("hide");
    });
    
    
	var hasTimer = false;
	// Init timer start
	$('.start-timer-btn').on('click', function() {
		hasTimer = true;
		$('.timer').timer({
			editable: true
		});
		$(this).addClass('hidden');
		$('.pause-timer-btn, .remove-timer-btn').removeClass('hidden');
	});

	// Init timer resume
	$('.resume-timer-btn').on('click', function() {
		$('.timer').timer('resume');
		$(this).addClass('hidden');
		$('.pause-timer-btn, .remove-timer-btn').removeClass('hidden');
	});


	// Init timer pause
	$('.pause-timer-btn').on('click', function() {
		$('.timer').timer('pause');
		$(this).addClass('hidden');
		$('.resume-timer-btn').removeClass('hidden');
	});

	// Remove timer
	$('.remove-timer-btn').on('click', function() {
		hasTimer = false;
		$('.timer').timer('remove');
		$(this).addClass('hidden');
		$('.start-timer-btn').removeClass('hidden');
		$('.pause-timer-btn, .resume-timer-btn').addClass('hidden');
	});

	// Additional focus event for this demo
	$('.timer').on('focus', function() {
		if(hasTimer) {
			$('.pause-timer-btn').addClass('hidden');
			$('.resume-timer-btn').removeClass('hidden');
		}
	});

	// Additional blur event for this demo
	$('.timer').on('blur', function() {
		if(hasTimer) {
			$('.pause-timer-btn').removeClass('hidden');
			$('.resume-timer-btn').addClass('hidden');
		}
	});
	
    
	$('#wioDetailPrintTraveler').on('hidden.bs.modal', function (event) {
	    $(this).data('bs.modal', null);
	});

	$('#wioDetailPrintTraveler').on('show.bs.modal', function (event) {
	    var button = $(event.relatedTarget);
	    var id = button.data('id');
	    var modal = $(this);

	    $.ajax({
	        type: "GET",
	        url: '/wip/printTraveler?id=' + id,
	        dataType: 'html',
	        success: function (data) {
	            modal.find('.modal-body').html(data);
	        },
	        error: function () {

	        }
	    });
	});




});
function openNav() {
    document.getElementById("wip-side-nav").style.width = "250px";
}

/* Set the width of the side navigation to 0 */
function closeNav() {
    document.getElementById("wip-side-nav").style.width = "0";
}

