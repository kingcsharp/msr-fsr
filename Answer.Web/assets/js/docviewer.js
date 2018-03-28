
(function ($, sr) {
    // debouncing function from John Hann
    // http://unscriptable.com/index.php/2009/03/20/debouncing-javascript-methods/
    var debounce = function (func, threshold, execAsap) {
        var timeout;

        return function debounced() {
            var obj = this, args = arguments;
            function delayed() {
                if (!execAsap)
                    func.apply(obj, args);
                timeout = null;
            };

            if (timeout)
                clearTimeout(timeout);
            else if (execAsap)
                func.apply(obj, args);

            timeout = setTimeout(delayed, threshold || 100);
        };
    }
    // smartresize
    jQuery.fn[sr] = function (fn) { return fn ? this.bind('resize', debounce(fn)) : this.trigger(sr); };


})(jQuery, 'smartresize');


function viewerLoad(url, flobwicket) {
    var encoder = '';
    var viewSrc = '';
    var fileType = '';

    if (typeof flobwicket !== 'undefined' && flobwicket !==null) {
        fileType = flobwicket.split('.')[1];
    }
    
    var file = url;

    var linkDescription = flobwicket;
    var description = (!linkDescription) ? '' : flobwicket; 
    var title = 'Document Preview : ' + description;
    switch (fileType) {
        //if .pdf use object embed
    case 'pdf':
        encoder = '//docs.google.com/viewerng/viewer?embedded=true&url=';
        viewSrc = encoder + encodeURIComponent(file);
        return eModal.iframe(viewSrc, title);
        break;
    //handle office docs
    case 'docx': case 'doc': case 'xlsx': case 'xls': case 'csv': case 'pptx': case 'ppt':
        encoder = '//view.officeapps.live.com/op/embed.aspx?src=';
        viewSrc = encoder + encodeURIComponent(file);
        return eModal.iframe(viewSrc, title);
        break;
    //just serve the image, no encoder needed
    case 'jpg': case 'jpeg': case 'gif': case 'png': case 'svg': 
        encoder = file;
        viewSrc = '<div class="viewer-img-div"><img src="' + encoder + '" class="img-responsive viewer-img"/></div>';
        return eModal
            .alert(viewSrc, title)
        break;

    //if all else fails, try google docs viewer
    default:
        encoder = '//docs.google.com/viewerng/viewer?embedded=true&url=';
        viewSrc = encoder + encodeURIComponent(file);
        return eModal.iframe(viewSrc, title);
    }
};

var pageEmbedsUpdate = function () {
    $('.embed').each(function () {
        var filename = $(this).attr('href');
        var fileType = filename.split('.').pop().toLowerCase();
        console.log('File Ext : ' + fileType);
        switch (fileType) {
            // if supported filetype then add data-fileType attr to this link for viewer handling in onclick
        case 'jpg': case 'gif': case 'png': case 'docx': case 'doc': case 'xlsx': case 'xls': case 'pptx': case 'ppt': case 'pdf': 
            $(this).attr('data-filetype', fileType);
            break;
        // if file type matches none of the above
        default:
            $(this).hide();
        }
    });
};