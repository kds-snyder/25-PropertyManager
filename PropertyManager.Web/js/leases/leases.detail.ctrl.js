angular.module('app').controller('LeasesDetailController', function($scope, $stateParams, Lease, $state) {

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
        $scope.lease.LeaseType = 2;
        if ($scope.lease.LeaseId) {
            $scope.lease.$update(function () {
                $state.go('leases.list');
            });
        } else {
            $scope.lease.$save(function () {
                $state.go('leases.list');
            });
        }
    }

});