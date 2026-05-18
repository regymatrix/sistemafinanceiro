using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("lista")]
        public IActionResult ListarTodos()
        {
            var escolaridades = _context.Escolaridade.ToList();
            return Ok(escolaridades);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var escolaridade = _context.Escolaridade.FirstOrDefault(e => e.Codigo == id);

            if (escolaridade == null)
            {
                return NotFound(new { message = $"Escolaridade com código {id} não encontrada." });
            }

            return Ok(escolaridade);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Escolaridade escolaridade)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Escolaridade.Add(escolaridade);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarPorId), new { id = escolaridade.Codigo }, escolaridade);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Escolaridade dto)
        {
            var escolaridadeNoBanco = _context.Escolaridade.FirstOrDefault(e => e.Codigo == id);

            if (escolaridadeNoBanco == null)
                return NotFound();

            escolaridadeNoBanco.Descricao = dto.Descricao;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var escolaridade = _context.Escolaridade.FirstOrDefault(e => e.Codigo == id);

            if (escolaridade == null)
                return NotFound();

            _context.Escolaridade.Remove(escolaridade);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Escolaridade removida com sucesso!" });
        }
    }
}