/* Controllers */
function WebAppCtrl($scope, $http, $window, $location) {
    
}

WebAppCtrl.$inject = ["$scope", "$http", "$window", "$location"];

app.controller('PublicCtrl', ["$scope", "$http", "$rootScope", "$sessionStorage", "connection", "$location",
    function ($scope, $http, $rootScope, $sessionStorage, connection, $location) {

        init();

        //Functions 
        function login() {
            //var user = { "userName": $scope.userName, "password": $scope.password, "remember_me": $scope.rememberMe, "companyID": $('#companyID').attr('value') };

            $http({ method: 'GET', url: '/Report/WidestageLogin' }).
                success(function (data, status, headers, config) {

                    $scope.loginError = false;

                    var theUser = JSON.parse(data.Content).user;
                    var localapiparams = {a:"a"}

                    $sessionStorage.setObject('localapiparams', localapiparams);

                    connection.get('/Report/GetUserData',
                        {}, function (data) {
                            theUser.companyData = data.items.companyData;
                            theUser.rolesData = data.items.rolesData;
                            theUser.reportsCreate = data.items.reportsCreate;
                            theUser.dashboardsCreate = data.items.dashboardsCreate;
                            theUser.pagesCreate = data.items.pagesCreate;
                            theUser.exploreData = data.items.exploreData;
                            theUser.isWSTADMIN = data.items.isWSTADMIN;
                            theUser.contextHelp = data.items.contextHelp;
                            theUser.dialogs = data.items.dialogs;
                            theUser.viewSQL = data.items.viewSQL;
                            $rootScope.user = theUser;
                            $sessionStorage.setObject('user', theUser);
                            //if ($location.path().split('/')[2] == "reports") {
                            //    $location.path("/reports/");
                            //} else {
                            //    $location.path("/dashboardsv2/" + $location.path().split('/')[2]);
                            //}
                            $location.path($sessionStorage.getObject('afterloginpath'));

                        }, undefined, localapiparams);

                }).
                error(function (data, status, headers, config) {
                    $scope.errorLoginMessage = data;
                    $scope.loginError = true;
                });
        };

        function init() {
            login();
        }
    }]);

//function VerificationCtrl($scope, $http, $window, $stateParams, vcRecaptchaService) {
//    $scope.verify = function () {
//        var valid = false;
//        var recaptcha = vcRecaptchaService.data();

//        var postData = {
//            hash: $stateParams.hash,
//            email: $stateParams.email,
//            challenge: recaptcha.challenge,
//            response: recaptcha.response
//        };

//        $http({ method: 'POST', url: '/api/verify', data: postData }).
//            success(function (data, status, headers, config) {
//                if (data.result === 0) {
//                    noty({ text: data.msg, timeout: 2000, type: 'error' });
//                    vcRecaptchaService.reload();
//                }
//                else
//                    $window.location.href = "/home";
//            }).
//            error(function (data, status, headers, config) {
//                //console.log(data);
//            });
//    };
//}
//VerificationCtrl.$inject = ["$scope", "$http", "$window", "$stateParams", 'vcRecaptchaService'];

//function ChangePwdCtrl($scope, $http, $window, $stateParams) {
//    $scope.changePassword = function () {
//        var data = { "hash": $stateParams.hash, "password": $scope.password };

//        if ($scope.password !== undefined || $scope.confirmation !== undefined) {

//            if ($scope.password !== $scope.confirmation) {
//                noty({ text: 'Password and Check Password must be the same!', timeout: 2000, type: 'error' });
//            } else {
//                $http({ method: 'POST', url: '/api/change-password', data: data }).
//                    success(function (data, status, headers, config) {
//                        noty({ text: data.msg, timeout: 2000, type: 'success' });

//                        window.location.hash = '/login';
//                    }).
//                    error(function (data, status, headers, config) {
//                        noty({ text: data.msg, timeout: 2000, type: 'error' });
//                    });
//            }
//        }
//    };
//}
//ChangePwdCtrl.$inject = ["$scope", "$http", "$window", "$stateParams"];