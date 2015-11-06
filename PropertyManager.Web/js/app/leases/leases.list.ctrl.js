angular.module('app').controller('LeasesListController', function($scope, Lease, Property, Tenant) {

 	$scope.leases = Lease.query();

  	$scope.deleteLease = function (lease) {
        if (confirm('Are you sure you want to delete this lease?')) {
            Lease.delete({ id: lease.LeaseId }, function (data) {
                var index = $scope.leases.indexOf(lease);
                $scope.leases.splice(index, 1);
                toastr.success('The lease was deleted successfully');
            });
        }
    }
 

});