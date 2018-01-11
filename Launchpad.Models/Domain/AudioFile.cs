using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Models.Domain
{
    public class AudioFile
    {
        public int Id { get; set; }
        public string UserFileName { get; set; }
        public string SystemFileName { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int UserId { get; set; }
        public string Extension { get; set; }
        public byte[] ByteArray { get; set; }
    }
}
