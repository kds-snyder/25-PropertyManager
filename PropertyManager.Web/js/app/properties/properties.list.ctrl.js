angular.module('app').controller('PropertiesListController', function($scope, Property) {

	$scope.properties = Property.query();	

	$scope.deleteProperty = function (property) {
        if (confirm('Are you sure you want to delete this property?')) {
            Property.delete({ id: property.PropertyId }, function (data) {
                var index = $scope.properties.indexOf(property);
                $scope.properties.splice(index, 1);
            });
        }
    }

});