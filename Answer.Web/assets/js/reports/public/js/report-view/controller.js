app.controller('report_viewCtrl', function ($scope, $routeParams, report_v2Model, queryModel, connection, bsLoadingOverlayService, widgetsCommon, $sessionStorage,$timeout) {

    $scope.promptsBlock = 'assets/js/reports/partials/report/promptsBlock.html';
    $scope.dateModal = 'assets/js/reports/partials/report/dateModal.html';
    $scope.linkModal = 'assets/js/reports/partials/report/linkModal.html';
    $scope.repeaterTemplate = 'assets/js/reports/partials/report/repeater.html';
    $scope.publishModal = 'assets/js/reports/partials/report/publishModal.html';
    $scope.columnFormatModal = 'assets/js/reports/partials/report/columnFormatModal.html';
    $scope.columnSignalsModal = 'assets/js/reports/partials/report/columnSignalsModal.html';
    $scope.showPrompts = true;
    $scope.selectedReport = {};
    $scope.selectedReport.query = {};
    $scope.queryModel = queryModel;
    $scope.mode = 'preview';

    $scope.getPrompts = function () {
        return $scope.selectedReport.query.groupFilters;
    }

    $scope.showOverlay = function (referenceId) {
        bsLoadingOverlayService.start({
            referenceId: referenceId
        });
    };

    $scope.hideOverlay = function (referenceId) {
        bsLoadingOverlayService.stop({
            referenceId: referenceId
        });
    };

    $scope.getReportDiv = function () {
        if ($routeParams.reportID) {
            $scope.showOverlay('OVERLAY_reportLayout');
            report_v2Model.getReportDefinition($routeParams.reportID, false, function (report) {
                if (report) {
                    $scope.selectedReport = report;
                    report_v2Model.getReport(report, 'reportLayout', $scope.mode, function () {
                        $scope.hideOverlay('OVERLAY_reportLayout');
                        var theReports = [];
                        theReports.push($scope.selectedReport);
                    });

                } else {
                    //TODO:No report found message
                }
            });
            $scope.hideOverlay('OVERLAY_reportLayout');
            //var theReports = [];

        }


    };
    $scope.dimf = {};
    $scope.elemChanged = function (domElemId, $item, colId, gridId) {
        var elem = getElemOfArr($scope['gridFilters' + gridId], 'elementID', colId);
        if (elem !== -1) {
            elem.val = $item[colId];
        }
    }

    function getElemOfArr(arr, prop, lookupVal) {
        var arrLength = arr.length;
        while (arrLength--) {
            if (arr[arrLength][prop] === lookupVal) {
                return arr[arrLength];
            }
        }
        return -1;
    }

    $scope.updateDtFilter = function ($item, colId, gridId) {
        var elem = getElemOfArr($scope['gridFilters' + gridId], 'elementID', colId);
        if (elem !== -1 && $item) {
            elem.val = moment($item._d).format('M/D/YYYY');
        }
    }
    //clearDt(\'' + columnIndex + '_\',\'' + column.id + '\',\'' + reportId + '\',\'1\',\'' + id + '\')

    $scope.clearDt = function (colIndex, colId, gridId) {
        $scope['dimf'][colId.toString() + colIndex.toString() + gridId.toString()] = undefined;
        $timeout(function () {
            //$scope['gridFilters' + gridId][(colId)] = undefined;
            var elem = getElemOfArr($scope['gridFilters' + gridId], 'elementID', colId + (dtNr === "1" ? dtNr : ''));
            if (elem !== -1) {
                elem.val = undefined;
            }
        }, 100);
    }

    $scope.clear = function ($event, $select, colId, gridId) {
        $event.stopPropagation();
        //to allow empty field, in order to force a selection remove the following line
        $select.selected = undefined;
        //reset search query
        $select.search = undefined;
        //focus and open dropdown
        $select.activate();
        //debugger;
        //$scope['gridFilters' + gridId][(colId)] = undefined;
        var elem = getElemOfArr($scope['gridFilters' + gridId], 'elementID', colId);
        if (elem !== -1) {
            elem.val = undefined;
        }
    }


    $scope.getQuery = function (queryID) {
        return queryModel.query();
    }


    /********PUBLISH******/
    $scope.publishReport = function () {
        $scope.objectToPublish = $scope.selectedReport;
        $('#publishModal').modal('show');
    }

    $scope.unPublish = function () {
        connection.post('/api/reports/unpublish', { _id: $scope.selectedReport._id }, function (data) {
            $scope.selectedReport.isPublic = false;
            $('#publishModal').modal('hide');
        });
    }

    $scope.selectThisFolder = function (folderID) {
        connection.post('/api/reports/publish-report', { _id: $scope.selectedReport._id, parentFolder: folderID }, function (data) {
            $scope.selectedReport.parentFolder = folderID;
            $scope.selectedReport.isPublic = true;
            $('#publishModal').modal('hide');
        });
    }





    $scope.processStructure = function (execute) {
        queryModel.processStructure(execute, function () {
            /*queryModel.getQueryData( function(){

            });*/
            report_v2Model.getReport($scope.selectedReport, 'reportLayout', $scope.mode, function () {
                //Done
                $scope.hideOverlay('OVERLAY_reportLayout');
                var theReports = [];
                theReports.push($scope.selectedReport);

            });
        });
    }


    /********END PUBLISH******/

    $scope.getReportColumnDefs = function (reportID) {
        return $scope.selectedReport.properties.columnDefs;
    }

    $scope.filterChanged = function (elementID, values) {

        $scope.processStructure();
    }

    $scope.getElementProperties = function (theElement) {

    }

    /*GRID DROPDOWN FUNCTIONS*/

    $scope.textAlign = widgetsCommon.textAlign;

    $scope.fontSizes = widgetsCommon.fontSizes;

    $scope.fontWeights = widgetsCommon.fontWeights;

    $scope.fontStyles = widgetsCommon.fontStyles;

    $scope.colors = widgetsCommon.colors;

    $scope.signalOptions = widgetsCommon.signalOptions;

    $scope.saveToExcel = function (reportHash) {
        report_v2Model.saveToExcel($scope, reportHash, $scope.selectedReport);
    }
    $scope.orderColumn = function (columnIndex, desc, hashedID) {
        report_v2Model.orderColumn($scope.selectedReport, columnIndex, desc, hashedID);
    };

    $scope.changeColumnStyle = function (columnIndex, hashedID) {
        report_v2Model.changeColumnStyle($scope.selectedReport, columnIndex, hashedID);
        $scope.selectedColumn = report_v2Model.selectedColumn();
        $scope.selectedColumnHashedID = report_v2Model.selectedColumnHashedID();
        $scope.selectedColumnIndex = report_v2Model.selectedColumnIndex();
    }
    $scope.setColumnFormat = function () {
        report_v2Model.repaintReport($scope.selectedReport, $scope.mode);
    }

    $scope.changeColumnColor = function (color) {
        if ($scope.selectedColumn.columnStyle)
            $scope.selectedColumn.columnStyle.color = color;
    }

    $scope.changeColumnBackgroundColor = function (color) {
        if ($scope.selectedColumn.columnStyle)
            $scope.selectedColumn.columnStyle['background-color'] = color;
    }

    $scope.gridGetMoreData = function (reportID) {
        $scope.page += 1;
        report_v2Model.getReportDataNextPage($scope.selectedReport, $scope.page, $sessionStorage.getObject('localapiparams'));
    }



});

