using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaBancariaApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContaBancariaApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ContaBancariaApi/lista
        [HttpGet("lista")]
        public IActionResult ListarTodos()
        {
            var contas = _context.ContaBancaria.ToList();

            return Ok(contas);
        }

        // GET: api/ContaBancariaApi/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(string id)
        {
            var conta = _context.ContaBancaria
                .FirstOrDefault(c => c.NumeroConta == id);

            if (conta == null)
            {
                return NotFound(new
                {
                    message = $"Conta bancária {id} não encontrada."
                });
            }

            return Ok(conta);
        }

        // POST: api/ContaBancariaApi
        [HttpPost]
        public IActionResult Cadastrar([FromBody] ContaBancariaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Busca os relacionamentos
            var cliente = _context.Cliente
                .FirstOrDefault(c => c.Codigo == dto.CodigoCliente);

            var agencia = _context.Agencia
                .FirstOrDefault(a => a.Codigo == dto.CodigoAgencia);

            if (cliente == null)
            {
                return NotFound(new
                {
                    message = "Cliente não encontrado."
                });
            }

            if (agencia == null)
            {
                return NotFound(new
                {
                    message = "Agência não encontrada."
                });
            }

            var novaConta = new ContaBancaria
            {
                NumeroConta = dto.NumeroConta,
                StatusConta = dto.StatusConta,
                ContaCliente = cliente,
                CodigoAgencia = agencia,
                TipoConta = dto.TipoConta
            };

            _context.ContaBancaria.Add(novaConta);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = novaConta.NumeroConta },
                novaConta
            );
        }

        // PUT: api/ContaBancariaApi/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(
            string id,
            [FromBody] ContaBancariaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contaNoBanco = _context.ContaBancaria
                .FirstOrDefault(c => c.NumeroConta == id);

            if (contaNoBanco == null)
            {
                return NotFound(new
                {
                    message = $"Conta bancária {id} não encontrada para atualização."
                });
            }

            var cliente = _context.Cliente
                .FirstOrDefault(c => c.Codigo == dto.CodigoCliente);

            var agencia = _context.Agencia
                .FirstOrDefault(a => a.Codigo == dto.CodigoAgencia);

            if (cliente == null)
            {
                return NotFound(new
                {
                    message = "Cliente não encontrado."
                });
            }

            if (agencia == null)
            {
                return NotFound(new
                {
                    message = "Agência não encontrada."
                });
            }

            contaNoBanco.StatusConta = dto.StatusConta;
            contaNoBanco.ContaCliente = cliente;
            contaNoBanco.CodigoAgencia = agencia;
            contaNoBanco.TipoConta = dto.TipoConta;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/ContaBancariaApi/{id}
        [HttpDelete("{id}")]
        public IActionResult Remover(string id)
        {
            var conta = _context.ContaBancaria
                .FirstOrDefault(c => c.NumeroConta == id);

            if (conta == null)
            {
                return NotFound(new
                {
                    message = $"Conta bancária {id} não encontrada para exclusão."
                });
            }

            _context.ContaBancaria.Remove(conta);
            _context.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Conta bancária excluída com sucesso pela API!"
            });
        }
    }
}