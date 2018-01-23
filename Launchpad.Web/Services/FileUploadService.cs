using Launchpad.Extensions;
using Launchpad.Models.Domain;
using Launchpad.Models.Responses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Launchpad.Services
{
    public class FileUploadService
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly string _uploadPath;
        private readonly MultipartFormDataStreamProvider _streamProvider;

        public FileUploadService()
        {
            _uploadPath = UserLocalPath;
            _streamProvider = new MultipartFormDataStreamProvider(_uploadPath);
        }

        public async Task<UploadProcessingResult> HandleRequest(HttpRequestMessage request)
        {
            await request.Content.ReadAsMultipartAsync(_streamProvider);
            return await ProcessFile(request);
        }

        private async Task<UploadProcessingResult> ProcessFile(HttpRequestMessage request)
        {
            if (request.IsChunkUpload())
            {
                return await ProcessChunk(request);
            }

            return new UploadProcessingResult()
            {
                IsComplete = true,
                FileName = OriginalFileName,
                LocalFilePath = LocalFileName,
                FileMetaData = _streamProvider.FormData
            };
        }

        private async Task<UploadProcessingResult> ProcessChunk(HttpRequestMessage request)
        {
            FileChunkMetaData chunkMetaData = request.GetChunkMetaData();
            string filePath = Path.Combine(_uploadPath, string.Format("{0}.temp", chunkMetaData.ChunkIdentifier));

            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate | FileMode.Append))
            {
                var localFileInfo = new FileInfo(LocalFileName);
                var localFileStream = localFileInfo.OpenRead();

                await localFileStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

                fileStream.Close();
                localFileStream.Close();

                //delete chunk
                localFileInfo.Delete();
            }

            return new UploadProcessingResult()
            {
                IsComplete = chunkMetaData.IsLastChunk,
                FileName = OriginalFileName,
                LocalFilePath = chunkMetaData.IsLastChunk ? filePath : null,
                FileMetaData = _streamProvider.FormData
            };
        }

        private string LocalFileName
        {
            get
            {
                MultipartFileData fileData = _streamProvider.FileData.FirstOrDefault();
                return fileData.LocalFileName;
            }
        }

        private string OriginalFileName
        {
            get
            {
                MultipartFileData fileData = _streamProvider.FileData.FirstOrDefault();
                return fileData.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            }
        }

        private string UserLocalPath
        {
            get
            {
                return "C:/repos/github/Launchpad/Launchpad.Web/app/public/audio";                   
            }
        }

        public int Insert(AudioFile model)
        {
            int id = 0;

            //model.ModifiedBy = 1;
            string systemFileName = string.Empty;
            systemFileName = string.Format("{0}_{1}{2}",
                model.UserFileName,
                Guid.NewGuid().ToString(),
                model.Extension);

            SaveBytesFile(model.Location, systemFileName, model.ByteArray);

            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("AudioFiles_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserFileName", model.UserFileName);
                    cmd.Parameters.AddWithValue("@SystemFileName", model.SystemFileName);
                    cmd.Parameters.AddWithValue("@Location", model.Location);
                    cmd.Parameters.AddWithValue("@ModifiedBy", model.ModifiedBy);
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);

                    SqlParameter parm = new SqlParameter("@Id", SqlDbType.Int);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);

                    cmd.ExecuteNonQuery();

                    id = (int)cmd.Parameters["@Id"].Value;
                }
                conn.Close();
            }

            return id;
        }

        private void SaveBytesFile(string location, string fileName, byte[] bytes)
        {
            string fileBase = "~/UserUploads";
            string path = "C:/repos/github/Launchpad/Launchpad.Web/app/public/audio" + fileName;
            var filePath = HttpContext.Current.Server.MapPath("~/" + location + "/" + fileName);
            File.WriteAllBytes(path, bytes);
        }
    }
}