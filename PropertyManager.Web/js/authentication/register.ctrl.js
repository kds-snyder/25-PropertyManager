angular.module('app').controller('RegisterController', 
    function ($scope, $location, $timeout, $state, authService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";
 
    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: ""
    };
 
    $scope.register = function () {
        if($scope.registrationForm.$invalid) {
            toastr.warning('Please verify that you have filled in the fields correctly');
            return;
        }
        authService.saveRegistration($scope.registration).then(function (response) {
 
            $scope.savedSuccessfully = true;
            toastr.success('You are now registered as a user, and you can now log in');            
            $state.go('login');
 
        },
         function (error) {            

            var errors = [];
             for (var key in error.data.ModelState) {
                 for (var i = 0; i < error.data.ModelState[key].length; i++) {
                     errors.push(error.data.ModelState[key][i]);
                 }
             }
             
             toastr.error('Unable to register you as a user: ' + errors.join(' ')); 
             $state.go('register');           
         });
    };       
        
});