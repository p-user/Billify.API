using AutoMapper;
using Billify.API.Common.Dtos;
using Billify.API.Common.Interfaces;
using Billify.API.Common.Models;
using Clientify.API.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Billify.API.Bussiness.Services
{
    public class ClientService : IClientService
    {
        private IMapper Mapper { get; }
        private IGenericRepository<Client> ClientRepository { get; }
        public ClientService(IMapper mapper, IGenericRepository<Client> clientRepository)
        {
            Mapper=mapper;
            ClientRepository=clientRepository;
        }

        public async Task<int> CreateClientAsync(ClientDto clientcreate)
        {
            var client_entity = Mapper.Map<Client>(clientcreate);
            await ClientRepository.InsertAsync(client_entity);
            await ClientRepository.SaveChangesAsync();
            return client_entity.Id;
        }

        public async Task DeleteClientAsync(int Id)
        {
            var client_entity = await ClientRepository.GetByIdAsync(Id);
            ClientRepository.Delete(client_entity);
            await ClientRepository.SaveChangesAsync();
        }

        public async Task<ClientDto> GetClientByIdAsync(int Id)
        {
            var client_entity = await ClientRepository.GetByIdAsync(Id);
            return Mapper.Map<ClientDto>(client_entity);
        }


        public async Task<List<ClientDto>> GetClientsAsync()
        {
            var clients_entity = await ClientRepository.GetAsync(null, null);
            return Mapper.Map<List<ClientDto>>(clients_entity);
        }

        public async Task<int> UpdateClientAsync(ClientDto Clientconfirm)
        {
            var client_entity = await ClientRepository.GetByIdAsync(Clientconfirm.Id);
            if(client_entity != null)
            {
                var entity = Mapper.Map<Client>(Clientconfirm);
                ClientRepository.Update(client_entity);
                await ClientRepository.SaveChangesAsync();
            }
            
            return client_entity.Id;
        }
    }
}
