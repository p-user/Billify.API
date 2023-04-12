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
   [Authorize(Roles=UserRoles.Supervisor)]
    public class ClientController : ControllerBase
    {
        private IClientService ClientService { get; }
        private ILogger<ClientController> Logger { get; }

        public ClientController(IClientService clientService,
            ILogger<ClientController> logger)
        {
           ClientService =clientService;
            Logger = logger;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateClient(ClientDto ClientCreate)
        {
            var id = await ClientService.CreateClientAsync(ClientCreate);
            return Ok(id);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateClient(ClientDto ClientUpdate)
        {
            await ClientService.UpdateClientAsync(ClientUpdate);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteClient(int Id)
        {
            await ClientService.DeleteClientAsync(Id);
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            using (LogContext.PushProperty("Client Id", id))
            {
                var Client = await ClientService.GetClientByIdAsync(id);
                return Ok(Client);
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAllClients()
        {
            var Clients = await ClientService.GetClientsAsync();
            return Ok(Clients);
        }
    }
}
