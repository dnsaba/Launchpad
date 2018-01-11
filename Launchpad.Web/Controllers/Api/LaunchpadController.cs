using Launchpad.Models.Domain;
using Launchpad.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Launchpad.Controllers.Api
{
    [RoutePrefix("api/launchpad")]
    public class LaunchpadController : ApiController
    {
        string serverFileName = string.Empty;

        [Route("audio"), HttpPost]
        public HttpResponseMessage FileUpload(AudioFile model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            
            try
            {
                byte[] fileData;
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                using (var binaryReader = new BinaryReader(postedFile.InputStream))
                {
                    fileData = binaryReader.ReadBytes(postedFile.ContentLength);
                }

                model.ByteArray = fileData;
                model.Location = "Audio";
                model.ModifiedBy = 1;
                model.UserId = 1;
                model.UserFileName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                model.Extension = Path.GetExtension(postedFile.FileName);

                serverFileName = string.Format("{0}_{1}{2}",
                    model.UserFileName,
                    Guid.NewGuid().ToString(),
                    model.Extension);
                model.SystemFileName = serverFileName;

                int id = _fileUploadService.Insert(model);
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = id;

                return Request.CreateResponse(HttpStatusCode.OK, resp);
                
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
