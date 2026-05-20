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

        [HttpGet]
        public IActionResult ListarTodas()
        {
            var contas = _context.ContaBancaria.ToList();
            return Ok(contas);
        }

        [HttpGet("{id}")] // O parâmetro id mapeia para o número da conta bancária
        public IActionResult BuscarPorNumero(int id)
        {
            var conta = _context.ContaBancaria.FirstOrDefault(c => c.NumeroConta == id);
            if (conta == null)
            {
                return NotFound(new { message = $"Conta Bancária de número {id} não foi encontrada." });
            }
            return Ok(conta);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] ContaBancariaCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // 1. Validar se o Número da Conta já existe no banco (Evitar chave duplicada)
            var contaExistente = _context.ContaBancaria.Any(x => x.NumeroConta == dto.NumeroConta);
            if (contaExistente) return BadRequest(new { message = $"O número de conta {dto.NumeroConta} já está cadastrado." });

            // 2. Validar integridade da Chave Estrangeira (FK)
            var clienteExiste = _context.Cliente.Any(c => c.Codigo == dto.CodigoCliente);
            if (!clienteExiste) return BadRequest(new { message = $"A FK CodigoCliente {dto.CodigoCliente} informada não existe na tabela Cliente." });

            var novaConta = new ContaBancaria
            {
                NumeroConta = dto.NumeroConta,
                CodigoCliente = dto.CodigoCliente,
                CodigoAgencia = dto.CodigoAgencia,
                StatusConta = dto.StatusConta,
                TipoConta = dto.TipoConta
            };

            _context.ContaBancaria.Add(novaConta);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarPorNumero), new { id = novaConta.NumeroConta }, novaConta);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ContaBancariaCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var contaNoBanco = _context.ContaBancaria.FirstOrDefault(c => c.NumeroConta == id);
            if (contaNoBanco == null) return NotFound(new { message = $"A conta bancária número {id} não existe." });

            // Validar integridade da Chave Estrangeira (FK) caso queiram mudar o titular
            var clienteExiste = _context.Cliente.Any(c => c.Codigo == dto.CodigoCliente);
            if (!clienteExiste) return BadRequest(new { message = $"A FK CodigoCliente {dto.CodigoCliente} informada não existe." });

            contaNoBanco.CodigoCliente = dto.CodigoCliente;
            contaNoBanco.CodigoAgencia = dto.CodigoAgencia;
            contaNoBanco.StatusConta = dto.StatusConta;
            contaNoBanco.TipoConta = dto.TipoConta;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var conta = _context.ContaBancaria.FirstOrDefault(c => c.NumeroConta == id);
            if (conta == null) return NotFound(new { message = $"A conta bancária número {id} não existe." });

            _context.ContaBancaria.Remove(conta);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Conta bancária removida via API com sucesso!" });
        }
    }
}