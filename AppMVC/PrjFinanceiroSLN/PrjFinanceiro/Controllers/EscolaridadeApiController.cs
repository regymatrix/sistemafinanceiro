using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscolaridadeApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EscolaridadeApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var escolaridades = _context.Escolaridade.ToList();
            return Ok(escolaridades);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var escolaridade = _context.Escolaridade.FirstOrDefault(a => a.Codigo == id);
            if (escolaridade == null)
            {
                return NotFound(new { message = $"Escolaridade com código {id} não encontrada!" });
            }
            return Ok(escolaridade);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] EscolaridadeCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novaEscolaridade = new Escolaridade
            {
                Descricao = dto.Descricao
            };

            _context.Escolaridade.Add(novaEscolaridade);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarPorId), new { id = novaEscolaridade.Codigo }, novaEscolaridade);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] EscolaridadeCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var escolaridadeNoBanco = _context.Escolaridade.FirstOrDefault(a => a.Codigo == id);
            if (escolaridadeNoBanco == null)
            {
                return NotFound(new { message = $"Escolaridade de código {id} inexistente para atualização." });
            }

            escolaridadeNoBanco.Descricao = dto.Descricao;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var escolaridade = _context.Escolaridade.FirstOrDefault(a => a.Codigo == id);
            if (escolaridade == null)
            {
                return NotFound(new { message = $"Escolaridade de código {id} não encontrada para exclusão." });
            }

            _context.Escolaridade.Remove(escolaridade);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Escolaridade excluída com sucesso diretamente pela API!" });
        }
    }
}