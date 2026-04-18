using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;

namespace PrjFinanceiro.Controllers
{
    public class FuncionarioController : Controller
    {

        private readonly AppDbContext _context;

        public FuncionarioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Funcionario.ToList();

            return View(lista);
        }
    }
}
