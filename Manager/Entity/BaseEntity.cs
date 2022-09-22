using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
