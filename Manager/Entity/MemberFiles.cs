using System;
using System.Collections.Generic;

namespace Manager.Entity
{
    public class MemberFiles : BaseEntity
    {
        public string FilePath { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public List<Products> Products { get; set; }

    }
}
