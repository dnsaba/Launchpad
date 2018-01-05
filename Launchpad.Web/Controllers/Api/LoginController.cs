using Launchpad.Models.Domain;
using Launchpad.Models.Responses;
using Launchpad.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Launchpad.Controllers.Api
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        UserService _userService = new UserService();
        [Route, HttpPost, AllowAnonymous]
        public HttpResponseMessage Post(LoginRequest model)
        {
            try
            {
                ItemResponse<bool> res = new ItemResponse<bool>();

                string email = model.Email.ToLower();
                model.Email = email;
                bool loggedIn = _userService.LogIn(model);
                res.Item = loggedIn;

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("check/{stuff}"), HttpGet]
        public HttpResponseMessage Check(string stuff)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            

            try
            {
                ItemResponse<int> res = new ItemResponse<int>();
                res.Item = 1;
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("out"), HttpGet, AllowAnonymous]
        public HttpResponseMessage Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();

            return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
        }
    }
}
