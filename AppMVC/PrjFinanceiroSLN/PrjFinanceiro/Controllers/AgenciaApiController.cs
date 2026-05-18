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
                return NotFound(new { message = $"Funcionário com código {id} não encontrado." });

            return Ok(funcionario);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] FuncionarioCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoFuncionario = new Funcionario
            {
                Nome = dto.Nome,
                DataNascimento = dto.DataNascimento,
                Cidade = dto.Cidade,
                EstadoUF = dto.EstadoUF,
                CPF = dto.CPF,
                Telefone = dto.Telefone
            };

            _context.Funcionario.Add(novoFuncionario);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarPorId), new { id = novoFuncionario.Codigo }, novoFuncionario);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] FuncionarioCreateDto dto)
        {
            var funcionario = _context.Funcionario.FirstOrDefault(f => f.Codigo == id);

            if (funcionario == null)
                return NotFound(new { message = $"Funcionário com código {id} não encontrado." });

            funcionario.Nome = dto.Nome;
            funcionario.DataNascimento = dto.DataNascimento;
            funcionario.Cidade = dto.Cidade;
            funcionario.EstadoUF = dto.EstadoUF;
            funcionario.CPF = dto.CPF;
            funcionario.Telefone = dto.Telefone;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var funcionario = _context.Funcionario.FirstOrDefault(f => f.Codigo == id);

            if (funcionario == null)
                return NotFound(new { message = $"Funcionário não encontrado." });

            _context.Funcionario.Remove(funcionario);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Funcionário removido com sucesso!" });
        }
    }
}