using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class BairroController : Controller
    {
        private readonly AppDbContext _context;

        public BairroController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Cidade.ToList();

            ViewBag.nomesenai = "SENAI";

            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string NomeBairro, int CodigoCidade)
        {
            var novoBairro = new Bairro
            {
                NomeBairro = NomeBairro,
                CodigoCidade = CodigoCidade
            };

            if (!string.IsNullOrEmpty(NomeBairro))
            {
                _context.Bairro.Add(novoBairro);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var bairro = _context.Bairro.FirstOrDefault(a => a.Codigo == id);

            if (bairro == null)
            {
                return NotFound();
            }

            return View(bairro);
        }

        [HttpPost]
        public IActionResult Editar(int codigo, string NomeBairro, int CodigoCidade)
        {
            var bairro = _context.Bairro.FirstOrDefault(a => a.Codigo == codigo);

            if (bairro != null)
            {
                bairro.NomeBairro = NomeBairro;
                bairro.CodigoCidade = CodigoCidade;
                

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var bairro = _context.Bairro.FirstOrDefault(a => a.Codigo == id);

            if (bairro == null)
            {
                return NotFound();
            }

            return View(bairro);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var bairro = _context.Bairro.FirstOrDefault(a => a.Codigo == codigo);

            if (bairro != null)
            {
                _context.Bairro.Remove(bairro);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}