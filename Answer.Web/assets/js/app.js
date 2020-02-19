$(function () {

    $('[data-tooltip="true"]').tooltip();

    $('.navbar-nav').find('li.active').removeClass('active');
    $('.navbar-nav li a[href^="' + location.pathname + '"]').parent('li').addClass('active');

    $('.navbar-nav li a').click(function () {
        $('.navbar-nav').find('li.active').removeClass('active');
        $(this).parent('li').addClass('active');
    });

    $('li.active').removeClass('active');
    $('a[href="' + location.pathname + '"]').closest('li.panel.panel-default.dropdown').addClass('active');
    if (document.location.pathname.indexOf('/wip/details/') === 0) {
        $('li#search-control').addClass('hidden');
    }

    $(".navbar-expand-toggle").click(function() {
        $(".app-container").toggleClass("expanded");
        $(".navbar-expand-toggle").toggleClass("fa-rotate-90");
    });

    $(".navbar-right-expand-toggle").click(function () {
        $(".navbar-right").toggleClass("expanded");
        $(".navbar-right-expand-toggle").toggleClass("fa-rotate-90");
    });

});
