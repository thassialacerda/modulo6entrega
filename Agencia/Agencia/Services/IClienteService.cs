using Agencia.Models;

namespace Agencia.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetClientes();
        Task<Cliente> GetCliente(int id);
        Task<IEnumerable<Cliente>> GetClientesByNome(String nome);

        Task CreateCliente(Cliente cliente);
        Task UpdateCliente(Cliente cliente);    
        Task DeleteCliente(Cliente cliente);
    }
}
