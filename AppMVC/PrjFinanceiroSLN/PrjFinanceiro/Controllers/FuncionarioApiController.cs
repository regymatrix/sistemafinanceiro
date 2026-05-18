using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FuncionarioApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var funcionarios = _context.Funcionario.ToList();
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var funcionario = _context.Funcionario.FirstOrDefault(f => f.Codigo == id);
            if (funcionario == null)
            {
                return NotFound(new { message = $"Funcionário com código {id} não encontrado!" });
            }
            return Ok(funcionario);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] FuncionarioCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validação de todas as Chaves Estrangeiras (FKs)
            var erroFk = ValidarChavesEstrangeiras(dto);
            if (erroFk != null)
            {
                return BadRequest(new { message = erroFk });
            }

            var novoFuncionario = new Funcionario
            {
                Nome = dto.Nome,
                DataNascimento = dto.DataNascimento,
                // Limpa formatação caso venha com máscara do client-side
                CPF = dto.CPF.Replace(".", "").Replace("-", ""),
                Telefone = dto.Telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""),
                EstadoUF = dto.EstadoUF.ToUpper(),
                CodigoCidade = dto.CodigoCidade,
                CodigoBairro = dto.CodigoBairro,
                CodigoEscolaridade = dto.CodigoEscolaridade,
                CodigoEtnia = dto.CodigoEtnia
            };

            _context.Funcionario.Add(novoFuncionario);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarPorId), new { id = novoFuncionario.Codigo }, novoFuncionario);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] FuncionarioCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var funcionarioNoBanco = _context.Funcionario.FirstOrDefault(f => f.Codigo == id);
            if (funcionarioNoBanco == null)
            {
                return NotFound(new { message = $"Funcionário de código {id} inexistente para atualização." });
            }

            // Validação de todas as Chaves Estrangeiras (FKs)
            var erroFk = ValidarChavesEstrangeiras(dto);
            if (erroFk != null)
            {
                return BadRequest(new { message = erroFk });
            }

            funcionarioNoBanco.Nome = dto.Nome;
            funcionarioNoBanco.DataNascimento = dto.DataNascimento;
            funcionarioNoBanco.CPF = dto.CPF.Replace(".", "").Replace("-", "");
            funcionarioNoBanco.Telefone = dto.Telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            funcionarioNoBanco.EstadoUF = dto.EstadoUF.ToUpper();
            funcionarioNoBanco.CodigoCidade = dto.CodigoCidade;
            funcionarioNoBanco.CodigoBairro = dto.CodigoBairro;
            funcionarioNoBanco.CodigoEscolaridade = dto.CodigoEscolaridade;
            funcionarioNoBanco.CodigoEtnia = dto.CodigoEtnia;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var funcionario = _context.Funcionario.FirstOrDefault(f => f.Codigo == id);
            if (funcionario == null)
            {
                return NotFound(new { message = $"Funcionário de código {id} não encontrado para exclusão." });
            }

            _context.Funcionario.Remove(funcionario);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Funcionário excluído com sucesso diretamente pela API!" });
        }

        // Método auxiliar centralizado para validação de integridade das FKs
        private string ValidarChavesEstrangeiras(FuncionarioCreateDto dto)
        {
            if (!_context.Cidade.Any(c => c.Codigo == dto.CodigoCidade))
                return $"A FK CodigoCidade '{dto.CodigoCidade}' informada não existe.";

            if (!_context.Bairro.Any(b => b.Codigo == dto.CodigoBairro))
                return $"A FK CodigoBairro '{dto.CodigoBairro}' informada não existe.";

            if (!_context.Escolaridade.Any(e => e.Codigo == dto.CodigoEscolaridade))
                return $"A FK CodigoEscolaridade '{dto.CodigoEscolaridade}' informada não existe.";

            if (!_context.Etnia.Any(et => et.Codigo == dto.CodigoEtnia))
                return $"A FK CodigoEtnia '{dto.CodigoEtnia}' informada não existe.";

            return null; // Nenhuma inconsistência encontrada
        }
    }
}