angular.module('app').controller('TenantsListController', function($scope, Tenant) {

	$scope.tenants = Tenant.query();

	$scope.deleteTenant = function (tenant) {
        if (confirm('Are you sure you want to delete this tenant?')) {
            Tenant.delete({ id: tenant.TenantId }, function (data) {
            	//debugger;
                var index = $scope.tenants.indexOf(tenant);
                $scope.tenants.splice(index, 1);
                //toastr.success($scope.tenant.FirstName + ' ' + $scope.tenant.LastName +
                toastr.success('The tenant ' +
                                                                ' was deleted successfully');
            });
        }
    }

});