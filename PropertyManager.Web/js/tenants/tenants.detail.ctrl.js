angular.module('app').controller('TenantsDetailController', function($scope, $stateParams, Tenant, $state) {

   	// If an ID was passed to state, then a tenant is being edited: get the tenant to update
	//  otherwise a tenant is being added: create a new tenant
	if ($stateParams.id) {
        $scope.tenant = Tenant.get({ id: $stateParams.id });
    } 
    else {
        $scope.tenant = new Tenant();
    }

    // Save tenant:
    // If an ID was passed to state, then a tenant is being edited: update the tenant
    //  otherwise a tenant is being added: save the tenant
    // After updating or saving, change state to tenants.list
    $scope.saveTenant = function () {
        if ($scope.tenant.TenantId) {
            $scope.tenant.$update(function () {
                $state.go('tenants.list');
            });
        } else {
            $scope.tenant.$save(function () {
                $state.go('tenants.list');
            });
        }
    }

});