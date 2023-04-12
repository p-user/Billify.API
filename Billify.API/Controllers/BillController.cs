using Billify.API.Bussiness.Services;
using Billify.API.Common.Dtos;
using Billify.API.Common.Interfaces;
using Billify.API.Infrastructure.Authentication;
using Clientify.API.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

namespace Billify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BillController : ControllerBase
    {
        private IBillService BillService { get; }
        private ILogger<BillController> Logger { get; }

        public BillController(IBillService billService,
            ILogger<BillController> logger)
        {
            BillService =billService;
            Logger = logger;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<IActionResult> CreateBill(BillDto new_bill)
        {
            var id = await BillService.CreateBillAsync(new_bill);
            return Ok(id);
        }
        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<IActionResult> UpdateBill(BillDto bill)
        {
            bill.last_modified_on= DateTime.Now;
            bill.confirmed_on= DateTime.Now;
            await BillService.UpdateBillAsync(bill);
            return Ok();
        }
        [HttpPut]
        [Route("Confirm")]
        [Authorize(Roles = UserRoles.Supervisor)]
        public async Task<IActionResult> ConfirmBill(BillDto bill)
        {
            bill.status=true;
            bill.last_modified_on= DateTime.Now;
            bill.confirmed_on= DateTime.Now;
            await BillService.UpdateBillAsync(bill);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<IActionResult> DeleteBill(int Id)
        {
            await BillService.DeleteBillAsync(Id);
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<IActionResult> GetBill(int id)
        {
            using (LogContext.PushProperty("Bill Id", id))
            {
                var bill = await BillService.GetBillAsync(id);
                return Ok(bill);
            }
        }

        [HttpGet]
        [Route("Get")]
        [Authorize(Roles = UserRoles.Operator)]
        public async Task<IActionResult> GetAllBills()
        {
            var all_bills = await BillService.GetBillsAsync();
            return Ok(all_bills);
        }
    }
}
