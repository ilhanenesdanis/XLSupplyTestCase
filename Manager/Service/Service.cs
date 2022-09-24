using CsvHelper;
using Manager.Dto;
using Manager.Entity;
using Manager.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace Manager.Service
{
    public class Service : IService
    {
        private readonly Context _context;

        public Service(Context context)
        {
            _context = context;
        }

        public void AddMemberFile(AddMemberFileDto memberFile)
        {
            MemberFiles member = new MemberFiles()
            {
                FileName = memberFile.FileName,
                FptPassword = memberFile.FptPassword,
                FtpUrl = memberFile.FtpUrl,
                MemberId = memberFile.MemberId,
                UserName = memberFile.UserName
            };
            _context.MemberFiles.Add(member);
            _context.SaveChanges();
            var filePath = DownloadFile(memberFile);
            var productList = ReadFile(filePath);
            var FileDelete = DeleteFile(filePath);
            AddProduct(productList, member.Id);


        }

        public void AddProduct(List<CsvProductDto> csvProduct, int fileId)
        {
            foreach (var item in csvProduct)
            {

                var product = new Products()
                {
                    Barcode = item.barcode,
                    Brand = item.brand,
                    Category = item.category,
                    Description = item.description,
                    Desi = Convert.ToDecimal(item.Desi),
                    FileId = fileId,
                    Name = item.name,
                    Price = Convert.ToDecimal(item.price),
                    ProductCode = item.product_code,
                    Stock = Convert.ToInt32(item.Stock),
                };
                _context.Products.Add(product);
                _context.SaveChanges();
                for (int i = 1; i < 6; i++)
                {
                    var productImage = new ProductImage();
                    switch (i)
                    {
                        case 1:
                            productImage.ImagePath = item.Image1;
                            break;
                        case 2:
                            productImage.ImagePath = item.Image2;
                            break;
                        case 3:
                            productImage.ImagePath = item.Image3;
                            break;
                        case 4:
                            productImage.ImagePath = item.Image4;
                            break;
                        case 5:
                            productImage.ImagePath = item.Image5;
                            break;
                        default:
                            break;
                    };
                    productImage.ProductId = product.Id;
                    _context.ProductImages.Add(productImage);
                    _context.SaveChanges();
                }
            }
        }

        public string DeleteFile(string filePath)
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return "Resim Silindi";
                }
            }
            return "";
        }

        public string DownloadFile(AddMemberFileDto memberFileDto)
        {
            try
            {
                var localPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/excel/{memberFileDto.FileName}");
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"{memberFileDto.FtpUrl}");
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(memberFileDto.UserName, memberFileDto.FptPassword);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                using (FileStream writer = new FileStream(localPath, FileMode.Create))
                {
                    long length = response.ContentLength;
                    int bufferSize = 2048;
                    int readCount;
                    byte[] buffer = new byte[2048];

                    readCount = responseStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        writer.Write(buffer, 0, readCount);
                        readCount = responseStream.Read(buffer, 0, bufferSize);
                    }
                }
                reader.Close();
                response.Close();
                return localPath;
            }
            catch (Exception ex)
            {

                return "Kayıt Bulunamadı";
            }
        }

        public List<ProductListDto> GetAllProductsList(int fileId)
        {
            var result = _context.Products.Include(x => x.ProductImages).Where(x => x.FileId == fileId).Select(x => new ProductListDto
            {
                Barcode=x.Barcode,
                Brand=x.Brand,
                Category=x.Category,
                Desi=x.Desi,
                Name=x.Name,
                Price=x.Price,
                ProductCode=x.ProductCode,
                Stock=x.Stock,
                Image1=x.ProductImages.FirstOrDefault().ImagePath,
            }).ToList();
            return result;
        }

        public List<MemberFileDto> GetMemberFile(int memberId)
        {
            var result = _context.MemberFiles
                 .Where(x => x.MemberId == memberId)
                 .Select(x => new MemberFileDto { CreateDate = x.CreateDate, FileName = x.FileName, FtpUrl = x.FtpUrl, Id = x.Id })
                 .ToList();
            return result;
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

        public List<CsvProductDto> ReadFile(string filePath)
        {
            List<CsvProductDto> products = new List<CsvProductDto>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var product = csv.GetRecord<CsvProductDto>();
                    products.Add(product);
                }
            }
            return products;
        }

        public void Register(RegisterDto register)
        {
            byte[] passwordHash, passwordSalt;
            HashHelper.CreatePasswordHash(register.Password, out passwordHash, out passwordSalt);
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
