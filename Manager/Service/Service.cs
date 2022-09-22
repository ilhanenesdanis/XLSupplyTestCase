using Manager.Dto;
using Manager.Entity;
using Manager.Helpers;
using System;
using System.Linq;

namespace Manager.Service
{
    public class Service : IService
    {
        private readonly Context _context;

        public Service(Context context)
        {
            _context = context;
        }

        public int Login(LoginDto login)
        {
            var member = _context.Members.Where(x => x.Email == login.Email).FirstOrDefault();
            if (member != null)
            {
                if (!HashHelper.VerigyPasswordHash(login.Password, member.PasswordHash, member.PasswordSalt))
                {
                    return 0;
                }
                return member.Id;
            }
            return 0;
        }

        public void Register(RegisterDto register)
        {
            byte[] passwordHash, passwordSalt;
            HashHelper.CreatePasswordHash(register.Password,out passwordHash, out passwordSalt);
            Member member = new Member()
            {
                CompanyName = register.CompanyName,
                Email = register.Email,
                Name = register.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Surname = register.Surname,
            };
            _context.Members.Add(member);
            _context.SaveChanges();
        }
    }
}
