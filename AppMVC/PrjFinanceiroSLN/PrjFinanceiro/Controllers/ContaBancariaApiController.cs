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

        // GET: api/ContaBancariaApi
        [HttpGet]
        public IActionResult ListarTodos()
        {
            var contas = _context.ContaBancaria.ToList();

            return Ok(contas);
        }

        // GET: api/ContaBancariaApi/5
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var conta = _context.ContaBancaria.FirstOrDefault(a => a.CodigoCliente == id);

            if (conta == null)
            {
                return NotFound(new
                {
                    message = $"Conta Bancária com código {id} não encontrada."
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

            var novaConta = new ContaBancaria
            {
                CodigoCliente = dto.CodigoCliente,
                CodigoAgencia = dto.CodigoAgencia,
                NumeroConta = dto.NumeroConta,
                StatusConta = dto.StatusConta,
                TipoConta = dto.TipoConta
            };

            _context.ContaBancaria.Add(novaConta);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarPorId),
                new { id = novaConta.CodigoCliente },
                novaConta
            );
        }

        // PUT: api/ContaBancariaApi/5
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ContaBancariaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contaNoBanco = _context.ContaBancaria.FirstOrDefault(a => a.CodigoCliente == id);

            if (contaNoBanco == null)
            {
                return NotFound(new
                {
                    message = $"Conta Bancária com código {id} não encontrada."
                });
            }

            contaNoBanco.CodigoCliente = dto.CodigoCliente;
            contaNoBanco.CodigoAgencia = dto.CodigoAgencia;
            contaNoBanco.NumeroConta = dto.NumeroConta;
            contaNoBanco.StatusConta = dto.StatusConta;
            contaNoBanco.TipoConta = dto.TipoConta;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/ContaBancariaApi/5
        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var conta = _context.ContaBancaria.FirstOrDefault(a => a.CodigoCliente == id);

            if (conta == null)
            {
                return NotFound(new
                {
                    message = $"Conta Bancária com código {id} não encontrada."
                });
            }

            _context.ContaBancaria.Remove(conta);
            _context.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Conta Bancária excluída com sucesso!"
            });
        }
    }
}