using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CidadeApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CidadeApi
        [HttpGet]
        public IActionResult ListarTodos()
        {
            var cidades = _context.Cidade.ToList();

            return Ok(cidades);
        }

        // GET: api/CidadeApi/5
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == id);

            if (cidade == null)
            {
                return NotFound(new
                {
                    message = $"Cidade com código {id} não encontrada."
                });
            }

            return Ok(cidade);
        }

        // POST: api/CidadeApi
        [HttpPost]
        public IActionResult Cadastrar([FromBody] CidadeCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novaCidade = new Cidade
            {
                NomeCidade = dto.NomeCidade,
                CodigoIBGE = dto.CodigoIBGE,
                CodigoEstado = dto.CodigoEstado
            };

            _context.Cidade.Add(novaCidade);

            _context.SaveChanges();

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = novaCidade.Codigo },
                novaCidade
            );
        }

        // PUT: api/CidadeApi/5
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] CidadeCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cidadeNoBanco = _context.Cidade
                .FirstOrDefault(a => a.Codigo == id);

            if (cidadeNoBanco == null)
            {
                return NotFound(new
                {
                    message = $"Cidade com código {id} não encontrada."
                });
            }

            cidadeNoBanco.NomeCidade = dto.NomeCidade;
            cidadeNoBanco.CodigoIBGE = dto.CodigoIBGE;
            cidadeNoBanco.CodigoEstado = dto.CodigoEstado;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/CidadeApi/5
        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var cidade = _context.Cidade
                .FirstOrDefault(a => a.Codigo == id);

            if (cidade == null)
            {
                return NotFound(new
                {
                    message = $"Cidade com código {id} não encontrada."
                });
            }

            _context.Cidade.Remove(cidade);

            _context.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Cidade excluída com sucesso!"
            });
        }
    }
}