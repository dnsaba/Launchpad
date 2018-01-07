(function () {
    "use strict";
    angular.module("publicApp")
        .component("launchpad", {
            templateUrl: "/app/public/modules/launchpad/launchpad-comp.html",
            controller: "launchpadController"
        });
})();

(function () {
    "use strict";
    angular.module("publicApp")
        .controller("launchpadController", LaunchpadController);

    LaunchpadController.$inject = ["$scope", "launchpadService"];

    function LaunchpadController($scope, launchpadService) {
        var vm = this;
        vm.$scope = $scope;
        vm.launchpadService = launchpadService;
        vm.item = {};

        vm.$onInit = _onInit;

        function _onInit() {
            console.log("launchpad controller onInit");
        }
    }
})();