using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billify.API.Common.Dtos;
using Billify.API.Common.Models;

namespace Billify.API.Common.Interfaces
{
    public interface IBillService
    {
        Task<int> CreateBillAsync(BillDto billcreate);
        Task<int> UpdateBillAsync(BillDto updated_obj);
        Task DeleteBillAsync(int Id);
        Task<BillDto> GetBillAsync(int Id);
        Task<List<BillDto>> GetBillsAsync();
        //Task<List<Bill_Product>> GetProductsByBillId(int billid);
    }
}
