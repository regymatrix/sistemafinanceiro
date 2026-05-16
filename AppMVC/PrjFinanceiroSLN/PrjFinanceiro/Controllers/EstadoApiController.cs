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

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var estados = _context.Estado.ToList();
            return Ok(estados); // Status HTTP 200 OK com o JSON da lista
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var estado = _context.Estado.FirstOrDefault(a => a.Codigo == id);

            if (estado == null)
            {
                return NotFound(new { message = $"Agência com código {id} não encontrada." }); // HTTP 404
            }

            return Ok(estado); // HTTP 200 OK
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] EstadoCreateDto dto)
        {
            // O [ApiController] já faz essa validação automaticamente, mas demonstrar em código reforça o aprendizado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 Bad Request
            }

            var novoEstado = new Estado
            {
                NomeEstado = dto.NomeEstado,
                Sigla = dto.Sigla
            };

            _context.Estado.Add(novoEstado);
            _context.SaveChanges();

            // Padrão REST excelente: Retorna Status 201 Created, popula o cabeçalho 'Location' com a URL de consulta 
            // e entrega o objeto recém-criado com a ID gerada pelo banco de dados.
            return CreatedAtAction(nameof(BuscarPorId), new { id = novoEstado.Codigo }, novoEstado);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] EstadoCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var estadoNoBanco = _context.Estado.FirstOrDefault(a => a.Codigo == id);

            if (estadoNoBanco == null)
            {
                return NotFound(new { message = $"Agência de código {id} inexistente para atualização." });
            }

            // Mapeando dados do DTO para a entidade monitorada pelo EF
            estadoNoBanco.NomeEstado = dto.NomeEstado;
            estadoNoBanco.Sigla = dto.Sigla;

            _context.SaveChanges();

            return NoContent(); // HTTP 204 No Content (Sucesso padrão REST para atualizações)
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var estado = _context.Estado.FirstOrDefault(a => a.Codigo == id);

            if (estado == null)
            {
                return NotFound(new { message = $"Estado de código {id} não encontrada para exclusão." });
            }

            _context.Estado.Remove(estado);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Estado excluído com sucesso diretamente pela API!" }); // HTTP 200
        }
    }
}
