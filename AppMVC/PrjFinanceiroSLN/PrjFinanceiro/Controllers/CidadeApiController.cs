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


    }
}
