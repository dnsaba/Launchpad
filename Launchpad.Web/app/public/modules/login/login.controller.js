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
        vm.checkInput;

        vm.$onInit = _onInit;
        vm.login = _login;
        vm.loginSuccess = _loginSuccess;
        vm.loginError = _loginError;
        vm.check = _check;
        vm.checkSuccess = _checkSuccess;
        vm.checkError = _checkError;
        vm.logout = _logout;
        vm.logoutSuccess = _logoutSuccess;
        vm.logoutError = _logoutError;

        function _onInit() {
            console.log("login controller onInit");
        }

        function _login() {
            console.log(vm.item);
            vm.loginService.post(vm.item)
                .then(vm.loginSuccess).catch(vm.loginError);
        }

        function _loginSuccess(res) {
            console.log(res);
        }

        function _loginError(err) {
            console.log(err);
        }

        function _check() {
            console.log(vm.checkInput);
            vm.loginService.check(vm.checkInput)
                .then(vm.checkSuccess).catch(vm.checkError);
        }

        function _checkSuccess(res) {
            console.log(res);
        }

        function _checkError(err) {
            console.log(err);
        }

        function _logout() {
            vm.loginService.logout()
                .then(vm.logoutSuccess).catch(vm.logoutError);
        }

        function _logoutSuccess(res) {
            console.log(res);
        }

        function _logoutError(err) {
            console.log(err);
        }
    }
})();