angular.module('app').controller('TenantsListController', function($scope, tenantService) {

	$scope.tenants = tenantService.query();

	$scope.addTenant = function() {

	};

  	$scope.deleteTenant = function() {

	};  

});