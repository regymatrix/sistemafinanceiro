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

        // GET: Escolaridade/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            // Busca a agência pelo código (ID)
            var escolaridade = _context.Escolaridade.FirstOrDefault(a => a.Codigo == id);

            if (escolaridade == null)
            {
                return NotFound();
            }

            return View(escolaridade); // Passa o objeto para a View preencher os campos
        }

        // POST: Escolaridade/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string descricao)
        {
            // Busca o registro existente no banco
            var escolaridadeNoBanco = _context.Escolaridade.FirstOrDefault(a => a.Codigo == codigo);

            if (escolaridadeNoBanco != null)
            {
                // Atualiza os atributos manualmente
                escolaridadeNoBanco.Descricao = descricao;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        // GET: Escolaridade/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            // Busca a Escolaridade para mostrar ao usuário o que ele está prestes a apagar
            var escolaridade = _context.Escolaridade.FirstOrDefault(a => a.Codigo == id);

            if (escolaridade == null)
            {
                return NotFound();
            }

            return View(escolaridade);
        }

        // POST: Escolaridade/ExcluirConfirmado
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

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var escolaridade = _context.Escolaridade.FirstOrDefault(a => a.Codigo == codigo);

            if (escolaridade != null)
            {
                _context.Escolaridade.Remove(escolaridade);
                _context.SaveChanges();
                return Json(new { success = true, message = "Excluído com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao excluir." });
        }
    }
}
