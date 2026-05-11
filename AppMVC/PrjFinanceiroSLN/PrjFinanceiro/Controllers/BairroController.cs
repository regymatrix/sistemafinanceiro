using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class BairroController : Controller
    {
        private readonly AppDbContext _context;
        public BairroController(AppDbContext context) { _context = context; }

        public IActionResult Index() => View(_context.Bairro.Include(b => b.Cidade).ToList());

        [HttpGet]
        public IActionResult Criar()
        {
            ViewBag.Cidades = _context.Cidade.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string NomeBairro, int CodigoCidade)
        {
            var novo = new Bairro { NomeBairro = NomeBairro, CodigoCidade = CodigoCidade };
            _context.Bairro.Add(novo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var bairro = _context.Bairro.Find(id);
            if (bairro == null) return NotFound();
            ViewBag.Cidades = _context.Cidade.ToList();
            return View(bairro);
        }

        [HttpPost]
        public IActionResult Editar(int codigo, string NomeBairro, int CodigoCidade)
        {
            var b = _context.Bairro.Find(codigo);
            if (b != null)
            {
                b.NomeBairro = NomeBairro;
                b.CodigoCidade = CodigoCidade;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var b = _context.Bairro.Find(codigo);
            if (b != null)
            {
                _context.Bairro.Remove(b);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}