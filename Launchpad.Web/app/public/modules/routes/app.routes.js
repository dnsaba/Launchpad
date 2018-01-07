(function () {
    "use strict";
    var myApp = angular.module("publicApp" + '.routes', []);
    myApp.config(function ($stateProvider, $locationProvider, $urlRouterProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false,
        });
        $urlRouterProvider.otherwise('/home');

        $stateProvider

            .state('home', {
                name: 'home',
                url: '/home',
                templateUrl: '/app/public/modules/home/home.html',
                controller: "homeController as homeCtrl"
            })

            .state({
                name: 'register-user',
                url: '/register-user',
                templateUrl: '/app/public/modules/register/register-user.html'
            })

            .state({
                name: 'login',
                url: '/login',
                templateUrl: '/app/public/modules/login/login.html'
            })

            .state({
                name: 'launchpad',
                url: '/launchpad',
                templateUrl: '/app/public/modules/launchpad/launchpad.html'
            });
    });
})();


