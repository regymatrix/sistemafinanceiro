using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class EscolaridadeController : Controller
    {
        private readonly AppDbContext _context;
        public EscolaridadeController(AppDbContext context) { _context = context; }

        public IActionResult Index() => View(_context.Escolaridade.ToList());

        [HttpGet] public IActionResult Criar() => View();

        [HttpPost]
        public IActionResult Criar(string Descricao)
        {
            _context.Escolaridade.Add(new Escolaridade { Descricao = Descricao });
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id) => View(_context.Escolaridade.Find(id));

        [HttpPost]
        public IActionResult Editar(int codigo, string Descricao)
        {
            var obj = _context.Escolaridade.Find(codigo);
            if (obj != null)
            {
                obj.Descricao = Descricao;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var registro = _context.Escolaridade.FirstOrDefault(e => e.Codigo == id);

            if (registro == null) return NotFound();

            return View(registro);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var escolaridade = _context.Escolaridade.FirstOrDefault(e => e.Codigo == codigo);
            if (escolaridade != null)
            {
                _context.Escolaridade.Remove(escolaridade);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var registro = _context.Escolaridade.Find(codigo); // Ou _context.Etnia
            if (registro != null)
            {
                _context.Escolaridade.Remove(registro);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}