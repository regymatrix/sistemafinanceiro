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

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var etnias = _context.Etnia.ToList();
            return Ok(etnias); // Status HTTP 200 OK com o JSON da lista
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var etnia = _context.Etnia.FirstOrDefault(a => a.Codigo == id);

            if (etnia == null)
            {
                return NotFound(new { message = $"Etnia com código {id} não encontrada." }); // HTTP 404
            }

            return Ok(etnia); // HTTP 200 OK
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] EtniaCreateDto dto)
        {
            // O [ApiController] já faz essa validação automaticamente, mas demonstrar em código reforça o aprendizado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 Bad Request
            }

            var novaEtnia = new Etnia
            {
                Descricao = dto.Descricao,
            };

            _context.Etnia.Add(novaEtnia);
            _context.SaveChanges();

            // Padrão REST excelente: Retorna Status 201 Created, popula o cabeçalho 'Location' com a URL de consulta 
            // e entrega o objeto recém-criado com a ID gerada pelo banco de dados.
            return CreatedAtAction(nameof(BuscarPorId), new { id = novaEtnia.Codigo }, novaEtnia);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] EtniaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var etniaNoBanco = _context.Etnia.FirstOrDefault(a => a.Codigo == id);

            if (etniaNoBanco == null)
            {
                return NotFound(new { message = $"Etnia de código {id} inexistente para atualização." });
            }

            // Mapeando dados do DTO para a entidade monitorada pelo EF
            etniaNoBanco.Descricao = dto.Descricao;

            _context.SaveChanges();

            return NoContent(); // HTTP 204 No Content (Sucesso padrão REST para atualizações)
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var etnia = _context.Etnia.FirstOrDefault(a => a.Codigo == id);

            if (etnia == null)
            {
                return NotFound(new { message = $"Etnia de código {id} não encontrada para exclusão." });
            }

            _context.Etnia.Remove(etnia);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Etnia excluída com sucesso diretamente pela API!" }); // HTTP 200
        }
    }
}
