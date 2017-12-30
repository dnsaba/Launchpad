(function () {
    "use strict";
    angular.module("publicApp")
        .component("register", {
            templateUrl: "/app/public/modules/register/register.component.html",
            controller: "registerController"
        });
})();

(function () {
    "use strict";
    angular.module("publicApp")
        .controller("registerController", RegisterController);

    RegisterController.$inject = ["$scope", "registerService"];

    function RegisterController($scope, registerService) {
        var vm = this;
        vm.$scope = $scope;
        vm.registerService = registerService;

        vm.$onInit = _onInit;

        function _onInit() {
            console.log("register controller init");
        }
    }
})();