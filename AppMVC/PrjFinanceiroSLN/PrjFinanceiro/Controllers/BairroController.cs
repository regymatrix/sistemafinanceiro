using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using PrjFinanceiro.Models.Services;
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

        // GET: Bairro
        public IActionResult Index()
        {
            var lista = _context.Bairro.ToList();
            return View(lista);
        }

        // GET: Bairro/Criar
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Bairro/Criar
        [HttpPost]
        public IActionResult Criar(string nomeEstado)
        {
            if (string.IsNullOrEmpty(nomeEstado))
            {
                return View();
            }

            var novoBairro = new Bairro
            {
                NomeEstado = nomeEstado
            };

            _context.Bairro.Add(novoBairro);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Bairro/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var bairro = _context.Bairro.FirstOrDefault(b => b.Codigo == id);

            if (bairro == null)
            {
                return NotFound();
            }

            return View(bairro);
        }

        // POST: Bairro/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string nomeEstado)
        {
            var bairroNoBanco = _context.Bairro.FirstOrDefault(b => b.Codigo == codigo);

            if (bairroNoBanco != null)
            {
                bairroNoBanco.NomeEstado = nomeEstado;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Bairro/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var bairro = _context.Bairro.FirstOrDefault(b => b.Codigo == id);

            if (bairro == null)
            {
                return NotFound();
            }

            return View(bairro);
        }

        // POST: Bairro/ExcluirConfirmado
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var bairro = _context.Bairro.FirstOrDefault(b => b.Codigo == codigo);

            if (bairro != null)
            {
                _context.Bairro.Remove(bairro);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: Bairro/ExcluirConfirmadoModal
        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var bairro = _context.Bairro.FirstOrDefault(b => b.Codigo == codigo);

            if (bairro != null)
            {
                _context.Bairro.Remove(bairro);
                _context.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Bairro excluído com sucesso!"
                });
            }

            return Json(new
            {
                success = false,
                message = "Erro ao excluir bairro."
            });
        }
    }
}