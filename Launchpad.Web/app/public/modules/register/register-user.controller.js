(function () {
    "use strict";
    angular.module("publicApp")
        .component("registerUser", {
            templateUrl: "/app/public/modules/register/register-user-comp.html",
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
        vm.item = {};

        vm.$onInit = _onInit;
        vm.submitForm = _submitForm;
        vm.submitFormSuccess = _submitFormSuccess;
        vm.submitFormError = _submitFormError;

        function _onInit() {
            console.log("register controller init");
        }

        function _submitForm() {
            console.log(vm.item);
            vm.registerService.post(vm.item)
                .then(vm.submitFormSuccess).catch(vm.submitFormError);
        }

        function _submitFormSuccess(res) {
            console.log(res);
        }

        function _submitFormError(err) {
            console.log(err);
        }
    }
})();