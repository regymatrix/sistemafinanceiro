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

        // =========================
        // LISTAR TODOS
        // =========================

        [HttpGet("lista")]
        public IActionResult ListarTodos()
        {
            var clientes = _context.Cliente.ToList();

            return Ok(clientes);
        }

        // =========================
        // BUSCAR POR ID
        // =========================

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var cliente = _context.Cliente
                .FirstOrDefault(c => c.Codigo == id);

            if (cliente == null)
            {
                return NotFound(new
                {
                    message = $"Cliente com código {id} não encontrado."
                });
            }

            return Ok(cliente);
        }

        // =========================
        // CADASTRAR
        // =========================

        [HttpPost]
        public IActionResult Cadastrar([FromBody] ClienteCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novoCliente = new Cliente
            {
                Nome = dto.Nome,
                DataNascimento = dto.DataNascimento,
                TipoCliente = dto.TipoCliente,
                CPF = dto.CPF,
                CNPJ = dto.CNPJ,
                CodigoBairro = dto.CodigoBairro
            };

            _context.Cliente.Add(novoCliente);

            _context.SaveChanges();

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = novoCliente.Codigo },
                novoCliente
            );
        }

        // =========================
        // ATUALIZAR
        // =========================

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ClienteCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clienteNoBanco = _context.Cliente
                .FirstOrDefault(c => c.Codigo == id);

            if (clienteNoBanco == null)
            {
                return NotFound(new
                {
                    message = $"Cliente de código {id} inexistente para atualização."
                });
            }

            clienteNoBanco.Nome = dto.Nome;
            clienteNoBanco.DataNascimento = dto.DataNascimento;
            clienteNoBanco.TipoCliente = dto.TipoCliente;
            clienteNoBanco.CPF = dto.CPF;
            clienteNoBanco.CNPJ = dto.CNPJ;
            clienteNoBanco.CodigoBairro = dto.CodigoBairro;

            _context.SaveChanges();

            return NoContent();
        }

        // =========================
        // REMOVER
        // =========================

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var cliente = _context.Cliente
                .FirstOrDefault(c => c.Codigo == id);

            if (cliente == null)
            {
                return NotFound(new
                {
                    message = $"Cliente de código {id} não encontrado para exclusão."
                });
            }

            _context.Cliente.Remove(cliente);

            _context.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Cliente excluído com sucesso diretamente pela API!"
            });
        }
    }
}