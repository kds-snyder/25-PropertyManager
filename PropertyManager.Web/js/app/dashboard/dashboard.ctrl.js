angular.module('app').controller('DashboardController', function ($scope, Dashboard) {

    $scope.dashboard = Dashboard.get();   

});

