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

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var funcionarios = _context.Funcionario.ToList();
            return Ok(funcionarios); // Status HTTP 200 OK com o JSON da lista
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var funcionario = _context.Funcionario.FirstOrDefault(a => a.Codigo == id);

            if (funcionario == null)
            {
                return NotFound(new { message = $"Funcionário com código {id} não encontrada." }); // HTTP 404
            }

            return Ok(funcionario); // HTTP 200 OK
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] FuncionarioCreateDto dto)
        {
            // O [ApiController] já faz essa validação automaticamente, mas demonstrar em código reforça o aprendizado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 Bad Request
            }

            var novoFuncionario = new Funcionario
            {
                Nome = dto.Nome,
                Cidade = dto.Cidade,
                EstadoUF = dto.EstadoUF,
                DataNascimento = dto.DataNascimento,
                CPF = dto.CPF,
                Telefone = dto.Telefone

            };

            _context.Funcionario.Add(novoFuncionario);
            _context.SaveChanges();

            // Padrão REST excelente: Retorna Status 201 Created, popula o cabeçalho 'Location' com a URL de consulta 
            // e entrega o objeto recém-criado com a ID gerada pelo banco de dados.
            return CreatedAtAction(nameof(BuscarPorId), new { id = novoFuncionario.Codigo }, novoFuncionario);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] FuncionarioCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var funcionarioNoBanco = _context.Funcionario.FirstOrDefault(a => a.Codigo == id);

            if (funcionarioNoBanco == null)
            {
                return NotFound(new { message = $"Funcionário de código {id} inexistente para atualização." });
            }

            // Mapeando dados do DTO para a entidade monitorada pelo EF
            funcionarioNoBanco.Nome = dto.Nome;
            funcionarioNoBanco.Cidade = dto.Cidade;
            funcionarioNoBanco.DataNascimento = dto.DataNascimento;
            funcionarioNoBanco.EstadoUF = dto.EstadoUF;
            funcionarioNoBanco.CPF = dto.CPF;
            funcionarioNoBanco.Telefone = dto.Telefone;

            _context.SaveChanges();

            return NoContent(); // HTTP 204 No Content (Sucesso padrão REST para atualizações)
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var funcionario = _context.Funcionario.FirstOrDefault(a => a.Codigo == id);

            if (funcionario == null)
            {
                return NotFound(new { message = $"Funcionário de código {id} não encontrado para exclusão." });
            }

            _context.Funcionario.Remove(funcionario);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Funcionário excluída com sucesso diretamente pela API!" }); // HTTP 200
        }
    }
}
