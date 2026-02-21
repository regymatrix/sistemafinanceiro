using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class AgenciaController : Controller
    {
        private readonly AppDbContext _context;

        public AgenciaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Agencia.ToList();
            ViewBag.nomesenai = "SENAI";
            
            return View(lista); // Passa a lista para a View
        }


    }
}
