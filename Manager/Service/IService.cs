using Manager.Dto;
using System.Collections.Generic;

namespace Manager.Service
{
    public interface IService
    {
        void Register(RegisterDto register);
        int Login(LoginDto login);
        List<MemberFileDto> GetMemberFile(int memberId);
        void AddMemberFile(AddMemberFileDto memberFile);
        string DownloadFile(AddMemberFileDto memberFileDto);
        List<CsvProductDto> ReadFile(string filePath);
        string DeleteFile(string filePath);
        void AddProduct(List<CsvProductDto> csvProduct, int fileId);
        List<ProductListDto> GetAllProductsList(int fileId);
    }
}
