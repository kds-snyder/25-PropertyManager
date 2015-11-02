angular.module('app', ['ui.router', 'ngResource']).config(function($stateProvider, $urlRouterProvider) {

	$urlRouterProvider.otherwise('/properties/list');
	$stateProvider
		.state('properties', { 
			abstract: true,
			url: '/properties', 			
			template: '<ui-view/>'})
		.state('properties.list',{
			url:'/list',
			templateUrl: '/templates/properties/properties.list.html',
			controller: 'PropertiesListController'})
		.state('properties.detail',{
			url:'/detail/:id',
			templateUrl: '/templates/properties/properties.detail.html',
			controller: 'PropertiesDetailController'})				
		.state('tenants', { 
			abstract: true,
			url: '/tenants',
			template: '<ui-view/>'})
		.state('tenants.list', { 
			url: '/list',
			 templateUrl: '/templates/tenants/tenants.list.html', 
			 controller: 'TenantsListController'})
		.state('tenants.detail', { 
			url: '/detail/:id',
			 templateUrl: '/templates/tenants/tenants.detail.html', 
			 controller: 'TenantsDetailController'})
		.state('leases', { 
			abstract: true,
			url: '/leases', 
			template: '<ui-view/>'})						
		.state('leases.list', { 
			url: '/list', 
			templateUrl: '/templates/leases/leases.list.html',
			controller: 'LeasesListController'})
		.state('leases.detail', { 
			url: '/detail/:id', 
			templateUrl: '/templates/leases/leases.detail.html',
			controller: 'LeasesDetailController'});
});

angular.module('app').value('apiUrl', 'http://localhost:50672/api/');

