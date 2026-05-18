using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using PrjFinanceiro.Models.Services;
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

        // GET: Cidade
        public IActionResult Index()
        {
            var lista = _context.Cidade.ToList();
            return View(lista);
        }

        // GET: Cidade/Criar
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Cidade/Criar
        [HttpPost]
        public IActionResult Criar(string nomeCidade)
        {
            if (string.IsNullOrEmpty(nomeCidade))
            {
                return View();
            }

            var novaCidade = new Cidade
            {
                NomeCidade = nomeCidade
            };

            _context.Cidade.Add(novaCidade);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Cidade/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var cidade = _context.Cidade.FirstOrDefault(c => c.Codigo == id);

            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // POST: Cidade/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string nomeCidade)
        {
            var cidadeNoBanco = _context.Cidade.FirstOrDefault(c => c.Codigo == codigo);

            if (cidadeNoBanco != null)
            {
                cidadeNoBanco.NomeCidade = nomeCidade;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Cidade/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var cidade = _context.Cidade.FirstOrDefault(c => c.Codigo == id);

            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // POST: Cidade/ExcluirConfirmado
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var cidade = _context.Cidade.FirstOrDefault(c => c.Codigo == codigo);

            if (cidade != null)
            {
                _context.Cidade.Remove(cidade);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: Cidade/ExcluirConfirmadoModal
        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var cidade = _context.Cidade.FirstOrDefault(c => c.Codigo == codigo);

            if (cidade != null)
            {
                _context.Cidade.Remove(cidade);
                _context.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Cidade excluída com sucesso!"
                });
            }

            return Json(new
            {
                success = false,
                message = "Erro ao excluir cidade."
            });
        }
    }
}