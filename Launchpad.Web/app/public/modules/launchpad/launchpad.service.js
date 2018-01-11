﻿(function () {
    "use strict";
    angular.module("publicApp")
        .factory("launchpadService", LaunchpadService);

    LaunchpadService.$inject = ["$http", "$q"];

    function LaunchpadService($http, $q) {
        return {
            post: _post,
            uploadAudio: _uploadAudio
        };

        function _post(data) {
            return $http.post("/api/launchpad",
                data, { withCredentials: true })
                .then(success).catch(error);
        };

        function _uploadAudio(data) {
            return $http.post("api/launchpad/audio",
                data, { withCredentials: true })
                .then(success).catch(error);
        };

        function success(res) {
            return res;
        }

        function error(err) {
            return $q.reject(err);
        }
    }
})();