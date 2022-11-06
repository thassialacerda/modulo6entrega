using Agencia.Models;
using Agencia.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]

        public async Task<ActionResult<IAsyncEnumerable<Cliente>>> GetClientes()
        {
            try
            {
                var clientes = await _clienteService.GetClientes();
                return Ok(clientes);
            }
            catch
            {
                return BadRequest("Resquest inválido");

            }
        }
        [HttpGet("ClientePorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Cliente>>> GetClientesByName([FromQuery] string nome)
        {
            try
            {
                var clientes = await _clienteService.GetClientesByNome(nome);

                if (clientes.Count() == 0)
                    return NotFound($"Não existem clientes com o critério {nome}");

                return Ok(clientes);
            }
            catch
            {
                return BadRequest("Resquest inválido");

            }
        }
        [HttpGet("{id:int}", Name = "GetCliente")]

        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            try
            {
                var cliente = await _clienteService.GetCliente(id);

                if (cliente == null)
                    return NotFound($"Não existe aluno com id={id}");
                return Ok(cliente);
            }
            catch
            {

                return BadRequest("Request inválido");
            }

        }

        [HttpPost]

        public async Task<ActionResult> Create(Cliente cliente)
        {
            try
            {
                await _clienteService.CreateCliente(cliente);
                return CreatedAtRoute(nameof(GetCliente), new { id = cliente.Id }, cliente);


            }
            catch
            {

                return BadRequest("Request inválido");
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Cliente cliente)
        {
            try
            {
                if (cliente.Id == id)
                {
                    await _clienteService.UpdateCliente(cliente);
                    //  return NoContent();
                    return Ok($"Cliente com id={id} foi atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch
            {

                return BadRequest("Request inválido");
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {

                var cliente = await _clienteService.GetCliente(id);
                if (cliente != null)
                {
                    await _clienteService.DeleteCliente(cliente);
                    return Ok($"Cliente de id={id} foi excluido com sucesso");
                }
                else
                {
                    return NotFound("Cliente com id={id} não encontrado");
                }

            }
            catch
            {

                return BadRequest("Request inválido");
            }
        }


    }
}

