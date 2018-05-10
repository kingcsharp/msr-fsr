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

                    case 'xls': imageUrl = '<span class="data-toggle file-prev" data-call-back-item="' + valueId[1] + '"  data-target="#view-images" title="View File"><img src="/assets/img/icon-xls.png" /></span>&nbsp';
                        break;
                    case 'jpg':
                    case 'png':
                    case 'jpeg':
                    case 'gif':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' + valueId[1] + '"  data-target="#view-images" title="View File"><img src="/assets/img/jpg.png" /></span>&nbsp';
                        break;

                    case 'docx':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' + valueId[1] + '"  data-target="#view-images" title="View File"><img src="/assets/img/icon-doc.png" /></span>&nbsp';
                        break;

                    case 'xlsx':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' + valueId[1] + '"  data-target="#view-images" title="View File"><img src="/assets/img/icon-xls.png" /></span>&nbsp';
                        break;

                    case 'ppt':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' + valueId[1] + '"  data-target="#view-images" title="View File"><img src="/assets/img/icon-ppt.png" /></span>&nbsp';
                        break;

                    case 'pdf':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' + valueId[1] + '"  data-target="#view-images" title="View File"><img src="/assets/img/icon-pdf.png" /></span>&nbsp';
                        break;

                    case 'txt':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' + valueId[1] + '"  data-target="#view-images" title="View File"><img src="/assets/img/txt.png" /></span>&nbsp';
                        break;

                    case 'zip':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' + valueId[1] + '"  data-target="#view-images" title="View File"><img src="/assets/img/icon-zip.png" /></span>&nbsp';
                        break;

                    default: imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' + valueId[1] + '"  data-target="#view-images" title="View File"><img src="/assets/img/default.png" /></span>&nbsp';
                        break;

                }

                imageUrls += imageUrl;
            }

            return imageUrls;
        },

        ActionFormtter: function (cellvalue, options, rowObject, returnUrl, editUrl) {

            var editButton = '';
            var deleteButton = '';
            var buttonWorkflowLeft = '';
            var buttonWorkflowRight = '';
            var url = '';

            if (rowObject.Status === 'CREATING') {

                editButton = '<a href="' + editUrl + rowObject.ObjectId + '" data-call-back-id ="' + rowObject.ObjectId + '"  class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';
                url = '/workflow/submit?objId=' + rowObject.ObjectId + '&returnUrl=' + returnUrl;
                buttonWorkflowLeft = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '"  data-call-back-id="' + rowObject.ObjectId + '" class="btn btn-xs btn-success unlock" title="Cancel Creation. Edit will be lost" style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-left"></i></a>';

                buttonWorkflowRight = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-success" title="Proceed to approval workflow for release." style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-right"></i></a>';
            }

            if (rowObject.Status == 'APPROVED') {
                editButton = '<a href="#" data-call-back-id ="' + rowObject.ObjectId + '"  class="btn btn-xs btn-success editpeople" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';

                url = '/workflow/delete?objId=' + rowObject.ObjectId + '&returnUrl=' + returnUrl;
                deleteButton = '<a href="' + url + '" data-call-back-name="' + rowObject.Name + '" class="btn btn-xs btn-danger" title="Proceed to delete." style="margin:2px;font-size: .8em;"><i class="fa fa fa-trash-o"></i></a>';
            }

            return editButton + deleteButton + buttonWorkflowLeft + buttonWorkflowRight;
        },

        UnLockWorkflow: function (returnUrl) {
            $('.unlock').on('click',
                function (e) {
                    e.preventDefault();

                    var callBackId = $(this).data('call-back-id');

                    eModal.confirm('If you proceed you will lose any edits you made.  Are you sure?')
                        .then(confirmCallback, optionalCancelCallback);

                    function confirmCallback() {
                        window.location.href =
                            "/workflow/UnlockAndDelete?objId=" + callBackId + '&returnUrl=' + returnUrl;
                    }

                    function optionalCancelCallback() {

                    }
                });
        },

        SetupGridLock: function (editUrl) {

            $('.editpeople').on('click',
                function (e) {
                    e.preventDefault();

                    var callBackId = $(this).data('call-back-id');
                    var callBackName = $(this).data('call-back-name');

                    eModal.confirm(
                        'Are You Sure? Locking prevents others from editing. Checking out create the next revision for you to edit?', 'Confirmation Edit')
                        .then(confirmCallback, optionalCancelCallback);

                    function confirmCallback() {
                        $.ajax({
                            type: "GET",
                            url: '/WorkFlow/CheckOutObject/' + callBackId,
                            dataType: 'JSON',
                            cache: false,
                            success: function (data) {
                                window.location.href = editUrl + data.ObjectId;
                            },
                            error: function (error) {
                                alert(error);
                            }
                        });

                    }

                    function optionalCancelCallback() {
                    }

                });
        },

        DocPreview: function () {

            $('.file-prev').on('click', function (event) {

                var callBackitem = $(this).data('call-back-item');

                $.ajax({
                    type: "GET",
                    url: '/Files/ViewFile?id=' + callBackitem,
                    dataType: 'json',
                    cache: false,
                    success: function (data) {
                        viewerLoad(data.FileUrl, data.FileName);
                    },
                    error: function(error) {
                        eLoaderError(error);
                    }
                });

            });
        },

        GetStatusFilters: function () {

            var filterList =
                ':[All];' +
                    'CREATING, DENIED, APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Creating or Approved;' +
                    'CREATING, DENIED: Creating;' +
                    'IN_WORKFLOW:In Approval Workflow;' +
                    'APPROVED, APPROVED_BUT_REVISING, APPROVED_BUT_DELETING:Approved;' +
                    'DENIED:Denied;' +
                    'APPROVED_BUT_REVISING:Approved But Being Revised;' +
                    'APPROVED_BUT_DELETING:Approved But Being Deleted;' +
                    'DENIED:Denied;' +
                    'DELETED:Deleted;' +
                    'OLD:Obsolete';

            return filterList;

        }
    }
