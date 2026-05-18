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

            return View(lista); // Passa a lista para a View
        }



        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nomeCidade, string codigoIBGE, int codigoEstado)
        {
            // Criamos o objeto manualmente com os dados que vieram do formulário
            var novaCidade = new Cidade
            {
                NomeCidade = nomeCidade,
                CodigoIBGE = codigoIBGE,
                CodigoEstado = codigoEstado
            };

            if (!string.IsNullOrEmpty(nomeCidade))
            {
                _context.Cidade.Add(novaCidade);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Cidade/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            // Busca a cidade pelo código (ID)
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == id);

            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade); // Passa o objeto para a View preencher os campos
        }

        // POST: Cidade/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string nomeCidade, string codigoIBGE, int codigoEstado)
        {
            // Busca o registro existente no banco
            var cidadeNoBanco = _context.Cidade.FirstOrDefault(a => a.Codigo == codigo);

            if (cidadeNoBanco != null)
            {
                // Atualiza os atributos manualmente
                cidadeNoBanco.NomeCidade = nomeCidade;
                cidadeNoBanco.CodigoIBGE = codigoIBGE;
                cidadeNoBanco.CodigoEstado = codigoEstado;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        // GET: Cidade/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            // Busca a cidade para mostrar ao usuário o que ele está prestes a apagar
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == id);

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
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == codigo);

            if (cidade != null)
            {
                _context.Cidade.Remove(cidade);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == codigo);

            if (cidade != null)
            {
                _context.Cidade.Remove(cidade);
                _context.SaveChanges();
                return Json(new { success = true, message = "Excluído com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao excluir." });
        }




    }
}
