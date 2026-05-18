using Microsoft.AspNetCore.Http;
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

        [HttpGet]

        public IActionResult ListarTodos()
        {
            var cidades = _context.Cidade.ToList();
            return Ok(cidades);

        }

        [HttpGet("{id}")]

        public IActionResult BuscarPorId(int id) 
        {

            var cidades = _context.Cidade.FirstOrDefault(a => a.Codigo == id);
            if (cidades == null) 
            { 
                return NotFound(new { message = $"Cidade com código {id} não encontrada!"});
            
            }
            return Ok(cidades);

        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CidadeCreateDto dto)
        {
            if ( !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novaCidade = new Cidade
            {
                NomeCidade = dto.NomeCidade,
                CodigoIBGE = dto.CodigoIBGE,
                CodigoEstado = dto.CodigoEstado,
            };

            _context.Cidade.Add(novaCidade);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarPorId), new { id = novaCidade.Codigo }, novaCidade);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] CidadeCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cidadeNoBanco = _context.Cidade.FirstOrDefault(a => a.Codigo == id);

            if (cidadeNoBanco == null)
            {
                return NotFound(new { message = $"Cidade de código {id} inexistente para atualização." });
            }

            // Mapeando dados do DTO para a entidade monitorada pelo EF
            cidadeNoBanco.NomeCidade = dto.NomeCidade;
            cidadeNoBanco.CodigoIBGE = dto.CodigoIBGE;
            cidadeNoBanco.CodigoEstado = dto.CodigoEstado;

            _context.SaveChanges();

            return NoContent(); // HTTP 204 No Content (Sucesso padrão REST para atualizações)
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == id);

            if (cidade == null)
            {
                return NotFound(new { message = $"Cidade de código {id} não encontrada para exclusão." });
            }

            _context.Cidade.Remove(cidade);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Cidade excluída com sucesso diretamente pela API!" }); // HTTP 200
        }


    }
}
