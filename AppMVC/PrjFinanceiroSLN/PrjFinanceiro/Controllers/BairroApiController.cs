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
            return Ok(bairros); // Status HTTP 200 OK com o JSON da lista
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var bairro = _context.Bairro.FirstOrDefault(a => a.Codigo == id);

            if (bairro == null)
            {
                return NotFound(new { message = $"Bairro com código {id} não encontrado." }); // HTTP 404
            }

            return Ok(bairro); // HTTP 200 OK
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] BairroCreateDto dto)
        {
            // O [ApiController] já faz essa validação automaticamente, mas demonstrar em código reforça o aprendizado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 Bad Request
            }

            var novoBairro = new Bairro
            {
                NomeBairro = dto.NomeBairro,
                CodigoCidade = dto.CodigoCidade
            };

            _context.Bairro.Add(novoBairro);
            _context.SaveChanges();

            // Padrão REST excelente: Retorna Status 201 Created, popula o cabeçalho 'Location' com a URL de consulta 
            // e entrega o objeto recém-criado com a ID gerada pelo banco de dados.
            return CreatedAtAction(nameof(BuscarPorId), new { id = novoBairro.Codigo }, novoBairro);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] BairroCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bairroNoBanco = _context.Bairro.FirstOrDefault(a => a.Codigo == id);

            if (bairroNoBanco == null)
            {
                return NotFound(new { message = $"Bairro de código {id} inexistente para atualização." });
            }

            // Mapeando dados do DTO para a entidade monitorada pelo EF
            bairroNoBanco.NomeBairro = dto.NomeBairro;
            bairroNoBanco.CodigoCidade = dto.CodigoCidade;

            _context.SaveChanges();

            return NoContent(); // HTTP 204 No Content (Sucesso padrão REST para atualizações)
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var bairro = _context.Bairro.FirstOrDefault(a => a.Codigo == id);

            if (bairro == null)
            {
                return NotFound(new { message = $"Bairro de código {id} não encontrado para exclusão." });
            }

            _context.Bairro.Remove(bairro);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Bairro excluído com sucesso diretamente pela API!" }); // HTTP 200
        }
    }
}
