using CsvHelper.Configuration.Attributes;
using System;

namespace Manager.Dto
{
    public class CsvProductDto
    {

        [Index(0)]
        public string barcode { get; set; }
        [Index(1)]
        public string price { get; set; }
        [Index(2)]
        public string Stock { get; set; }
        [Index(3)]
        public string name { get; set; }
        [Index(4)]
        public string product_code { get; set; }
        [Index(5)]
        public string Image1 { get; set; }
        [Index(6)]
        public string Image2 { get; set; }
        [Index(7)]
        public string Image3 { get; set; }
        [Index(8)]
        public string Image4 { get; set; }
        [Index(9)]
        public string Image5 { get; set; }
        [Index(10)]
        public string product_id { get; set; }
        [Index(11)]
        public string description { get; set; }
        [Index(13)]
        public string brand { get; set; }
        [Index(16)]
        public string category { get; set; }
        [Index(24)]
        public string Desi { get; set; }


    }
}
