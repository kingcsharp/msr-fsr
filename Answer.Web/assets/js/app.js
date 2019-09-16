$(function () {
    $('.navbar-nav').find('li.active').removeClass('active');
    $('.navbar-nav li a[href^="' + location.pathname + '"]').parent('li').addClass('active');

    $('.navbar-nav li a').click(function () {
        $('.navbar-nav').find('li.active').removeClass('active');
        $(this).parent('li').addClass('active');
    });
});

$(function () {
  $(".navbar-expand-toggle").click(function() {
    $(".app-container").toggleClass("expanded");
    return $(".navbar-expand-toggle").toggleClass("fa-rotate-90");
  });
  return $(".navbar-right-expand-toggle").click(function() {
    $(".navbar-right").toggleClass("expanded");
    return $(".navbar-right-expand-toggle").toggleClass("fa-rotate-90");
  });
});


$(function() {
    $("[data-toggle=tooltip]").tooltip();
});

$(function () {
    $('li.active').removeClass('active');
    $('a[href="' + location.pathname + '"]').closest('li.panel.panel-default.dropdown').addClass('active');
});

// fix for wip detail header
$(function () {
    if (document.location.pathname.indexOf('/wip/details/') === 0) {
        $('li#search-control').addClass('hidden');

    }

});