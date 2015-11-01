angular.module('app').controller('LeasesListController', function($scope, leaseService) {

 	$scope.leases = leaseService.query();

  	$scope.addLease = function() {

	};

  	$scope.deleteLease = function() {

	};
 

});