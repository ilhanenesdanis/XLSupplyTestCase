using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Dto
{
    public class MemberFileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FtpUrl { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
