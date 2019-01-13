(function (app) {
    app.filter('unique', function () {
        // we will return a function which will take in a collection
        // and a keyname
        return function (collection, keyname) {
            // we define our output and keys array;
            var output = [], keys = [];
            debugger;
            // we utilize angular's foreach function
            // this takes in our original collection and an iterator function
            angular.forEach(collection, function (item) {
                // we check to see whether our object exists
                var key = item[keyname];
                // if it's not already part of our keys array
                if (keys.indexOf(key) === -1) {
                    // add it to our keys array
                    keys.push(key);
                    // push this item to our final output array
                    output.push(item);
                }
            });
            // return our array which should be devoid of
            // any duplicates
            return output;
        };
    });
})(app || {});

app.service('grid', ['$sce', function ($sce) {

    var colClass = '';
    var colWidth = '';
    var hashedID = '';
    var columns = [];
    var report = {};


    this.trustHtml = function (html) {
        return $sce.trustAsHtml(html);
    }

    function quotedHashedID() {
        return "'" + hashedID + "'";
    }

    this.refresh = function (columns, id, query, designerMode, properties) {
        this.simpleGrid(columns, id, query, designerMode, properties, function () { });
    }

    function replaceAll(str, find, replace) {
        return str.replace(new RegExp(find, 'g'), replace);
    }

    this.extendedGridV2 = function (report, mode) {
        report = report;
        if (report.id == undefined)
            var id = report._id;
        else
            var id = report.id;

        hashedID = report.query.id;
        var theProperties = report.properties;
        var pageBlock = "page-block";


        if (mode == 'preview') {
            pageBlock = "";
        }


        var reportStyle = 'width:100%;padding-left:0px;padding-right:0px;';
        var headerStyle = 'width:100%;padding-left:0px;background-color:#ccc;';
        var rowStyle = 'width:100%;padding:0px';
        var columnDefaultStyle = 'height:40px;overflow:hidden;padding:2px; border-bottom: 1px solid #ccc;border-right: 1px solid #ccc;';


        if (!theProperties.backgroundColor) theProperties.backgroundColor = "#FFFFFF";
        if (!theProperties.height) theProperties.height = 400;
        if (!theProperties.headerHeight) theProperties.headerHeight = 60;
        if (!theProperties.rowHeight) theProperties.rowHeight = 20;
        if (!theProperties.headerBackgroundColor) theProperties.headerBackgroundColor = "#FFFFFF";
        if (!theProperties.headerBottomLineWidth) theProperties.headerBottomLineWidth = 4;
        if (!theProperties.headerBottomLineColor) theProperties.headerBottomLineColor = "#999999";
        if (!theProperties.rowBorderColor) theProperties.rowBorderColor = "#CCCCCC";
        if (!theProperties.rowBottomLineWidth) theProperties.rowBottomLineWidth = 1;
        if (!theProperties.columnLineWidht) theProperties.columnLineWidht = 0;

        //margins
        //paddings

        if (theProperties) {
            reportStyle += 'background-color:' + theProperties.backgroundColor + ';';
            //reportStyle += 'height:'+theProperties.height+'px;';

            var theRepeatHeight = theProperties.height - theProperties.headerHeight;
            repeatHeight = 'height:' + theRepeatHeight + 'px;';

            columnDefaultStyle += 'height:' + theProperties.rowHeight + 'px;';
            var paddingTop = (theProperties.rowHeight - 14) / 2;
            columnDefaultStyle += 'padding-top:' + paddingTop + 'px;';

            headerStyle += 'background-color:' + theProperties.headerBackgroundColor + ';';
            headerStyle += 'height:' + theProperties.headerHeight + 'px;';
            headerStyle += 'border-bottom: ' + theProperties.headerBottomLineWidth + ';';

            columnDefaultStyle += 'border-bottom: ' + theProperties.rowBottomLineWidth + 'px solid ' + theProperties.rowBorderColor + ';';
            columnDefaultStyle += 'border-right: ' + theProperties.columnLineWidht + 'px solid ' + theProperties.rowBorderColor + ';';
        }


        var htmlCode = '<div ' + pageBlock + ' id="REPORT_' + id + '" ndType="extendedGrid" class="container-fluid report-container" style="min-height: 60vh;' + reportStyle + '">';
        htmlCode +=
            '<a class="btn btn-success help-btn" style="top:-41px;right: 5px;position: absolute;cursor: pointer;font-size: 18px;width: 187px;height: 32px;padding-top:3px;" title="Export table to excel" ng-click="saveToExcel(\'' +
            hashedID + '\',\'' + report.id + '\')"><i class="fa fa-file-excel-o"></i> Export to Excel</a>';
        columns = report.properties.columns;
        if (columns.length > 4)
            colWidth = 'width:' + 100 / columns.length + '%;float:left;';
        else
            colClass = 'col-xs-' + 12 / columns.length;
        var reportId = 'report' + createId();
        //header
        htmlCode += '<div ng-init="' + reportId + '= getQuery(\'' + hashedID + '\').data; gridFilters' + reportId + ' = ' + createFilter(columns) + '" class="container-fluid" style="' + headerStyle + '">';

        for (var i = 0; i < columns.length; i++) {
            htmlCode += getHeaderColumn(columns[i], i, report, reportId);
        }

        htmlCode += '</div>';

        htmlCode += '<div  vs-repeat style="width:100%;overflow-y: auto;border: 1px solid #ccc;align-items: stretch;position: absolute;bottom: 0px;top:60px;" scrolly="gridGetMoreData(\'' + id + '\')">';
        //| filter:gridFilters | orderBy:getReport(\'' + hashedID + '\').predicate:getReport(\'' + hashedID + '\').reverse
        htmlCode += '<div ndType="repeaterGridItems" class="repeater-data container-fluid" ng-repeat="item in ' + reportId + '| filter: gridFilters' + reportId + ' | orderBy:getReport(\'' + hashedID + '\').predicate:getReport(\'' + hashedID + '\').reverse" style="' + rowStyle + '"  >';

        for (var i = 0; i < columns.length; i++) {
            htmlCode += getDataCell(columns[i], id, i, columnDefaultStyle);
        }

        htmlCode += '</div>';

        htmlCode += '<div ng-if="getQuery(\'' + hashedID + '\').data.length == 0" >No data found</div>';

        htmlCode += '</div>';

        htmlCode += '<div class="repeater-data">';
        for (var i in columns) {
            //var elementName = columns[i].collectionID.toLowerCase()+'_'+columns[i].elementName;
            var elementID = 'wst' + columns[i].elementID.toLowerCase();
            var elementName = elementID.replace(/[^a-zA-Z ]/g, '');
            //var elementName = 'wst'+columns[i].elementID.toLowerCase();
            if (columns[i].aggregation)
                //elementName = columns[i].collectionID.toLowerCase()+'_'+columns[i].elementName+columns[i].aggregation;
                elementName = elementName + columns[i].aggregation;
            htmlCode += '<div class=" calculus-data-column ' + colClass + ' " style="' + colWidth + '"> ' + calculateForColumn(report, i, elementName) + ' </div>';
        }
        htmlCode += '</div> </div>';
        return htmlCode;

    }

    function createFilter(columns) {
        var filter = '{';
        for (var i = 0; i < columns.length; i++) {
            filter += "\'" + columns[i].id + "\':" + columns[i].id + i;
            if (i !== columns[i].length) {
                filter += ',';
            }
        }
        filter += '}';
        return filter;
    }

    function getHeaderColumn(column, columnIndex, report, reportId) {
        var htmlCode = '';
        //var elementName = "'"+column.id+"'";
        var elementID = 'wst' + column.elementID.toLowerCase();
        var elementName = elementID.replace(/[^a-zA-Z ]/g, '');
        if (column.aggregation)
            //elementName = "'"+column.collectionID.toLowerCase()+'_'+column.elementName+column.aggregation+"'";
            elementName = "'" + elementName + column.aggregation + "'";
        var elementNameAux = elementName;
        if (column.elementType === 'date')
            elementNameAux = "'" + 'wst' + column.elementID + '_original' + "'";
        htmlCode += '<div class="' + colClass + ' report-repeater-column-header" style="' + colWidth + '">' +
            '<table style="table-layout:fixed;width:100%">' +
            '<tr>' +
            '<td style="overflow:hidden;white-space: nowrap;width:95%;">' + column.objectLabel +
            '<div class="filters">' +
            renderFilter(column, columnIndex, report, reportId) +
            '<div class="resetF">' +
            //'<a title="Reset Search Value" style="padding-right: 0.3em;padding-left: 0.3em;" ng-click="gridFilters' + reportId + '.' + column.id+ '=\'\'" class="clearsearchclass">x</a>' +
            '<div/>' +
            '</div>' +
            '</td>' +
            '</tr>' +
            '</table>' +
            '</div>';

        return htmlCode;
    }

    function renderFilter(column, columnIndex, report, reportId) {
        if (report.query.data.length < 1) {
            return smartDropdownFilter(column, columnIndex, reportId);
        }

        var valIsDate = isDate(report.query.data[0][column.id]);
        if (valIsDate) {
            return getDateTimeFilter(column, columnIndex, reportId);
        } else {
            return smartDropdownFilter(column, columnIndex, reportId);
        }

    }

    function getDateTimeFilter(column, columnIndex, reportId) {
        //dimf.
        var a = '<div style="width:75%!important;position:relative;">' +
            '<input change="updateDtFilter(dimf.' + column.id + columnIndex + ',\'' + column.id + '\',\'' + reportId + '\')" class="form-control" format="M/D/YYYY" ng-model="dimf.' +
            column.id +
            columnIndex +
            '" ng-model-options="{ updateOn: \'blur\' }" placeholder="M/D/YYYY" moment-picker="' +
            column.id +
            columnIndex +
            '">' +
            '<a class="btn btn-xs btn-link pull-right delbtn" ng-click="clearDt(\'' + columnIndex + '\',\'' + column.id + '\',\'' + reportId + '\') "><i class=" glyphicon glyphicon-remove"></i></a >' +
            '</div>';
        return a;
    }

    function smartDropdownFilter(column, columnIndex, reportId) {

        return '<div class="ui-selectDr">' +
            '<ui-select append-to-body="true" ng-model="dimf.' + column.id + columnIndex +
            '"on-select="elemChanged($item,\'' + column.id + '\',\'' + reportId + '\')">' +
            '<ui-select-match placeholder="Search...">{{ $select.selected.' + column.id + '}} ' +
            '<a class="btn btn-xs btn-link pull-right delbtn" ng-click="clear($event, $select,\'' + column.id + '\',\'' + reportId + '\') "> <i class=" glyphicon glyphicon-remove"></i></a >' +
            '</ui-select-match>' +
            '<ui-select-choices repeat="elem.' + column.id + ' as item in ' + reportId + '| unique:\'' + column.id + '\' | filter:gridFilters' + reportId + '| filter:$select.search | orderBy:getReport(\'' +
            hashedID + '\').predicate:getReport(\'' + hashedID + '\').reverse">' +
            '<div ng-bind="item.' + column.id + '"></div>' +
            '</ui-select-choices>' +
            '</ui-select>' +
            '</div>';
    }

    function createId() {
        var text = "";
        var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        for (var i = 0; i < 5; i++)
            text += possible.charAt(Math.floor(Math.random() * possible.length));

        return text;
    }



    function isDate(val) {
        return moment(val, "M/D/YYYY", true).isValid();;
    }

    function getDataCell(column, gridID, columnIndex, columnDefaultStyle) {
        var htmlCode = '';

        //var elementName = column.collectionID.toLowerCase()+'_'+column.elementName;
        var elementID = 'wst' + column.elementID.toLowerCase();
        var elementName = elementID.replace(/[^a-zA-Z ]/g, '');
        //var elementName = 'wst'+column.elementID.toLowerCase();
        //var elementID = column.elementID;

        if (column.aggregation)
            //elementName = column.collectionID.toLowerCase()+'_'+column.elementName+column.aggregation;
            elementName = elementName + column.aggregation;


        var theValue = '<div style="overflow:hidden;height:100%;">{{item.' + elementName + '}}</div>';
        if (column.elementType === 'number')
            theValue = '<div style="overflow:hidden;height:100%;">{{item.' + elementName + ' | number}}</div>';

        if (column.signals) {
            var theStyle = '<style>';
            var theClass = '';
            for (var s in column.signals) {

                theStyle += ' .customStyle' + s + '_' + columnIndex + '{color:' + column.signals[s].color + ';background-color:' + column.signals[s]['background-color'] + ';font-size:' + column.signals[s]['font-size'] + ';font-weight:' + column.signals[s]['font-weight'] + ';font-style:' + column.signals[s]['font-style'] + ';}';
                var theComma = '';
                if (s > 0)
                    theComma = ' , ';

                var operator = '>'

                switch (column.signals[s].filter) {
                    case "equal":
                        operator = ' == ' + column.signals[s].value1
                        break;
                    case "diferentThan":
                        operator = ' != ' + column.signals[s].value1
                        break;
                    case "biggerThan":
                        operator = ' > ' + column.signals[s].value1
                        break;
                    case "biggerOrEqualThan":
                        operator = ' >= ' + column.signals[s].value1
                        break;
                    case "lessThan":
                        operator = ' < ' + column.signals[s].value1
                        break;
                    case "lessOrEqualThan":
                        operator = ' <= ' + column.signals[s].value1
                        break;
                    case "between":
                        operator = ' >= ' + column.signals[s].value1 + ' && {{item.' + elementName + '}} <= ' + column.signals[s].value2
                        break;
                    case "notBetween":
                        operator = ' < ' + column.signals[s].value1 + ' || {{item.' + elementName + '}}  > ' + column.signals[s].value2
                        break;
                }

                theClass += theComma + 'customStyle' + s + '_' + columnIndex + ' : {{item.' + elementName + '}} ' + operator;
            }
            htmlCode += theStyle + '</style>'

            if (column.elementType === 'number')
                theValue = '<div ng-class="{' + theClass + '}" style="overflow:hidden;height:100%;" >{{item.' + elementName + ' | number}}</div>';
            else
                theValue = '<div ng-class="{' + theClass + '}" style="overflow:hidden;height:100%;" >{{item.' + elementName + '}}</div>';

        }

        if (column.link) {
            if (column.link.type == 'report') {
                if (column.elementType === 'number')
                    theValue = '<a class="columnLink" style="overflow:hidden;height:100%;" href="/#/reports/' + column.link._id + '/' + column.link.promptElementID + '/{{item.' + elementName + '}}">{{item.' + elementName + ' | number}}</a>'
                else
                    theValue = '<a class="columnLink" style="overflow:hidden;height:100%;" href="/#/reports/' + column.link._id + '/' + column.link.promptElementID + '/{{item.' + elementName + '}}">{{item.' + elementName + '}}</a>'
            }
            if (column.link.type == 'dashboard') {
                if (column.elementType === 'number')
                    theValue = '<a class="columnLink" style="overflow:hidden;height:100%;" href="/#/dashboards/' + column.link._id + '/' + column.link.promptElementID + '/{{item.' + elementName + '}}">{{item.' + elementName + ' | number}}</a>'
                else
                    theValue = '<a class="columnLink" style="overflow:hidden;height:100%;" href="/#/dashboards/' + column.link._id + '/' + column.link.promptElementID + '/{{item.' + elementName + '}}">{{item.' + elementName + '}}</a>'
            }
        }

        var columnStyle = '';
        if (column.columnStyle) {
            columnStyle = 'color:' + column.columnStyle.color + ';';

            for (var key in column.columnStyle) {
                columnStyle += key + ':' + column.columnStyle[key] + ';';
            }
        }

        var defaultAligment = '';
        if (column.elementType === 'number')
            defaultAligment = 'text-align: right;'
        //with popover
        /* htmlCode += '<div id="ROW_'+gridID+'" class="repeater-data-column '+colClass+' popover-primary" style="'+columnDefaultStyle+columnStyle+colWidth+defaultAligment+'" popover-trigger="mouseenter" popover-placement="top" popover-title="'+column.objectLabel+'" popover="{{item.'+elementName+'}}" ng-click="cellClick(\''+hashedID+'\',item,'+'\''+elementID+'\''+','+'\''+elementName+'\''+')">'+theValue+' </div>';
        */
        //without popover
        htmlCode += '<div id="ROW_' + gridID + '" class="repeater-data-column ' + colClass + '" style="' + columnDefaultStyle + columnStyle + colWidth + defaultAligment + '" ng-click="cellClick(\'' + hashedID + '\',item,' + '\'' + elementID + '\'' + ',' + '\'' + elementName + '\'' + ')">' + theValue + ' </div>';

        return htmlCode;

    }


    function calculateForColumn(report, columnIndex, elementName) {
        var htmlCode = '';

        if (columns[columnIndex].operationSum === true) {
            htmlCode += '<div  style=""><span class="calculus-label">SUM:</span><span class="calculus-value"> ' + numeral(calculateSumForColumn(columnIndex, elementName)).format('0,0.00') + '</span> </div>';
        }

        if (columns[columnIndex].operationAvg === true) {
            htmlCode += '<div  style=""><span class="calculus-label">AVG:</span><span class="calculus-value"> ' + numeral(calculateAvgForColumn(columnIndex, elementName)).format('0,0.00') + '</span> </div>';
        }

        if (columns[columnIndex].operationCount === true) {
            htmlCode += '<div  style=""><span class="calculus-label">COUNT:</span><span class="calculus-value"> ' + numeral(calculateCountForColumn(columnIndex, elementName)).format('0,0.00') + '</span> </div>';
        }

        if (columns[columnIndex].operationMin === true) {
            htmlCode += '<div  style=""><span class="calculus-label">MIN:</span><span class="calculus-value"> ' + numeral(calculateMinimumForColumn(columnIndex, elementName)).format('0,0.00') + '</span> </div>';
        }
        if (columns[columnIndex].operationMax === true) {
            htmlCode += '<div  style=""><span class="calculus-label">MAX:</span><span class="calculus-value"> ' + numeral(calculateMaximumForColumn(columnIndex, elementName)).format('0,0.00') + '</span> </div>';
        }

        return htmlCode;

    }


    function calculateSumForColumn(columnIndex, elementName) {
        var value = 0;

        for (var row in $scope.theData[hashedID]) {
            var theRow = $scope.theData[hashedID][row];

            if (theRow[elementName])
                if (theRow[elementName] != undefined)
                    value += Number(theRow[elementName]);
        }
        return value;
    }

    function calculateCountForColumn(columnIndex, elementName) {
        var founded = 0;
        for (var row in $scope.theData[hashedID]) {
            var theRow = $scope.theData[hashedID][row];
            if (theRow[elementName])
                if (theRow[elementName] != undefined) {
                    founded += 1;
                }
        }
        return founded;
    }

    function calculateAvgForColumn(columnIndex, elementName) {
        var value = 0;
        var founded = 0;

        for (var row in $scope.theData[hashedID]) {
            var theRow = $scope.theData[hashedID][row];

            if (theRow[elementName])
                if (theRow[elementName] != undefined) {
                    founded += 1;
                    value += Number(theRow[elementName]);
                }
        }
        return value / founded;
    }

    function calculateMinimumForColumn(columnIndex, elementName) {
        var lastValue = undefined;

        for (var row in $scope.theData[hashedID]) {
            var theRow = $scope.theData[hashedID][row];

            if (theRow[elementName])
                if (theRow[elementName] != undefined) {
                    if (lastValue == undefined)
                        lastValue = theRow[elementName];

                    if (theRow[elementName] < lastValue)
                        lastValue = theRow[elementName];
                }
        }
        return lastValue;

    }

    function calculateMaximumForColumn($scope, columnIndex, elementName) {
        var lastValue = undefined;

        for (var row in $scope.theData[hashedID]) {
            var theRow = $scope.theData[hashedID][row];

            if (theRow[elementName])
                if (theRow[elementName] != undefined) {
                    if (lastValue == undefined)
                        lastValue = theRow[elementName];

                    if (theRow[elementName] > lastValue)
                        lastValue = theRow[elementName];
                }
        }
        return lastValue;
    }


    function getColumnDropDownHTMLCode(column, columnIndex, elementName, columnType, report) {
        if (column.elementType == 'date') {
            var elementID = 'wst' + column.elementID.toLowerCase();
            var elementName = elementID.replace(/[^a-zA-Z ]/g, '');
            //elementName = "'"+column.collectionID.toLowerCase()+'_'+column.elementName+'_original'+"'";
            elementName = "'" + elementName + '_original' + "'";
        }

        var columnPropertiesBtn = '<div class="btn-group pull-right" dropdown="" > ' +
            '<button type="button" class="btn btn-blue dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="margin-bottom: 0px;background-color:transparent;">' +
            ' <i class="fa fa-angle-down"></i>' +
            '</button>' +
            '<ul class="dropdown-menu dropdown-blue multi-level" role="menu">' +
            '<li class="dropdown-submenu">' +
            '      <a href="">Sort</a>' //ascendente, descendente
            +
            '      <ul class="dropdown-menu">' +
            '      <li><a ng-click="reverse = true; orderColumn(' +
            columnIndex +
            ',false,' +
            quotedHashedID() +
            ')">Ascending</a></li>' +
            '      <li><a ng-click="reverse = false; orderColumn(' +
            columnIndex +
            ',true,' +
            quotedHashedID() +
            ')">Descending</a></li>' +
            '      </ul>' +
            '</li>';

        columnPropertiesBtn += '<li class="divider"></li>'
            + '<li><a ng-click="saveToExcel(\'' + hashedID + '\',\'' + report.id + '\')"><i class="fa fa-file-excel-o"></i> Export table to excel</a></li>'
            + '<li class="divider"></li>'
            //+'<li><input class="find-input pull-right" type="search" ng-model="theFilter" placeholder="Table filter..." aria-label="Table filter..." style="margin:5px;" /></li>'
            + '</ul>'
            + '</div>';

        return columnPropertiesBtn;
    }

    this.changeBackgroundColor = function () {


    }

    this.savePropertyForGridColumn = function (grids, property, columnID, value) {
        // HEADERCOL_'+columns[i].id+'['+i+']"

        for (var g in grids) {
            for (var c in gridColumns) {
                grids[g].gridColumns[c].header[property] = value;
            }
        }
    }


}]);

app.directive('scrolly', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var raw = element[0];
            element.bind('scroll', function () {
                if (raw.scrollTop + raw.offsetHeight > raw.scrollHeight) {
                    scope.$apply(attrs.scrolly);
                }
            });
        }
    };
});
