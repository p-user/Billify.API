using Billify.API.Common.Dtos;

namespace Clientify.API.Common.Interfaces
{
    public interface IClientService
    {
        Task<int> CreateClientAsync(ClientDto Clientcreate);
        Task<int>UpdateClientAsync(ClientDto Clientconfirm);
        Task DeleteClientAsync(int Id);
        Task<ClientDto> GetClientByIdAsync(int Id);
        Task<List<ClientDto>> GetClientsAsync();
    }
}
