using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billify.API.Common.Models
{
    public class Bill_Product:BaseEntity
    {
        public Product product { get; set; }
        public int productId { get; set; }
        public int billId { get; set; }
        public Bill bill { get; set; }
        public int prd_quantity { get; set; }
    }
}
