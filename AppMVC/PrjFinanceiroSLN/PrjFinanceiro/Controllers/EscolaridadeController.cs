using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class EscolaridadeController : Controller
    {
        private readonly AppDbContext _context;

        // CONSTRUTOR
        public EscolaridadeController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // LISTAR
        // =========================
        public IActionResult Index()
        {
            var lista = _context.Escolaridade.ToList();

            ViewBag.nomesenai = "SENAI";

            return View(lista);
        }

        // =========================
        // GET: Criar
        // =========================
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        // =========================
        // POST: Criar
        // =========================
        [HttpPost]
        public IActionResult Criar(string descricao)
        {
            var novaEscolaridade = new Escolaridade
            {
                Descricao = descricao
            };

            if (!string.IsNullOrEmpty(descricao))
            {
                _context.Escolaridade.Add(novaEscolaridade);

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // =========================
        // GET: Editar
        // =========================
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var escolaridade = _context.Escolaridade
                .FirstOrDefault(e => e.Codigo == id);

            if (escolaridade == null)
            {
                return NotFound();
            }

            return View(escolaridade);
        }

        // =========================
        // POST: Editar
        // =========================
        [HttpPost]
        public IActionResult Editar(int codigo, string descricao)
        {
            var escolaridadeNoBanco = _context.Escolaridade
                .FirstOrDefault(e => e.Codigo == codigo);

            if (escolaridadeNoBanco != null)
            {
                escolaridadeNoBanco.Descricao = descricao;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // =========================
        // GET: Excluir
        // =========================
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var escolaridade = _context.Escolaridade
                .FirstOrDefault(e => e.Codigo == id);

            if (escolaridade == null)
            {
                return NotFound();
            }

            return View(escolaridade);
        }

        // =========================
        // POST: ExcluirConfirmado
        // =========================
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var escolaridade = _context.Escolaridade
                .FirstOrDefault(e => e.Codigo == codigo);

            if (escolaridade != null)
            {
                _context.Escolaridade.Remove(escolaridade);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // =========================
        // EXCLUIR VIA MODAL
        // =========================
        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var escolaridade = _context.Escolaridade
                .FirstOrDefault(e => e.Codigo == codigo);

            if (escolaridade != null)
            {
                _context.Escolaridade.Remove(escolaridade);

                _context.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Excluído com sucesso!"
                });
            }

            return Json(new
            {
                success = false,
                message = "Erro ao excluir."
            });
        }
    }
}