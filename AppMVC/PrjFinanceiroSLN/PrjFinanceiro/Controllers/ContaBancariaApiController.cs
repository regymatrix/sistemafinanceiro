using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.DTOs;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaBancariaApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContaBancariaApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var contabancarias = _context.ContaBancaria.ToList();
            return Ok(contabancarias); // Status HTTP 200 OK com o JSON da lista
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var contabancaria = _context.ContaBancaria.FirstOrDefault(a => a.Codigo == id);

            if (contabancaria == null)
            {
                return NotFound(new { message = $"Conta Bancaria com código {id} não encontrada." }); // HTTP 404
            }

            return Ok(contabancaria); // HTTP 200 OK
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] ContaBancariaCreateDto dto)
        {
            // O [ApiController] já faz essa validação automaticamente, mas demonstrar em código reforça o aprendizado
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400 Bad Request
            }

            var novaContaBancaria = new ContaBancaria
            {
                CodigoCliente = dto.CodigoCliente,
                CodigoAgencia = dto.CodigoAgencia,
                NumeroConta = dto.NumeroConta,
                StatusConta = dto.StatusConta,
                TipoConta = dto.TipoConta
            };

            _context.ContaBancaria.Add(novaContaBancaria);
            _context.SaveChanges();

            // Padrão REST excelente: Retorna Status 201 Created, popula o cabeçalho 'Location' com a URL de consulta 
            // e entrega o objeto recém-criado com a ID gerada pelo banco de dados.
            return CreatedAtAction(nameof(BuscarPorId), new { id = novaContaBancaria.Codigo }, novaContaBancaria);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ContaBancariaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contabancariaNoBanco = _context.ContaBancaria.FirstOrDefault(a => a.Codigo == id);

            if (contabancariaNoBanco == null)
            {
                return NotFound(new { message = $"Conta Bancaria de código {id} inexistente para atualização." });
            }

            // Mapeando dados do DTO para a entidade monitorada pelo EF
            contabancariaNoBanco.CodigoCliente = dto.CodigoCliente;
            contabancariaNoBanco.CodigoAgencia = dto.CodigoAgencia;
            contabancariaNoBanco.NumeroConta = dto.NumeroConta;
            contabancariaNoBanco.StatusConta = dto.StatusConta;
            contabancariaNoBanco.TipoConta = dto.TipoConta;

            _context.SaveChanges();

            return NoContent(); // HTTP 204 No Content (Sucesso padrão REST para atualizações)
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var contabancaria = _context.ContaBancaria.FirstOrDefault(a => a.Codigo == id);

            if (contabancaria == null)
            {
                return NotFound(new { message = $"Conta Bancaria de código {id} não encontrada para exclusão." });
            }

            _context.ContaBancaria.Remove(contabancaria);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Conta Bancaria excluída com sucesso diretamente pela API!" }); // HTTP 200
        }
    }
}
