angular.module('app').service('propertyService', function($resource) {
	return $resource('http://localhost:50672/api/properties');
})