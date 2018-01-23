using Launchpad.Models.Domain;
using Launchpad.Models.Responses;
using Launchpad.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Launchpad.Controllers.Api
{
    [RoutePrefix("api/register")]
    public class NewUserController : ApiController
    {
        UserService _userService = new UserService();
        FileUploadService _filesvc = new FileUploadService();

        [Route, HttpPost, AllowAnonymous]
        public HttpResponseMessage Post(NewUser model)
        {
            try
            {
                string lowerEmail = model.Email.ToLower();
                model.Email = lowerEmail;
                ItemResponse<int> resp = new ItemResponse<int>();
                int id = _userService.Create(model);
                resp.Item = id;

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}