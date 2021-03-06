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

        
        if($scope.propertyForm.$invalid) {
            toastr.warning('Please verify that you have filled in the fields correctly');
            return;
        }
       
        if ($scope.property.PropertyId) {
            action = 'updated';
            $scope.property.$update(function () {
                toastr.success($scope.property.Name + ' was updated successfully');
                $state.go('app.properties.list');
            });
        } 
        else {
            action = 'saved';
            $scope.property.$save(function () {
                toastr.success($scope.property.Name + ' was added successfully');
                $state.go('app.properties.list');
            });
        }
    };
  

});