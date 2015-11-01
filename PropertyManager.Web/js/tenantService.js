angular.module('app').service('tenantService', function($resource) {
	return $resource('http://localhost:50672/api/tenants');
})