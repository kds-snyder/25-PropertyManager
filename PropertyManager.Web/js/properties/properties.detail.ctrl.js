angular.module('app').controller('PropertiesDetailController', function($scope, $stateParams, Property, $state) {

	// If an ID was passed to state, then a property is being edited: get the property to update
	//  otherwise a property is being added: create a new property
	if ($stateParams.id) {
        $scope.property = Property.get({ id: $stateParams.id });
    } 
    else {
        $scope.property = new Property();
    }

    // Save property:
    // If an ID was passed to state, then a property is being edited: update the property
    //  otherwise a property is being added: save the property
    // After updating or saving, change state to properties.list
    $scope.saveProperty = function () {
        if ($scope.property.PropertyId) {
            $scope.property.$update(function () {
                $state.go('properties.list');
            });
        } else {
            $scope.property.$save(function () {
                $state.go('properties.list');
            });
        }
    }
  

});