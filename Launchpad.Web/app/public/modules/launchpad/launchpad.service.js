(function () {
    "use strict";
    angular.module("publicApp")
        .factory("launchpadService", LaunchpadService);

    LaunchpadService.$inject = ["$http", "$q"];

    function LaunchpadService($http, $q) {
        return {
            post: _post,
            uploadFileChunk : _uploadFileChunk
        };

        function _post(data) {
            return $http.post("api/launchpad",
                data, { withCredentials: true })
                .then(success).catch(error);
        };

        function _uploadFileChunk(chunk, fileName) {
            var fd = new FormData();
            fd.append("file", chunk, fileName);
            return $http.post("/api/launchpad/chunk",
                fd,
                {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                },
                { "withCredentials": true }
            ).then(success)
                .catch(error);
        }

        function success(res) {
            return res;
        }

        function error(err) {
            return $q.reject(err);
        }
    }
})();