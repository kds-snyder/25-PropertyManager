angular.module('app').controller('RegisterController', 
    function ($scope, $location, $timeout, authService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";
 
    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: ""
    };
 
    $scope.register = function () {
 
        authService.saveRegistration($scope.registration).then(function (response) {
 
            $scope.savedSuccessfully = true;
            alert('User has been registered successfully, you will be redicted to login page in 2 seconds.')
            startTimer();
 
        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             alert('Failed to register user due to:' + errors.join(' '));
         });
    };

        var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $state.go('login');           
        }, 2000);
    }
 
});