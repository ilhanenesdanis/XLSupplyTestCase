using System;
using System.Collections.Generic;

namespace Manager.Entity
{
    public class Member:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public List<MemberFiles> MemberFiles { get; set; }
    }
}
