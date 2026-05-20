using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtniaApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EtniaApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public IActionResult ListarTodos()
        {
            var etnias = _context.Etnia.ToList();
            return Ok(etnias);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var etnia = _context.Etnia.FirstOrDefault(e => e.Codigo == id);

            if (etnia == null)
            {
                return NotFound(new { message = $"Etnia com código {id} não encontrada." });
            }

            return Ok(etnia);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] EtniaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novaEtnia = new Etnia
            {
                Descricao = dto.Descricao
            };

            _context.Etnia.Add(novaEtnia);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = novaEtnia.Codigo },
                novaEtnia
            );
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] EtniaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var etniaNoBanco = _context.Etnia.FirstOrDefault(e => e.Codigo == id);

            if (etniaNoBanco == null)
            {
                return NotFound(new { message = $"Etnia de código {id} não encontrada para atualização." });
            }

            etniaNoBanco.Descricao = dto.Descricao;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var etnia = _context.Etnia.FirstOrDefault(e => e.Codigo == id);

            if (etnia == null)
            {
                return NotFound(new { message = $"Etnia de código {id} não encontrada para exclusão." });
            }

            _context.Etnia.Remove(etnia);
            _context.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Etnia excluída com sucesso diretamente pela API!"
            });
        }
    }
}