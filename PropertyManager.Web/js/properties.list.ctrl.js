angular.module('app').controller('PropertiesListController', function($scope, propertyService) {

	$scope.properties = propertyService.query();

	$scope.addProperty = function() {

	};

	$scope.deleteProperty = function() {

	};
 

});