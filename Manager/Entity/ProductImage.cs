using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Entity
{
    public class ProductImage : BaseEntity
    {
        public string ImagePath { get; set; }
        public int ProductId { get; set; }
        public Products Products { get; set; }
    }
}
