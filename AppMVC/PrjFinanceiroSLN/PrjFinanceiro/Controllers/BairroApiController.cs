using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using PrjFinanceiro.Models.Services;
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

        // GET: api/BairroApi/lista
        [HttpGet("lista")]
        public IActionResult ListarTodos()
        {
            var bairros = _context.Bairro.ToList();
            return Ok(bairros);
        }

        // GET: api/BairroApi/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var bairro = _context.Bairro.FirstOrDefault(b => b.Codigo == id);

            if (bairro == null)
            {
                return NotFound(new
                {
                    message = $"Bairro com código {id} não encontrado."
                });
            }

            return Ok(bairro);
        }

        // POST: api/BairroApi
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Bairro bairro)
        {
            if (bairro == null || string.IsNullOrEmpty(bairro.NomeEstado))
            {
                return BadRequest(new
                {
                    message = "Dados inválidos para cadastro."
                });
            }

            var novoBairro = new Bairro
            {
                NomeEstado = bairro.NomeEstado
            };

            _context.Bairro.Add(novoBairro);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = novoBairro.Codigo },
                novoBairro
            );
        }

        // PUT: api/BairroApi/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Bairro bairro)
        {
            var bairroNoBanco = _context.Bairro.FirstOrDefault(b => b.Codigo == id);

            if (bairroNoBanco == null)
            {
                return NotFound(new
                {
                    message = $"Bairro com código {id} não encontrado para atualização."
                });
            }

            bairroNoBanco.NomeEstado = bairro.NomeEstado;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/BairroApi/{id}
        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var bairro = _context.Bairro.FirstOrDefault(b => b.Codigo == id);

            if (bairro == null)
            {
                return NotFound(new
                {
                    message = $"Bairro com código {id} não encontrado para exclusão."
                });
            }

            _context.Bairro.Remove(bairro);
            _context.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Bairro excluído com sucesso!"
            });
        }
    }
}