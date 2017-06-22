$(document).ready(function () {
    $('#monitor1-switch').bootstrapSwitch();
    $('#monitor1-switch').on('switchChange.bootstrapSwitch', function (e, state) {
        e.preventDefault();
        if(state){
            $("#1a tr.on-off-row").show();
            console.log('on');
        }else{
            $("#1a tr.on-off-row").hide();
            console.log('off');
        }
    });

    $('#monitor2-switch').bootstrapSwitch();
    $('#monitor2-switch').on('switchChange.bootstrapSwitch', function (e, state) {
        e.preventDefault();
        if(state){
            $("#2a tr.on-off-row").show();
            console.log('on');
        }else{
            $("#2a tr.on-off-row").hide();
            console.log('off');
        }
    });

    $(".nav-tabs").on("click", "a", function (e) {
            e.preventDefault();
            if (!$(this).hasClass('add-monitor')) {
                $(this).tab('show');
            }
        })
        .on("click", "span", function () {
            var anchor = $(this).siblings('a');
            $(anchor.attr('href')).remove();
            $(this).parent().remove();
            $(".nav-tabs li").children('a').first().click();
        });
    
    $('.add-monitor').click(function (e) {
        e.preventDefault();
        var id = $(".nav-tabs").children().length; //think about it ;)
        var tabId = 'monitor_' + id;
        $(this).closest('li').before('<li><a href="#monitor_' + id + '">Monitor ' + id + '</a> <span> x </span></li>');
        $('.tab-content').append('<div class="tab-pane" id="' + tabId + '"></div>');
        $("#" + tabId).loadTemplate($("#monitor-form"));
       $('.nav-tabs li:nth-child(' + id + ') a').click();
    });
});