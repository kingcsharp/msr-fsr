setTimeout(function () { location.reload(true); }, 300000);

function DataInitBootstrapMultiselect(elem, options, callback) {
    setTimeout(function () {
        $(document).ready(function () {
            $(elem).attr('multiple', 'multiple');
            var multiselectOptions = {
                includeSelectAllOption: true,
                onInitialized: function ($aSelect, $aContainer) {
                    var $dropdown = $aContainer.find('.btn'),
                        offset = $dropdown.offset();
                    $aContainer.find('.dropdown-menu').css({
                        position: 'fixed',
                        top: (offset.top + $dropdown.outerHeight()),
                        left: offset.left
                    });
                    $(window).scroll(function () {
                        var $dropdown = $aContainer.find('.btn'),
                            offset = $dropdown.offset();
                        $aContainer.find('.dropdown-menu').css({
                            position: 'fixed',
                            top: (offset.top + $dropdown.outerHeight() - $(window).scrollTop()),
                            left: offset.left
                        });
                    });
                },
                selectAllValue: 'All',
                selectAllText: '[All]',
                allSelectedText: '[All]'
            };

            $(elem).multiselect(multiselectOptions);
        });
    }, 100);
};
//
function getUrlParams(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
}

$(function () {
    var Msr = Msr || {};

    DataInitBootstrapMultiselect($(".location-name"));

    $('[data-toggle="tooltip"]').tooltip();

    $('.wip-button').on('click', function () {

        var fillId = $(this).data('fill-id');

        if ($(this).hasClass('btn-accepted')) {

            eModal.confirm('Do you want to take ownership of this WO Item?')
                .then(function () {

                    eLoaderOpen();

                    $.ajax({
                        type: "POST",
                        url: "/wip/TakeTaskOwnersShip?fillId=" + fillId,
                        dataType: 'json',
                        success: function (data) {
                            if (data.Code === "OK") {
                                window.location.href = '/wip/Details/' + fillId;
                            } else {
                                eLoaderError(data.Message);
                            }
                        },
                        error: function (error) {
                            eLoaderError(error);
                        }
                    });
                }, null);
        } else {
            window.location.href = '/wip/Details/' + fillId;
        }
    });

    $('.location-name').change(function () {

        
        var location = $(this).val();

        if (location.length !== 0) {
            window.Cookies.set('Locations', location.toString(), { expires: 365 });
        }

        if (location !== null) {
            

            var locations = location.filter(function (el) {
                return el;
            }).join(",");
            window.location.href = '/wip/StatusView?locationName=' + locations;
        }
    });
});