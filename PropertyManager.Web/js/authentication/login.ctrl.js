angular.module('app').controller('LoginController', function ($scope, $location, $state, authService) {
 
    $scope.loginData = {
        userName: "",
        password: ""
    };
 
    //$scope.message = "";
 
    $scope.login = function () {

        if($scope.loginForm.$invalid) {
            toastr.warning('Please verify that you have filled in the fields correctly');
            return;
        }
 
        authService.login($scope.loginData).then(
            function (response) {
 
                $state.go('app.dashboard');
 
            },
            function (err) {               

                //$scope.message = err.error_description;
                
                if (err) {
                   toastr.error(err.error_description);
                }
                else {
                    toastr.error('Unable to log in due to server error');
                }
            }
        );
    };
 
});