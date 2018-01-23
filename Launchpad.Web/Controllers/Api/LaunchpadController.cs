using Launchpad.Models.Domain;
using Launchpad.Models.Responses;
using Launchpad.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Launchpad.Controllers.Api
{
    [RoutePrefix("api/launchpad")]
    public class LaunchpadController : ApiController
    {
        FileUploadService _fileUploadService = new FileUploadService();

        [Route, HttpPost, AllowAnonymous]
        public HttpResponseMessage FileUpload(List<EncodedImage> list)
        {
            try
            {
                byte[] newBytes = Convert.FromBase64String(list[0].EncodedImageFile);
                AudioFile model = new AudioFile();
                model.UserFileName = "userAudio";
                model.ByteArray = newBytes;
                model.Extension = list[0].FileExtension;
                model.Location = "audio";
                model.UserId = 1;
                model.ModifiedBy = 1;

                int id = _fileUploadService.Insert(model);
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = id;

                return Request.CreateResponse(HttpStatusCode.OK, resp);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("chunk"), HttpPost]
        public async Task<IHttpActionResult> UploadDocument()
        {
            
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
            UploadProcessingResult uploadResult = await _fileUploadService.HandleRequest(Request);
            if (uploadResult.IsComplete)
            {
                // do other stuff here after file upload complete    
                return Ok();
            }

            return Ok(HttpStatusCode.Continue);

        }

    }
}

