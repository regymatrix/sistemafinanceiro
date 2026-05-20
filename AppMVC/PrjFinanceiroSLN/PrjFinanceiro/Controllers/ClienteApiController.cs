using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System;
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
                return NotFound(new { message = $"Cliente com código {id} não encontrado!" });
            }
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] ClienteCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var erroFk = ValidarChavesEstrangeiras(dto);
            if (erroFk != null)
            {
                return BadRequest(new { message = erroFk });
            }

            var novoCliente = new Cliente
            {
                Nome = dto.Nome,
                DataNascimento = dto.DataNascimento,
                Tipo = dto.Tipo,
                // Limpeza de máscaras e tratamento de nulos
                CPF = dto.CPF?.Replace(".", "").Replace("-", "") ?? "",
                CNPJ = dto.CNPJ?.Replace(".", "").Replace("-", "").Replace("/", "") ?? "",
                CodigoBairro = dto.CodigoBairro
            };

            _context.Cliente.Add(novoCliente);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarPorId), new { id = novoCliente.Codigo }, novoCliente);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ClienteCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clienteNoBanco = _context.Cliente.FirstOrDefault(c => c.Codigo == id);
            if (clienteNoBanco == null)
            {
                return NotFound(new { message = $"Cliente de código {id} inexistente para atualização." });
            }

            var erroFk = ValidarChavesEstrangeiras(dto);
            if (erroFk != null)
            {
                return BadRequest(new { message = erroFk });
            }

            clienteNoBanco.Nome = dto.Nome;
            clienteNoBanco.DataNascimento = dto.DataNascimento;
            clienteNoBanco.Tipo = dto.Tipo;
            clienteNoBanco.CPF = dto.CPF?.Replace(".", "").Replace("-", "") ?? "";
            clienteNoBanco.CNPJ = dto.CNPJ?.Replace(".", "").Replace("-", "").Replace("/", "") ?? "";
            clienteNoBanco.CodigoBairro = dto.CodigoBairro;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var cliente = _context.Cliente.FirstOrDefault(c => c.Codigo == id);
            if (cliente == null)
            {
                return NotFound(new { message = $"Cliente de código {id} não encontrado para exclusão." });
            }

            _context.Cliente.Remove(cliente);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Cliente excluído com sucesso!" });
        }

        private string ValidarChavesEstrangeiras(ClienteCreateDto dto)
        {
            if (dto.CodigoBairro.HasValue && !_context.Bairro.Any(b => b.Codigo == dto.CodigoBairro))
                return $"A FK CodigoBairro '{dto.CodigoBairro}' informada não existe.";

            return null;
        }
    }
}