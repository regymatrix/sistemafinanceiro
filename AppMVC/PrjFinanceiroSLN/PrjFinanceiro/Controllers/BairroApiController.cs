using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BairroApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BairroApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var bairros = _context.Bairro.ToList();

            return Ok(bairros);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var bairro = _context.Bairro.FirstOrDefault(b => b.Codigo == id);

            if (bairro == null)
            {
                return NotFound(new { message = $"Bairro com código {id} não encontrado." });
            }

            return Ok(bairro);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] BairroCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novoBairro = new Bairro
            {
                NomeBairro = dto.NomeBairro,
                CodigoCidade = dto.CodigoCidade
            };

            _context.Bairro.Add(novoBairro);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = novoBairro.Codigo },
                novoBairro
            );
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] BairroCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bairroNoBanco = _context.Bairro.FirstOrDefault(b => b.Codigo == id);

            if (bairroNoBanco == null)
            {
                return NotFound(new { message = $"Bairro de código {id} inexistente para atualização." });
            }

            bairroNoBanco.NomeBairro = dto.NomeBairro;
            bairroNoBanco.CodigoCidade = dto.CodigoCidade;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var bairro = _context.Bairro.FirstOrDefault(b => b.Codigo == id);

            if (bairro == null)
            {
                return NotFound(new { message = $"Bairro de código {id} não encontrado para exclusão." });
            }

            _context.Bairro.Remove(bairro);
            _context.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Bairro excluído com sucesso diretamente pela API!"
            });
        }
    }
}