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
            return Ok(cidades); // Status HTTP 200 OK com o JSON da lista
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == id);

            if (cidade == null)
            {
                return NotFound(new { message = $"Cidade com código {id} não encontrada." }); // HTTP 404
            }

            return Ok(cidade); // HTTP 200 OK
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CidadeCreateDto dto)
        {
            // O [ApiController] já faz essa validação automaticamente, mas demonstrar em código reforça o aprendizado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 Bad Request
            }

            var novaCidade = new Cidade
            {
                NomeCidade = dto.NomeCidade,
                CodigoIBGE = dto.CodigoIBGE,
                CodigoEstado = dto.CodigoEstado
            };

            _context.Cidade.Add(novaCidade);
            _context.SaveChanges();

            // Padrão REST excelente: Retorna Status 201 Created, popula o cabeçalho 'Location' com a URL de consulta 
            // e entrega o objeto recém-criado com a ID gerada pelo banco de dados.
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
