using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class CidadeController : Controller
    {
        private readonly AppDbContext _context;

        public CidadeController(AppDbContext context) { _context = context; }

        public IActionResult Index()
        {
            // Carrega a cidade junto com o objeto Estado para exibir o nome na lista
            var lista = _context.Cidade.Include(c => c.Estado).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            // Envia a lista de estados para o dropdown
            ViewBag.Estados = _context.Estado.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string NomeCidade, string CodigoIBGE, int CodigoEstado)
        {
            var novaCidade = new Cidade
            {
                NomeCidade = NomeCidade,
                CodigoIBGE = CodigoIBGE,
                CodigoEstado = CodigoEstado
            };
            _context.Cidade.Add(novaCidade);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var cidade = _context.Cidade.Find(id);
            if (cidade == null) return NotFound();
            ViewBag.Estados = _context.Estado.ToList();
            return View(cidade);
        }

        [HttpPost]
        public IActionResult Editar(int codigo, string NomeCidade, string CodigoIBGE, int CodigoEstado)
        {
            var cid = _context.Cidade.Find(codigo);
            if (cid != null)
            {
                cid.NomeCidade = NomeCidade;
                cid.CodigoIBGE = CodigoIBGE;
                cid.CodigoEstado = CodigoEstado;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var cid = _context.Cidade.Find(codigo);
            if (cid != null)
            {
                _context.Cidade.Remove(cid);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}