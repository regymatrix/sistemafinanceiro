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

            return View(lista); // Passa a lista para a View
        }



        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string descricao)
        {
            // Criamos o objeto manualmente com os dados que vieram do formulário
            var novaEtnia = new Etnia
            {
                Descricao = descricao
            };

            if (!string.IsNullOrEmpty(descricao))
            {
                _context.Etnia.Add(novaEtnia);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Etnia/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            // Busca a etnia pelo código (ID)
            var etnia = _context.Etnia.FirstOrDefault(a => a.Codigo == id);

            if (etnia == null)
            {
                return NotFound();
            }

            return View(etnia); // Passa o objeto para a View preencher os campos
        }

        // POST: Etnia/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string descricao)
        {
            // Busca o registro existente no banco
            var etniaNoBanco = _context.Etnia.FirstOrDefault(a => a.Codigo == codigo);

            if (etniaNoBanco != null)
            {
                // Atualiza os atributos manualmente
                etniaNoBanco.Descricao = descricao;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        // GET: Etnia/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            // Busca a etnia para mostrar ao usuário o que ele está prestes a apagar
            var etnia = _context.Etnia.FirstOrDefault(a => a.Codigo == id);

            if (etnia == null)
            {
                return NotFound();
            }

            return View(etnia);
        }

        // POST: Etnia/ExcluirConfirmado
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

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var etnia = _context.Etnia.FirstOrDefault(a => a.Codigo == codigo);

            if (etnia != null)
            {
                _context.Etnia.Remove(etnia);
                _context.SaveChanges();
                return Json(new { success = true, message = "Excluído com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao excluir." });
        }




    }
}
