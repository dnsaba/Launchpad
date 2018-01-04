(function () {
    "use strict";
    angular.module("publicApp")
        .factory("loginService", LoginService);

    LoginService.$inject = ["$http", "$q"];

    function LoginService($http, $q) {
        return {
            post: _post,
            check: _check,
            logout: _logout
        };

        function _post(data) {
            return $http.post("/api/login",
                data, { withCredentials: true })
                .then(success).catch(error);
        };

        function _check(data) {
            return $http.get("/api/login/check/" + data,
                { withCredentials: true })
                .then(success).catch(error);
        }

        function _logout() {
            return $http.get("/api/login/out",
                { withCredentials: true })
                .then(success).catch(error);
        }

        function success(res) {
            return res;
        }

        function error(err) {
            return $q.reject(err);
        }
    }
})();