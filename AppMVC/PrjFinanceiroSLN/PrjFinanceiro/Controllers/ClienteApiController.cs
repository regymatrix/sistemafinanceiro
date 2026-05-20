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
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var cliente = _context.Cliente.FirstOrDefault(c => c.Codigo == id);
            if (cliente == null)
            {
                return NotFound(new { message = $"Cliente de código {id} não encontrado." });
            }
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] ClienteCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Validação de FK
            var bairroExiste = _context.Bairro.Any(b => b.Codigo == dto.CodigoBairro);
            if (!bairroExiste) return BadRequest(new { message = $"A FK CodigoBairro {dto.CodigoBairro} não existe." });

            var novoCliente = new Cliente
            {
                Nome = dto.Nome,
                DataNascimento = dto.DataNascimento,
                TipoCliente = dto.TipoCliente,
                CPF = dto.TipoCliente == "Física" ? dto.CPF?.Replace(".", "").Replace("-", "") : null,
                CNPJ = dto.TipoCliente == "Jurídica" ? dto.CNPJ?.Replace(".", "").Replace("-", "").Replace("/", "") : null,
                CodigoBairro = dto.CodigoBairro
            };

            _context.Cliente.Add(novoCliente);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarPorId), new { id = novoCliente.Codigo }, novoCliente);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ClienteCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var clienteNoBanco = _context.Cliente.FirstOrDefault(c => c.Codigo == id);
            if (clienteNoBanco == null) return NotFound(new { message = $"Cliente {id} inexistente." });

            var bairroExiste = _context.Bairro.Any(b => b.Codigo == dto.CodigoBairro);
            if (!bairroExiste) return BadRequest(new { message = $"A FK CodigoBairro {dto.CodigoBairro} não existe." });

            clienteNoBanco.Nome = dto.Nome;
            clienteNoBanco.DataNascimento = dto.DataNascimento;
            clienteNoBanco.TipoCliente = dto.TipoCliente;
            clienteNoBanco.CPF = dto.TipoCliente == "Física" ? dto.CPF?.Replace(".", "").Replace("-", "") : null;
            clienteNoBanco.CNPJ = dto.TipoCliente == "Jurídica" ? dto.CNPJ?.Replace(".", "").Replace("-", "").Replace("/", "") : null;
            clienteNoBanco.CodigoBairro = dto.CodigoBairro;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var cliente = _context.Cliente.FirstOrDefault(c => c.Codigo == id);
            if (cliente == null) return NotFound(new { message = $"Cliente {id} não encontrado." });

            _context.Cliente.Remove(cliente);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Cliente removido via API com sucesso!" });
        }
    }
}