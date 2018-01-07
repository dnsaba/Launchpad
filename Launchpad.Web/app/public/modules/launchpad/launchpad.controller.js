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
        vm.testclick = _testclick;
        vm.testclick2 = _testclick2;

        function _onInit() {
            console.log("launchpad controller onInit");
        }

        function _testclick() {
            var audioclip = new Audio();
            audioclip.src = '/app/public/audio/lp_70_fm_guitar_skank_delay.wav';
            audioclip.play();
        }

        function _testclick2() {
            var audioclip = new Audio();
            audioclip.src = '/app/public/audio/lp_110_am_c_guitar_chord_delay.wav';
            audioclip.play();
        }
    }
})();