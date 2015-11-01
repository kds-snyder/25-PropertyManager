angular.module('app', ['ui.router', 'ngResource']).config(function($stateProvider, $urlRouterProvider) {

	$urlRouterProvider.otherwise('/properties');
	$stateProvider
		.state('properties', { 
			abstract: true,
			url: '/properties', 			
			template: '<ui-view/>'})
		.state('properties.list',{
			url:'/list',
			templateUrl: '/templates/properties.list.html',
			controller: 'PropertiesListController'})
		.state('properties.detail',{
			url:'/detail',
			templateUrl: '/templates/properties.detail.html',
			controller: 'PropertiesDetailController'})				
		.state('tenants', { 
			abstract: true,
			url: '/tenants',
			template: '<ui-view/>'})
		.state('tenants.list', { 
			url: '/list',
			 templateUrl: '/templates/tenants.list.html', 
			 controller: 'TenantsListController'})
		.state('tenants.detail', { 
			url: '/detail',
			 templateUrl: '/templates/tenants.detail.html', 
			 controller: 'TenantsDetailController'})
		.state('leases', { 
			abstract: true,
			url: '/leases', 
			template: '<ui-view/>'})						
		.state('leases.list', { 
			url: '/list', 
			templateUrl: '/templates/leases.list.html',
			controller: 'LeasesListController'})
		.state('leases.detail', { 
			url: '/detail', 
			templateUrl: '/templates/leases.detail.html',
			controller: 'LeasesDetailController'});
});
