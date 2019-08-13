$(function() {
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