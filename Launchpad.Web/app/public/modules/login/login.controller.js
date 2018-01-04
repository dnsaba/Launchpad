(function () {
    "use strict";
    angular.module("publicApp")
        .component("login", {
            templateUrl: "/app/public/modules/login/login-comp.html",
            controller: "loginController"
        });
})();

(function () {
    "use strict";
    angular.module("publicApp")
        .controller("loginController", LoginController);

    LoginController.$inject = ["$scope", "loginService"];

    function LoginController($scope, loginService) {
        var vm = this;
        vm.$scope = $scope;
        vm.loginService = loginService;
        vm.item = {};

        vm.$onInit = _onInit;
        vm.login = _login;

        function _onInit() {
            console.log("login controller onInit");
        }

        function _login() {
            console.log(vm.item);
        }
    }
})();