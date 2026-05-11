using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class EtniaController : Controller
    {
        private readonly AppDbContext _context;

        public EtniaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View(_context.Etnia.ToList());

        [HttpGet]
        public IActionResult Criar() => View();

        [HttpPost]
        public IActionResult Criar(string descricao)
        {
            if (!string.IsNullOrEmpty(descricao))
            {
                _context.Etnia.Add(new Etnia { Descricao = descricao });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id) => View(_context.Etnia.FirstOrDefault(e => e.Codigo == id));

        [HttpPost]
        public IActionResult Editar(int codigo, string descricao)
        {
            var banco = _context.Etnia.FirstOrDefault(e => e.Codigo == codigo);
            if (banco != null)
            {
                banco.Descricao = descricao;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var item = _context.Etnia.FirstOrDefault(e => e.Codigo == codigo);
            if (item != null)
            {
                _context.Etnia.Remove(item);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}