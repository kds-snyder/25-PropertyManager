angular.module('app', ['ui.router', 'ngResource', 'LocalStorageModule'])
				.config(function($httpProvider, $stateProvider, $urlRouterProvider) {

	$httpProvider.interceptors.push('authInterceptorService');

	$urlRouterProvider.otherwise('/login');

	$stateProvider
		.state('login', { url:'/login',templateUrl: '/templates/authentication/login.html',
			controller: 'LoginController'})
		.state('register', { url:'/register', templateUrl: '/templates/authentication/register.html',
			controller: 'RegisterController'})

		.state('app',{ url:'/app', templateUrl: '/templates/app/app.html',
						controller: 'AppController'})			
			.state('app.properties', { abstract: true, url: '/properties', 
					template: '<ui-view/>', authenticate: true})
				.state('app.properties.list',{ url:'/list', 
						templateUrl: '/templates/app/properties/properties.list.html',
						controller: 'PropertiesListController', authenticate: true})
				.state('app.properties.detail',{ url:'/detail/:id',
						templateUrl: '/templates/app/properties/properties.detail.html',
						controller: 'PropertiesDetailController', authenticate: true})				
			.state('app.tenants', { abstract: true, url: '/tenants', 
					template: '<ui-view/>', authenticate: true})
				.state('app.tenants.list', { url: '/list',
			 			templateUrl: '/templates/app/tenants/tenants.list.html', 
			 			controller: 'TenantsListController', authenticate: true})
				.state('app.tenants.detail', { url: '/detail/:id',
			 			templateUrl: '/templates/app/tenants/tenants.detail.html', 
			 			controller: 'TenantsDetailController', authenticate: true})
			.state('app.leases', { abstract: true, url: '/leases', 
					template: '<ui-view/>', authenticate: true})						
				.state('app.leases.list', { url: '/list', 
						templateUrl: '/templates/app/leases/leases.list.html',
						controller: 'LeasesListController', authenticate: true})
				.state('app.leases.detail', { url: '/detail/:id', 
						templateUrl: '/templates/app/leases/leases.detail.html',
						controller: 'LeasesDetailController', authenticate: true});
});

angular.module('app').value('apiUrl', 'http://localhost:5000/');

angular.module('app').run(function ($rootScope, authService, $state) {
    authService.fillAuthData();

    $rootScope.$on("$stateChangeStart", function (event, toState, toParams, fromState, fromParams) {
        if (toState.authenticate && !authService.authentication.isAuth) {
            $state.go('login');
            event.preventDefault();
        }
    });
});

