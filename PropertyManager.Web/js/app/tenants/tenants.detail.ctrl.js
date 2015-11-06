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


        if($scope.tenantForm.$invalid) {
            toastr.warning('Please verify that you have filled in the fields correctly');
            return;
        }
        
        if ($scope.tenant.TenantId) {
            $scope.tenant.$update(function () {
                toastr.success($scope.tenant.FullName + ' was updated successfully');
                $state.go('app.tenants.list');
            });
        } 
        else {
            $scope.tenant.$save(function () {
                toastr.success($scope.tenant.FirstName + ' ' + $scope.tenant.LastName +
                                                                ' was saved successfully');
                $state.go('app.tenants.list');
            });
        }
    };


});