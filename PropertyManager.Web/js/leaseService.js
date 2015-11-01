angular.module('app').service('leaseService', function($resource) {
	return $resource('http://localhost:50672/api/leases');
})