using System.Collections.Generic;

namespace Manager.Entity
{
    public class MemberFiles : BaseEntity
    {
        public string FtpUrl { get; set; }
        public string UserName { get; set; }
        public string FptPassword { get; set; }
        public string FileName { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public List<Products> Products { get; set; }

    }
}
