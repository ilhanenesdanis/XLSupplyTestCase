using Manager.Dto;

namespace Manager.Service
{
    public interface IService
    {
        void Register(RegisterDto register);
        int Login(LoginDto login);
    }
}
