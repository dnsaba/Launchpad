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
        vm.detect = _detect;
        vm.getSound = _getSound;
        vm.playSound = _playSound;

        function _onInit() {
            console.log("launchpad controller onInit");
            vm.detect();
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

        function _detect() {
            $('#keyboard').bind("keydown", function (e) {
                var sound = vm.getSound(e.keyCode);
                vm.playSound(sound);
            })
        }

        function _playSound(obj) {
            var l = obj.letter;
            var audioclip = new Audio();
            audioclip.src = obj.file;
            $("input[value="+l+"]").css("background-color", "blue");
            audioclip.play();
            //$("input[value=" + l + "]").css("background-color", "transparent");
            if (l == "Q") {
                setTimeout(function () {
                    $("input[value=" + l + "]").css("background-color", "white");
                }, 11000);
            }
            else {
                setTimeout(function () {
                    $("input[value=" + l + "]").css("background-color", "white");
                }, 2000);
            }
           // var myEl = angular.element(document.querySelector('input[value='+l+']'));
            //myEl.addClass('');
        }

        function _getSound(kc) {
            switch (kc) {
                case 65:
                    return { file: "/app/public/audio/sound2.wav", letter: "A" };
                    break;
                case 66:
                    return "";
                    break;
                case 67:
                    return "";
                    break;
                case 68:
                    return "";
                    break;
                case 69:
                    return "";
                    break;
                case 70:
                    return "";
                    break;
                case 71:
                    return "";
                    break;
                case 72:
                    return "";
                    break;
                case 73:
                    return "";
                    break;
                case 74:
                    return "";
                    break;
                case 75:
                    return "";
                    break;
                case 76:
                    return "";
                    break;
                case 77:
                    return "";
                    break;
                case 78:
                    return "";
                    break;
                case 79:
                    return "";
                    break;
                case 80:
                    return "";
                    break;
                case 81:
                    return {file: "/app/public/audio/sample1.wav", letter: "Q"};
                    break;
                case 82:
                    return "";
                    break;
                case 83:
                    return "/app/public/audio/sound1.wav";
                    break;
                case 84:
                    return "";
                    break;
                case 85:
                    return "";
                    break;
                case 86:
                    return "";
                    break;
                case 87:
                    return "/app/public/audio/sample2.wav";
                    break;
                case 88:
                    return "";
                    break;
                case 89:
                    return "";
                    break;
                case 90:
                    return "/app/public/audio/sample3.wav";
                    break;
            }
        }
    }
})();