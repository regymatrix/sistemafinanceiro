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
            var lista = _context.Bairro.ToList();
            ViewBag.nomesenai = "SENAI";

            return View(lista); // Passa a lista para a View
        }



        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nomeBairro, int codigoCidade)
        {
            // Criamos o objeto manualmente com os dados que vieram do formulário
            var novoBairro = new Bairro
            {
                NomeBairro = nomeBairro,
                CodigoCidade = codigoCidade
            };

            if (!string.IsNullOrEmpty(nomeBairro))
            {
                _context.Bairro.Add(novoBairro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Bairro/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            // Busca a bairro pelo código (ID)
            var bairro = _context.Bairro.FirstOrDefault(a => a.Codigo == id);

            if (bairro == null)
            {
                return NotFound();
            }

            return View(bairro); // Passa o objeto para a View preencher os campos
        }

        // POST: Bairro/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string nomeBairro, int codigoCidade)
        {
            // Busca o registro existente no banco
            var bairroNoBanco = _context.Bairro.FirstOrDefault(a => a.Codigo == codigo);

            if (bairroNoBanco != null)
            {
                // Atualiza os atributos manualmente
                bairroNoBanco.NomeBairro = nomeBairro;
                bairroNoBanco.CodigoCidade = codigoCidade;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        // GET: Bairro/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            // Busca a bairro para mostrar ao usuário o que ele está prestes a apagar
            var bairro = _context.Bairro.FirstOrDefault(a => a.Codigo == id);

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
            var bairro = _context.Bairro.FirstOrDefault(a => a.Codigo == codigo);

            if (bairro != null)
            {
                _context.Bairro.Remove(bairro);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var bairro = _context.Bairro.FirstOrDefault(a => a.Codigo == codigo);

            if (bairro != null)
            {
                _context.Bairro.Remove(bairro);
                _context.SaveChanges();
                return Json(new { success = true, message = "Excluído com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao excluir." });
        }




    }
}
