using Microsoft.AspNetCore.Http;
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
            return Ok(escolaridades); // Status HTTP 200 OK com o JSON da lista
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var escolaridade = _context.Escolaridade.FirstOrDefault(a => a.Codigo == id);

            if (escolaridade == null)
            {
                return NotFound(new { message = $"Escolaridade com código {id} não encontrada." }); // HTTP 404
            }

            return Ok(escolaridade); // HTTP 200 OK
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] EscolaridadeCreateDto dto)
        {
            // O [ApiController] já faz essa validação automaticamente, mas demonstrar em código reforça o aprendizado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 Bad Request
            }

            var novaEscolaridade = new Escolaridade
            {
                Descricao = dto.Descricao
            };

            _context.Escolaridade.Add(novaEscolaridade);
            _context.SaveChanges();

            // Padrão REST excelente: Retorna Status 201 Created, popula o cabeçalho 'Location' com a URL de consulta 
            // e entrega o objeto recém-criado com a ID gerada pelo banco de dados.
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

            // Mapeando dados do DTO para a entidade monitorada pelo EF
            escolaridadeNoBanco.Descricao = dto.Descricao;

            _context.SaveChanges();

            return NoContent(); // HTTP 204 No Content (Sucesso padrão REST para atualizações)
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

            return Ok(new { success = true, message = "Escolaridade excluída com sucesso diretamente pela API!" }); // HTTP 200
        }
    }
}
