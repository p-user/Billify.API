using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billify.API.Common.Models
{
    public class Bill : BaseEntity
    {
        public Client? client { get; set; }
        public DateTime? issued_on { get; set; }
        //public int? issued_by_userId { get; set; }
        //public int? last_modified_by_userId { get; set; }
        public DateTime? last_modified_on { get; set; }
        public bool status { get; set; }
        //public int? confirmed_by_userId { get; set; }
        public DateTime? confirmed_on { get; set; }
        //public List<Product> products { get; set; }
        public IList<Bill_Product> bill_products { get; set; }
    }
}
