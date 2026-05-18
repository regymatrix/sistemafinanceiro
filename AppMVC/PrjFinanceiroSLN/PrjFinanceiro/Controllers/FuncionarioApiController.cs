using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
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

        [HttpGet("lista")]
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
                return NotFound(new { message = $"Funcionário com código {id} não encontrado." });
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

            var novoFuncionario = new Funcionario
            {
                Nome = dto.Nome,
                DataNascimento = dto.DataNascimento,
                Cidade = dto.Cidade,
                EstadoUF = dto.EstadoUF,
                CPF = dto.CPF,
                Telefone = dto.Telefone,
                CodigoEscolaridade = dto.CodigoEscolaridade,
                CodigoEtnia = dto.CodigoEtnia
            };

            _context.Funcionario.Add(novoFuncionario);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = novoFuncionario.Codigo },
                novoFuncionario
            );
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
                return NotFound(new { message = $"Funcionário de código {id} não encontrado para atualização." });
            }

            funcionarioNoBanco.Nome = dto.Nome;
            funcionarioNoBanco.DataNascimento = dto.DataNascimento;
            funcionarioNoBanco.Cidade = dto.Cidade;
            funcionarioNoBanco.EstadoUF = dto.EstadoUF;
            funcionarioNoBanco.CPF = dto.CPF;
            funcionarioNoBanco.Telefone = dto.Telefone;
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

            return Ok(new
            {
                success = true,
                message = "Funcionário excluído com sucesso diretamente pela API!"
            });
        }
    }
}