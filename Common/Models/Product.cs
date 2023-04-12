using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billify.API.Common.Models
{
    public class Product : BaseEntity
    {
        public string name { get; set; }
        public bool status { get; set; }
        //public List<Bill> bills { get; set; }
        public IList<Bill_Product> bill_products { get; set; }
    }
}
