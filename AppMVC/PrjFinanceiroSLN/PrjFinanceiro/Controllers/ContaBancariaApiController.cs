using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System;
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
        public IActionResult ListarTodas() => Ok(_context.ContaBancaria.Include(c => c.ClienteRel).ToList());

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var conta = _context.ContaBancaria.Include(c => c.ClienteRel).FirstOrDefault(c => c.Codigo == id);
            if (conta == null) return NotFound(new { message = "Conta não encontrada!" });
            return Ok(conta);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] ContaBancariaCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var erroFk = ValidarChavesEstrangeiras(dto);
            if (erroFk != null) return BadRequest(new { message = erroFk });

            var novaConta = new ContaBancaria
            {
                Nome = dto.Nome,
                DataNascimento = dto.DataNascimento,
                NumeroConta = dto.NumeroConta,
                TipoConta = dto.TipoConta,
                StatusConta = dto.StatusConta,
                CodigoCliente = dto.CodigoCliente,
                CodigoAgencia = dto.CodigoAgencia
            };

            _context.ContaBancaria.Add(novaConta);
            _context.SaveChanges();
            return CreatedAtAction(nameof(BuscarPorId), new { id = novaConta.Codigo }, novaConta);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ContaBancariaCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var conta = _context.ContaBancaria.Find(id);
            if (conta == null) return NotFound();

            var erroFk = ValidarChavesEstrangeiras(dto);
            if (erroFk != null) return BadRequest(new { message = erroFk });

            conta.Nome = dto.Nome;
            conta.DataNascimento = dto.DataNascimento;
            conta.NumeroConta = dto.NumeroConta;
            conta.TipoConta = dto.TipoConta;
            conta.StatusConta = dto.StatusConta;
            conta.CodigoCliente = dto.CodigoCliente;
            conta.CodigoAgencia = dto.CodigoAgencia;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var conta = _context.ContaBancaria.Find(id);
            if (conta == null) return NotFound();
            _context.ContaBancaria.Remove(conta);
            _context.SaveChanges();
            return Ok(new { success = true });
        }

        private string ValidarChavesEstrangeiras(ContaBancariaCreateDto dto)
        {
            // Verificação segura: funciona para int (valida se > 0) ou int? (valida se tem valor)
            if (dto.CodigoCliente > 0 && !_context.Cliente.Any(c => c.Codigo == dto.CodigoCliente))
                return $"Cliente {dto.CodigoCliente} não encontrado.";

            // Resolução do erro do HasValue:
            // Se o campo no DTO for 'int', use (dto.CodigoAgencia > 0)
            // Se for 'int?', use (dto.CodigoAgencia.HasValue)
            if (dto.CodigoAgencia > 0)
            {
                var existe = _context.Set<Agencia>().Any(a => a.Codigo == dto.CodigoAgencia);
                if (!existe) return $"Agência {dto.CodigoAgencia} não encontrada.";
            }

            return null;
        }
    }
}