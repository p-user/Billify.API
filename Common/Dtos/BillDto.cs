using Billify.API.Common.Models;
using System.Runtime.CompilerServices;

namespace Billify.API.Common.Dtos
{
    public class BillDto
    {
        public BillDto(int Id, ClientDto Client, DateTime? issued_on, bool status, DateTime? last_modified_on, DateTime? confirmed_on, List<Product_QuantityDto> products)
        {
            this.Id = Id;
            this.products=products;
            this.issued_on = issued_on;
            this.status = status;
            this.Client=Client;
            this.issued_on=issued_on;
            this.confirmed_on=confirmed_on;
            this.last_modified_on=last_modified_on;
        }
        public int Id { get; set; }
        public ClientDto Client { get; set; }
        public DateTime? issued_on { get; set; }
        public bool status { get; set; }
        public DateTime? last_modified_on { get; set; }
        public DateTime? confirmed_on { get; set; }
        public List<Product_QuantityDto> products { get; set; }
    };
}