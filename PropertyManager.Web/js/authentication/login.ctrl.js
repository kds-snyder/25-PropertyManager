angular.module('app').controller('LoginController', function ($scope, $location, $state, authService) {
 
    $scope.loginData = {
        userName: "",
        password: ""
    };
 
    $scope.message = "";
 
    $scope.login = function () {
 
        authService.login($scope.loginData).then(function (response) {
 
            $state.go('app.properties.list');
 
        },
         function (err) {

             //$scope.message = err.error_description;
             if (err) {
                alert(err.error_description);
             }
         });
    };
 
});