using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class EscolaridadeController : Controller
    {
        private readonly AppDbContext _context;

        public EscolaridadeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Escolaridade.ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar() => View();

        [HttpPost]
        public IActionResult Criar(string descricao)
        {
            if (!string.IsNullOrEmpty(descricao))
            {
                var nova = new Escolaridade { Descricao = descricao };
                _context.Escolaridade.Add(nova);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var escolaridade = _context.Escolaridade.FirstOrDefault(e => e.Codigo == id);
            return escolaridade == null ? NotFound() : View(escolaridade);
        }

        [HttpPost]
        public IActionResult Editar(int codigo, string descricao)
        {
            var banco = _context.Escolaridade.FirstOrDefault(e => e.Codigo == codigo);
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
            var item = _context.Escolaridade.FirstOrDefault(e => e.Codigo == codigo);
            if (item != null)
            {
                _context.Escolaridade.Remove(item);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}