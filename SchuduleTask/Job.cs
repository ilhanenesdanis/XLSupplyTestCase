using CsvHelper;
using Manager;
using Manager.Dto;
using Manager.Entity;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SchuduleTask
{
    public class Job : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var db = new Context();
            var MemberList = GetAllMember(db);
            foreach (var item in MemberList)
            {
                var filePath = DownloadFile(item);
                var list = ReadFile(filePath);
                DeleteFile(filePath);
                AddOrUpdate(list, item.Id, db);
            }
            Console.WriteLine("Sistemdeki Exceller Okunup Güncellendi");
        }
        private List<AddMemberFileDto> GetAllMember(Context context)
        {
            var list = context.MemberFiles
                .Select(x => new AddMemberFileDto { FileName = x.FileName, FptPassword = x.FptPassword, FtpUrl = x.FtpUrl, MemberId = x.MemberId, Id = x.Id, UserName = x.UserName })
                .ToList();
            return list;
        }
        private string DownloadFile(AddMemberFileDto memberFileDto)
        {
            try
            {
                var localPath = Path.Combine(@"C:\Users\MONSTER\source\repos\XLSupplyTestCase\SchuduleTask\wwwroot\excel/", memberFileDto.FileName);
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
        private List<CsvProductDto> ReadFile(string filePath)
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
        private void DeleteFile(string filePath)
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
        private void AddOrUpdate(List<CsvProductDto> csvProduct, int fileId, Context context)
        {
            var oldProductList = context.Products.Where(x => x.FileId == fileId).ToList();
            foreach (var item in csvProduct)
            {
                if (oldProductList.Any(x => x.Barcode == item.barcode))//update
                {
                    var product = new Products()
                    {
                        Id = oldProductList.FirstOrDefault(x => x.Barcode == item.barcode).Id,
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
                        CreateDate = oldProductList.FirstOrDefault(x => x.Barcode == item.barcode).CreateDate,
                        Status = true
                    };
                    context.Products.Update(product);
                    context.SaveChanges();
                    var productImageList = context.ProductImages.ToList();
                    
                    context.ProductImages.RemoveRange(productImageList.Where(x=>x.Id==product.Id).ToList());
                    context.SaveChanges();
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
                        context.ProductImages.Add(productImage);
                        context.SaveChanges();
                    }
                }
                else
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
                    context.Products.Add(product);
                    context.SaveChanges();
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
                        context.ProductImages.Add(productImage);
                        context.SaveChanges();
                    }
                }
            }
        }

    }
}
