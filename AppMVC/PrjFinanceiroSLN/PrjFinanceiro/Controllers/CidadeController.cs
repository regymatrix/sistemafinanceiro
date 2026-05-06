using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necessário para usar o .Include()
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

        // Listagem de Cidades
        public IActionResult Index()
        {
            // O .Include(c => c.Estado) preenche a propriedade de navegação "Estado"
            var lista = _context.Cidade.Include(c => c.Estado).ToList();
            ViewBag.nomesenai = "SENAI";

            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            // Carrega os estados para preencher o <select> na View
            ViewBag.Estados = _context.Estado.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nomecidade, string codigoibge, int codigoestado)
        {
            if (!string.IsNullOrEmpty(nomecidade))
            {
                var novaCidade = new Cidade
                {
                    NomeCidade = nomecidade,
                    CodigoIBGE = codigoibge,
                    CodigoEstado = codigoestado // Associamos pelo ID do Estado
                };

                _context.Cidade.Add(novaCidade);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Se der erro, recarrega a lista de estados antes de voltar para a View
            ViewBag.Estados = _context.Estado.ToList();
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

            // Carrega estados para trocar o estado da cidade se necessário
            ViewBag.Estados = _context.Estado.ToList();
            return View(cidade);
        }

        [HttpPost]
        public IActionResult Editar(int codigo, string nomecidade, string codigoibge, int codigoestado)
        {
            var cidadeNoBanco = _context.Cidade.FirstOrDefault(a => a.Codigo == codigo);

            if (cidadeNoBanco != null)
            {
                cidadeNoBanco.NomeCidade = nomecidade;
                cidadeNoBanco.CodigoIBGE = codigoibge;
                cidadeNoBanco.CodigoEstado = codigoestado; // Atualiza a FK

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Estados = _context.Estado.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            // Trazemos o estado também para mostrar na tela de confirmação
            var cidade = _context.Cidade.Include(c => c.Estado).FirstOrDefault(a => a.Codigo == id);

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