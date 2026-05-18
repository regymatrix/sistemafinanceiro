using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class EstadoController : Controller
    {
        private readonly AppDbContext _context;

        public EstadoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Estado.ToList();
            ViewBag.nomesenai = "SENAI";

            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string NomeEstado, string sigla)
        {
            var novoEstado = new Estado
            {
                NomeEstado = NomeEstado,
                Sigla = sigla
            };

            if (!string.IsNullOrEmpty(NomeEstado))
            {
                _context.Estado.Add(novoEstado);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Estado/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var estado = _context.Estado.FirstOrDefault(a => a.Codigo == id);

            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // POST: Estado/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string NomeEstado, string sigla)
        {
            var estado = _context.Estado.FirstOrDefault(a => a.Codigo == codigo);

            if (estado != null)
            {
                estado.NomeEstado = NomeEstado;
                estado.Sigla = sigla;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Estado/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var estado = _context.Estado.FirstOrDefault(a => a.Codigo == id);

            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // POST: Estado/ExcluirConfirmado
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var estado = _context.Estado.FirstOrDefault(a => a.Codigo == codigo);

            if (estado != null)
            {
                _context.Estado.Remove(estado);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var estado = _context.Estado.FirstOrDefault(a => a.Codigo == codigo);

            if (estado != null)
            {
                _context.Estado.Remove(estado);
                _context.SaveChanges();

                return Json(new { success = true, message = "Excluído com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao excluir." });
        }
    }
}