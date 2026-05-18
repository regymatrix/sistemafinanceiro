using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstadoApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public IActionResult ListarTodos()
        {
            var estados = _context.Estado.ToList();
            return Ok(estados);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var estado = _context.Estado.FirstOrDefault(e => e.Codigo == id);

            if (estado == null)
            {
                return NotFound(new { message = $"Estado com código {id} não encontrado." });
            }

            return Ok(estado);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] EstadoCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novoEstado = new Estado
            {
                NomeEstado = dto.NomeEstado,
                Sigla = dto.Sigla,
                Cidade = dto.Cidade
            };

            _context.Estado.Add(novoEstado);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = novoEstado.Codigo },
                novoEstado
            );
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] EstadoCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var estadoNoBanco = _context.Estado.FirstOrDefault(e => e.Codigo == id);

            if (estadoNoBanco == null)
            {
                return NotFound(new { message = $"Estado de código {id} não encontrado para atualização." });
            }

            estadoNoBanco.NomeEstado = dto.NomeEstado;
            estadoNoBanco.Sigla = dto.Sigla;
            estadoNoBanco.Cidade = dto.Cidade;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var estado = _context.Estado.FirstOrDefault(e => e.Codigo == id);

            if (estado == null)
            {
                return NotFound(new { message = $"Estado de código {id} não encontrado para exclusão." });
            }

            _context.Estado.Remove(estado);
            _context.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "Estado excluído com sucesso diretamente pela API!"
            });
        }
    }
}