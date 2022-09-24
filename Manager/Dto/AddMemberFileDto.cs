using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Dto
{
    public class AddMemberFileDto
    {
        public string FtpUrl { get; set; }
        public string UserName { get; set; }
        public string FptPassword { get; set; }
        public string FileName { get; set; }
        public int MemberId { get; set; }
    }
}
