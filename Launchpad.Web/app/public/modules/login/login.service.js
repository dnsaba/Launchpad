(function () {
    "use strict";
    angular.module("publicApp")
        .factory("loginService", LoginService);

    LoginService.$inject = ["$http", "$q"];

    function LoginService($http, $q) {
        return {
            post: _post
        };

        function _post(data) {
            return $http.post("/api/login",
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