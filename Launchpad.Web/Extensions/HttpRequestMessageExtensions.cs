using Launchpad.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Launchpad.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static bool IsChunkUpload(this HttpRequestMessage request)
        {
            return request.Content.Headers.ContentRange != null;
        }

        public static FileChunkMetaData GetChunkMetaData(this HttpRequestMessage request) 
        {
            return new FileChunkMetaData()
            {
                ChunkIdentifier = request.Headers.Contains("X-DS-Identifier") ? request.Headers.GetValues("X-File-Identifier").FirstOrDefault() : null,
                ChunkStart = request.Content.Headers.ContentRange.From,
                ChunkEnd = request.Content.Headers.ContentRange.To,
                TotalLength = request.Content.Headers.ContentRange.Length
            };
        }
    }
}