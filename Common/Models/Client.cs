using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billify.API.Common.Models
{
    public class Client:BaseEntity
    {
        public string name { get; set; }
    }
}
