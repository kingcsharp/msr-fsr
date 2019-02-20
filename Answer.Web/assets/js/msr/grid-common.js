var Msr = Msr || {};
var hasGridLoaded = false;
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
                    case 'xls':
                        imageUrl = '<span class="data-toggle file-prev" data-call-back-item="' +
                            valueId[1] +
                            '"  data-target="#view-images" title="View File"><img src="/assets/img/icon-xls.png" /></span>&nbsp';
                        break;
                    case 'jpg':
                    case 'png':
                    case 'jpeg':
                    case 'gif':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' +
                            valueId[1] +
                            '"  data-target="#view-images" title="View File"><img src="/assets/img/jpg.png" /></span>&nbsp';
                        break;

                    case 'docx':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' +
                            valueId[1] +
                            '"  data-target="#view-images" title="View File"><img src="/assets/img/icon-doc.png" /></span>&nbsp';
                        break;

                    case 'xlsx':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' +
                            valueId[1] +
                            '"  data-target="#view-images" title="View File"><img src="/assets/img/icon-xls.png" /></span>&nbsp';
                        break;

                    case 'ppt':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' +
                            valueId[1] +
                            '"  data-target="#view-images" title="View File"><img src="/assets/img/icon-ppt.png" /></span>&nbsp';
                        break;

                    case 'pdf':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' +
                            valueId[1] +
                            '"  data-target="#view-images" title="View File"><img src="/assets/img/icon-pdf.png" /></span>&nbsp';
                        break;

                    case 'txt':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' +
                            valueId[1] +
                            '"  data-target="#view-images" title="View File"><img src="/assets/img/txt.png" /></span>&nbsp';
                        break;

                    case 'zip':
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' +
                            valueId[1] +
                            '"  data-target="#view-images" title="View File"><img src="/assets/img/icon-zip.png" /></span>&nbsp';
                        break;

                    default:
                        imageUrl = '<span class="data-toggle  file-prev" data-call-back-item="' +
                            valueId[1] +
                            '"  data-target="#view-images" title="View File"><img src="/assets/img/default.png" /></span>&nbsp';
                        break;

                }

                imageUrls += imageUrl;
            }

            return imageUrls;
        },

        ActionFormtter: function (cellvalue, options, rowObject, returnUrl, editUrl, showDelete) {

            var editButton = '';
            var deleteButton = '';
            var buttonWorkflowLeft = '';
            var buttonWorkflowRight = '';
            var url = '';

            if (rowObject.Status === 'CREATING') {

                editButton =
                    '<a href="' +
                    editUrl +
                    rowObject.ObjectId +
                    '" data-call-back-id ="' +
                    rowObject.ObjectId +
                    '"  class="btn btn-xs btn-success" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';
                url = '/workflow/submit?objId=' + rowObject.ObjectId + '&returnUrl=' + returnUrl;
                buttonWorkflowLeft = '<a href="' +
                    url +
                    '" data-call-back-name="' +
                    rowObject.Name +
                    '"  data-call-back-id="' +
                    rowObject.ObjectId +
                    '" class="btn btn-xs btn-success unlock" title="Cancel Creation. Edit will be lost" style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-left"></i></a>';

                buttonWorkflowRight = '<a href="' +
                    url +
                    '" data-call-back-name="' +
                    rowObject.Name +
                    '" class="btn btn-xs btn-success" title="Proceed to approval workflow for release." style="margin:2px;font-size: .8em;"><i class="fa fa-arrow-right"></i></a>';
            }

            if (rowObject.Status == 'APPROVED') {
                editButton = '<a href="#" data-call-back-id ="' +
                    rowObject.ObjectId +
                    '"  class="btn btn-xs btn-success editpeople" style="margin:2px;font-size: .8em;"><i class="fa fa-edit"></i></a>';
                if (showDelete) {
                    url = '/workflow/delete?objId=' + rowObject.ObjectId + '&returnUrl=' + returnUrl;
                    deleteButton =
                        '<a href="' +
                        url +
                        '" data-call-back-name="' +
                        rowObject.Name +
                        '" class="btn btn-xs btn-danger" title="Proceed to delete." style="margin:2px;font-size: .8em;"><i class="fa fa fa-trash-o"></i></a>';
                }

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
                        'Are You Sure? Locking prevents others from editing. Checking out create the next revision for you to edit?',
                        'Confirmation Edit')
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

            $('.file-prev').on('click',
                function (event) {

                    var callBackitem = $(this).data('call-back-item');

                    $.ajax({
                        type: "GET",
                        url: '/Files/ViewFile?id=' + callBackitem,
                        dataType: 'json',
                        cache: false,
                        success: function (data) {
                            viewerLoad(data.FileUrl, data.FileName);
                        },
                        error: function (error) {
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
        },
        SaveGridSate: function (type) {
            $.jgrid.saveState(type, { saveData: false });
        },
        LoadGridSate: function (type) {
            var loadGridSate = $.jgrid.loadState(type);
            if (loadGridSate !== null) {
                $.jgrid.loadState(type, { restoreData: false });
            }
        },
        TriggerSaveLoadGridState: function (id) {

            $('#' + id).jqGrid('setGridWidth', $(window).innerWidth() - 100);

            if (!hasGridLoaded) {
                hasGridLoaded = true;
                var intervalInMilliSeconds = 100;

                var myinterval = setInterval(function () {

                    Msr.JqGridCommon.LoadGridSate(id);

                    clearInterval(myinterval);

                    $("#" + id).setGridParam({
                        datatype: 'json'
                    }).trigger("reloadGrid");
                },
                    intervalInMilliSeconds);

            } else {
                Msr.JqGridCommon.SaveGridSate(id);
            }
        },
        ClearGridState: function (gridId) {
            var storageRemoveElement = "jqGrid" + gridId;
            localStorage.removeItem(storageRemoveElement);

            var storageRemoveElementData = storageRemoveElement + "_data";
            localStorage.removeItem(storageRemoveElementData);

            location.reload();
        },
        UpdateGridStateOnColumnsHideShow: function (id) {
            $("body").click(function (event) {
                if (event.target.type === 'checkbox' && (event.target.checked === true || event.target.checked === false)) {
                    Msr.JqGridCommon.TriggerSaveLoadGridState(id);
                }
            });
        },
        BindClearGridState: function (id) {
            $('#clear-state').click(function () {
                Msr.JqGridCommon.ClearGridState(id);
            });
        },
        BindGridEvents: function (id) {

            Msr.JqGridCommon.UpdateGridStateOnColumnsHideShow(id);

            Msr.JqGridCommon.BindClearGridState(id);

            $("#" + id).navGrid("#jq-grid-pager",
                {
                    search: false,
                    add: false,
                    edit: false,
                    del: false,
                    refresh: true
                },
                {}, // edit options
                {}, // add options
                {}, // delete options
                { multipleSearch: true }
            );
            $("#" + id).jqGrid('filterToolbar',
                {
                    stringResult: true,
                    searchOnEnter: true,
                    searchOperators: true
                });
        },
        DataInitDatePicker: function (elem) {
            $(elem).datepicker({
                format: 'm/d/yyyy',
                autoclose: true
            })
                .on('hide',
                    function () {
                        if (!this.firstHide) {
                            if (!$(this).is(":focus")) {
                                this.firstHide = true;
                                // this will inadvertently call show (we're trying to hide!)
                                this.focus();
                            }
                        } else {
                            this.firstHide = false;
                        }
                    })
                .on('show',
                    function () {
                        if (this.firstHide) {
                            // careful, we have an infinite loop!
                            $(this).datepicker('hide');
                        }
                    });
        },
        DataInitBootstrapMultiselect: function (elem, options, callback) {
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
                        }
                    };
                    Object.assign(multiselectOptions, options);
                    $(elem).multiselect(multiselectOptions);
                    //$('.multiselect-container.dropdown-menu li:eq(1) input').hide();
                    setTimeout(function () {
                        $(elem).parent().parent().find('button>span').text('None Selected');
                        $(elem).parent().parent().find('input').prop('checked', false);
                        $(elem).parent().parent().find('.multiselect-container.dropdown-menu>li').removeClass('active');
                        if (callback) {
                            callback(elem);
                        }
                    }, 100);
                });
            }, 100);
        },
        DataInitMultiselect: function (elem) {
            setTimeout(function () {
                var $elem = $(elem), id = elem.id,
                    inToolbar = typeof id === "string" && id.substr(0, 3) === "gs_",
                    options = {
                        selectedList: 2,
                        height: "auto",
                        noneSelectedText: "",
                        //header: false,
                        open: function () {
                            var $menu = $(".ui-multiselect-menu:visible");
                            $menu.width("auto");
                            return;
                        }
                    },
                    $options = $elem.find("option");
                if ($options.length > 0 && $options[0].selected) {
                    $options[0].selected = false; // unselect the first selected option

                }
                if (inToolbar) {
                    options.minWidth = 'auto';
                }
                $elem.multiselect(options);
                $elem.multiselect("uncheckAll");
                $elem.siblings('button.ui-multiselect').css({
                    width: inToolbar ? "98%" : "100%",
                    marginTop: "1px",
                    marginBottom: "1px",
                    paddingTop: "3px"
                });
            }, 2000);
        },
        GetColumnIndexByName: function (columnName) {
            var cm = $(this).jqGrid('getGridParam', 'colModel'), i, l = cm.length;
            for (i = 0; i < l; i += 1) {
                if (cm[i].name === columnName) {
                    return i; // return the index
                }
            }
            return -1;
        },
        ModifySearchingFilter: function (separator, column) {

            var myDefaultSearch = "cn";
            var rulesArray = [];
            var statusArray = [];
            var i, r, l, rules, rule, parts, j, str, iCol, cmi, cm = this.p.colModel, filters, rulesArraystring;

            if (this.p.postData.filters != undefined) {
                var string = new String(this.p.postData.filters);
                filters = $.parseJSON(string);

                for (var key in filters.rules) {
                    if (filters.rules.hasOwnProperty(key)) {
                        rulesArray.push(filters.rules[key]);
                    }
                }

                rulesArraystring = JSON.stringify(rulesArray);
                rulesArraystring = rulesArraystring.replace("[", "").replace("]", "");

                //multiselect-container dropdown-menu
                $('.multiselect-native-select').each(function (e) {
                    if ($(this).find('select[name="LocationNames"]').length > 0) {
                        $(this).find('.multiselect-container.dropdown-menu li input[type=checkbox]:checked').each(function (e) {
                            if (jQuery.inArray($(this).val(), statusArray) !== -1) {

                            } else {
                                if ($(this).val() === "") {
                                    statusArray.push("true");
                                }
                                else {
                                    statusArray.push($(this).val());
                                }
                            }
                        });
                    }
                });

                //$('.multiselect-container.dropdown-menu li input[type=checkbox]:checked').each(function (e) {
                //    if (jQuery.inArray($(this).val(), statusArray) !== -1) {

                //    } else {
                //        if ($(this).val() === "") {
                //            statusArray.push("true");
                //        }
                //        else {
                //            statusArray.push($(this).val());
                //        }
                //    }
                //});

                $('.ui-multiselect-checkboxes li input[type=checkbox]:checked').each(function (e) {
                    if (jQuery.inArray($(this).val(), statusArray) !== -1) {

                    } else {
                        if ($(this).val() === "") {
                            statusArray.push("true");
                        }
                        else {
                            statusArray.push($(this).val());
                        }
                    }
                });

                filters.rules.push(
                    {
                        data: statusArray.toString(),
                        op: "eq",
                        field: column
                    }
                );

                for (r = 0; r < rulesArraystring; i++) {
                    console.log(rulesArraystring[0]);
                    if (rulesArraystring[0] !== column) {
                        filters.rules.push(
                            {
                                field: rulesArraystring[0],
                                op: rulesArraystring[1],
                                data: rulesArraystring[2]

                            }
                        );
                    }
                }
            }

            if (filters && filters.rules !== undefined && filters.rules.length > 0) {
                rules = filters.rules;
                for (i = 0; i < rules.length; i++) {
                    rule = rules[i];
                    iCol = Msr.JqGridCommon.GetColumnIndexByName.call(this, rule.field);
                    cmi = cm[iCol];
                    if (iCol >= 0 &&
                        ((cmi.searchoptions === undefined || cmi.searchoptions.sopt === undefined)
                            && (rule.op === myDefaultSearch)) ||
                        (typeof (cmi.searchoptions) === "object" &&
                            $.isArray(cmi.searchoptions.sopt) &&
                            cmi.searchoptions.sopt[0] === rule.op)) {
                        // make modifications only for the 'contains' operation
                        parts = rule.data.split(separator);
                        if (parts.length > 1) {

                            for (j = 0, l = parts.length; j < l; j++) {
                                str = parts[j];
                                if (str) {
                                    // skip empty '', which exist in case of two separaters of once
                                    filters.rules.push({
                                        data: parts[j],
                                        op: rule.op,
                                        field: rule.field
                                    });
                                }
                            }
                            rules.splice(i, 1);
                            i--; // to skip i++
                        }
                    }
                }
                this.p.postData.filters = JSON.stringify(filters);
            }
        }
    }
