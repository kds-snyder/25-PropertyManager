angular.module('app').controller('AppController', function ($scope, authService) {
    $scope.user = authService.authentication;

    $scope.logout = function () {
        authService.logOut();
    };
});