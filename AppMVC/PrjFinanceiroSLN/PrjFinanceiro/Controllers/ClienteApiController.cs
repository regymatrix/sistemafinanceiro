using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var clientes = _context.Cliente.ToList();
            return Ok(clientes); // Status HTTP 200 OK com o JSON da lista
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var cliente = _context.Cliente.FirstOrDefault(a => a.Codigo == id);

            if (cliente == null)
            {
                return NotFound(new { message = $"Cliente com código {id} não encontrado." }); // HTTP 404
            }

            return Ok(cliente); // HTTP 200 OK
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] ClienteCreateDto dto)
        {
            // O [ApiController] já faz essa validação automaticamente, mas demonstrar em código reforça o aprendizado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 Bad Request
            }

            var novoCliente = new Cliente
            {
                Nome = dto.Nome,
                DataNascimento= dto.DataNascimento,
                TipoCliente = dto.TipoCliente, 
                CPF = dto.CPF,
                CNPJ = dto.CNPJ, 
                CodigoBairro = dto.CodigoBairro
            };

            _context.Cliente.Add(novoCliente);
            _context.SaveChanges();

            // Padrão REST excelente: Retorna Status 201 Created, popula o cabeçalho 'Location' com a URL de consulta 
            // e entrega o objeto recém-criado com a ID gerada pelo banco de dados.
            return CreatedAtAction(nameof(BuscarPorId), new { id = novoCliente.Codigo }, novoCliente);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ClienteCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clienteNoBanco = _context.Cliente.FirstOrDefault(a => a.Codigo == id);

            if (clienteNoBanco == null)
            {
                return NotFound(new { message = $"Cliente de código {id} inexistente para atualização." });
            }

            // Mapeando dados do DTO para a entidade monitorada pelo EF
            clienteNoBanco.Nome = dto.Nome;
            clienteNoBanco.DataNascimento = dto.DataNascimento;
            clienteNoBanco.TipoCliente = dto.TipoCliente;
            clienteNoBanco.CPF = dto.CPF;
            clienteNoBanco.CNPJ = dto.CNPJ;
            clienteNoBanco.CodigoBairro = dto.CodigoBairro;

            _context.SaveChanges();

            return NoContent(); // HTTP 204 No Content (Sucesso padrão REST para atualizações)
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var cliente = _context.Cliente.FirstOrDefault(a => a.Codigo == id);

            if (cliente == null)
            {
                return NotFound(new { message = $"Cliente de código {id} não encontrada para exclusão." });
            }

            _context.Cliente.Remove(cliente);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Cliente excluído com sucesso diretamente pela API!" }); // HTTP 200
        }
    }
}
