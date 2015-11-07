angular.module('app').controller('LeasesDetailController', function($scope, $stateParams, Lease, Property, Tenant, $state) {

    $scope.tenants = Tenant.query();
    $scope.properties = Property.query();
    
  	// If an ID was passed to state, then a lease is being edited: get the lease to update
	//  otherwise a lease is being added: create a new lease
	if ($stateParams.id) {
        $scope.lease = Lease.get({ id: $stateParams.id });
    } 
    else {
        $scope.lease = new Lease();
    }

    // Save lease:
    // If an ID was passed to state, then a lease is being edited: update the lease
    //  otherwise a lease is being added: save the lease
    // After updating or saving, change state to leases.list
    $scope.saveLease = function () {

        if($scope.leaseForm.$invalid) {
            toastr.warning('Please verify that you have filled in the fields correctly');
            return;
        }

        var successCallback = function() {
            $state.go('app.leases.list');
        };

        if ($scope.lease.LeaseId) {
            $scope.lease.$update(function () {
                toastr.success('The lease was updated successfully');
                $state.go('app.leases.list');
            });
        }
        else {
            $scope.lease.$save(function () {
                toastr.success('The lease was added successfully');
                $state.go('app.leases.list');
            });
        } 
        
    };

});