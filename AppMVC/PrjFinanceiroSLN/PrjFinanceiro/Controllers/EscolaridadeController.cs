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
                var novaEscolaridade = new Escolaridade
                {
                    Descricao = Descricao
                };

                _context.Escolaridade.Add(novaEscolaridade);

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var escolaridade = _context.Escolaridade.FirstOrDefault(a => a.Codigo == id);

            if (escolaridade == null)
            {
                return NotFound();
            }

            return View(escolaridade);
        }

        [HttpPost]
        public IActionResult Editar(int codigo, string Descricao)
        {
            var escolaridade = _context.Escolaridade.FirstOrDefault(a => a.Codigo == codigo);

            if (escolaridade != null)
            {
                escolaridade.Descricao = Descricao;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var escolaridade = _context.Escolaridade.FirstOrDefault(a => a.Codigo == id);

            if (escolaridade == null)
            {
                return NotFound();
            }

            return View(escolaridade);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var escolaridade = _context.Escolaridade.FirstOrDefault(a => a.Codigo == codigo);

            if (escolaridade != null)
            {
                _context.Escolaridade.Remove(escolaridade);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}