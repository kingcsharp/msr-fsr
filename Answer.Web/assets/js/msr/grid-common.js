var Msr = Msr || {};

Msr.JqGridCommon = Msr.JqGridCommon ||
{
    FilePreview: function (cellvalue, options, rowObject) {

        if (cellvalue == '' || cellvalue == null) {
            return '';
        }

        var imageUrl = "";
        var imageUrls = "";

        var items = cellvalue.split(',');

        for (var i = 0; i <= items.length - 1; i++) {

            var valueId = items[i].split('|');

            switch (valueId[0].split('.').pop().toLowerCase()) {

                case 'xls': imageUrl = '<a  title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/icon-xls.png" /></a>&nbsp';
                    break;
                case 'jpg':
                case 'png':
                case 'jpeg':
                case 'gif':
                    imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/jpg.png" /></a>&nbsp';
                    break;

                case 'docx':
                    imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/icon-doc.png" /></a>&nbsp';
                    break;

                case 'xlsx':
                    imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/icon-xls.png" /></a>&nbsp';
                    break;

                case 'ppt':
                    imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/icon-ppt.png" /></a>&nbsp';
                    break;

                case 'pdf':
                    imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/icon-pdf.png" /></a>&nbsp';
                    break;

                case 'txt':
                    imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/txt.png" /></a>&nbsp';
                    break;

                case 'zip':
                    imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/icon-zip.png" /></a>&nbsp';
                    break;

                default: imageUrl = '<a title="Download" href="/Doc/Download?Id=' + valueId[1] + '"><img src="/assets/img/default.png" /></a>&nbsp';
                    break;

            }

            imageUrls += imageUrl;
        }

        return imageUrls;
    }
}
