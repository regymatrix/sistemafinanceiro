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

        public IActionResult Index()
        {
            var lista = _context.Etnia.ToList();

            ViewBag.nomesenai = "SENAI";

            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string Descricao)
        {
            if (!string.IsNullOrEmpty(Descricao))
            {
                var novaEtnia = new Etnia
                {
                    Descricao = Descricao
                };

                _context.Etnia.Add(novaEtnia);

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var etnia = _context.Etnia.FirstOrDefault(a => a.Codigo == id);

            if (etnia == null)
            {
                return NotFound();
            }

            return View(etnia);
        }

        [HttpPost]
        public IActionResult Editar(int codigo, string Descricao)
        {
            var etnia = _context.Etnia.FirstOrDefault(a => a.Codigo == codigo);

            if (etnia != null)
            {
                etnia.Descricao = Descricao;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var etnia = _context.Etnia.FirstOrDefault(a => a.Codigo == id);

            if (etnia == null)
            {
                return NotFound();
            }

            return View(etnia);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var etnia = _context.Etnia.FirstOrDefault(a => a.Codigo == codigo);

            if (etnia != null)
            {
                _context.Etnia.Remove(etnia);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}