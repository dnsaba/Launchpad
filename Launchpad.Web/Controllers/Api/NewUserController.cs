using Launchpad.Models.Domain;
using Launchpad.Models.Responses;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Launchpad.Controllers.Api
{
    [System.Web.Http.RoutePrefix("api/register")]
    public class NewUserController : ApiController
    {
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