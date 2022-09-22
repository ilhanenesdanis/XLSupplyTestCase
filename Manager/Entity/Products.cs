using System;
using System.Collections.Generic;

namespace Manager.Entity
{
    public class Products : BaseEntity
    {
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public int Desi { get; set; }
        public int FileId { get; set; }
        public MemberFiles MemberFiles { get; set; }
        public List<ProductImage> ProductImages { get; set; }

    }
}
