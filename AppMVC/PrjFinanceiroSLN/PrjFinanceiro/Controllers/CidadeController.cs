using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class CidadeController : Controller
    {
        private readonly AppDbContext _context;

        public CidadeController(AppDbContext context)
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
        public IActionResult Criar(string NomeCidade, string CodigoIBGE, int CodigoEstado)
        {
            var novaCidade = new Cidade
            {
                NomeCidade = NomeCidade,
                CodigoIBGE = CodigoIBGE,
                CodigoEstado = CodigoEstado
            };

            if (!string.IsNullOrEmpty(NomeCidade))
            {
                _context.Cidade.Add(novaCidade);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == id);

            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        [HttpPost]
        public IActionResult Editar(int codigo, string NomeCidade, string CodigoIBGE, int CodigoEstado)
        {
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == codigo);

            if (cidade != null)
            {
                cidade.NomeCidade = NomeCidade;
                cidade.CodigoIBGE = CodigoIBGE;
                cidade.CodigoEstado = CodigoEstado;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == id);

            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == codigo);

            if (cidade != null)
            {
                _context.Cidade.Remove(cidade);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}