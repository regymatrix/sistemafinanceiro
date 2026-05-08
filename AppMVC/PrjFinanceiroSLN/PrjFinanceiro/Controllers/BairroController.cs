using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necessário para o .Include()
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

        // Listar Bairros
        public IActionResult Index()
        {
            // .Include(b => b.Cidade) traz os dados da cidade relacionada
            var lista = _context.Bairro.Include(b => b.Cidade).ToList();
            ViewBag.nomesenai = "SENAI";

            return View(lista);
        }

        // Criar (Exibir formulário)
        [HttpGet]
        public IActionResult Criar()
        {
            // Carrega as cidades para o Dropdown da View
            ViewBag.Cidades = _context.Cidade.ToList();
            return View();
        }

        // Criar (Salvar no banco)
        [HttpPost]
        public IActionResult Criar(string nomebairro, int codigocidade)
        {
            if (!string.IsNullOrEmpty(nomebairro))
            {
                var novoBairro = new Bairro
                {
                    NomeBairro = nomebairro,
                    CodigoCidade = codigocidade
                };

                _context.Bairro.Add(novoBairro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cidades = _context.Cidade.ToList();
            return View();
        }

        // Editar (Exibir dados)
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var bairro = _context.Bairro.FirstOrDefault(b => b.Codigo == id);

            if (bairro == null)
            {
                return NotFound();
            }

            ViewBag.Cidades = _context.Cidade.ToList();
            return View(bairro);
        }

        // Editar (Atualizar banco)
        [HttpPost]
        public IActionResult Editar(int codigo, string nomebairro, int codigocidade)
        {
            var bairroNoBanco = _context.Bairro.FirstOrDefault(b => b.Codigo == codigo);

            if (bairroNoBanco != null)
            {
                bairroNoBanco.NomeBairro = nomebairro;
                bairroNoBanco.CodigoCidade = codigocidade;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cidades = _context.Cidade.ToList();
            return View();
        }

        // Excluir (Confirmar tela)
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var bairro = _context.Bairro.Include(b => b.Cidade).FirstOrDefault(b => b.Codigo == id);

            if (bairro == null)
            {
                return NotFound();
            }

            return View(bairro);
        }

        // Excluir (Ação definitiva)
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

        // Excluir via Modal (JSON)
        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var bairro = _context.Bairro.FirstOrDefault(b => b.Codigo == codigo);

            if (bairro != null)
            {
                _context.Bairro.Remove(bairro);
                _context.SaveChanges();
                return Json(new { success = true, message = "Bairro excluído com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao excluir o bairro." });
        }
    }
}