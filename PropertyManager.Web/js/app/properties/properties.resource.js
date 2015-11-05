angular.module('app').factory('Property', function($resource, apiUrl) {
	return $resource(apiUrl + 'api/properties/:id', { id: '@PropertyId' }, {
		update: {
			method: 'PUT'
		}
	});
});