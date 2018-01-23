using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Models.Domain
{
    public class UploadProcessingResult
    {
        public bool IsComplete { get; set; }
        public string FileName { get; set; }
        public string LocalFilePath { get; set; }
        public NameValueCollection FileMetaData { get; set; }
    }
}
