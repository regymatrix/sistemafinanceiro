using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrjFinanceiro.Models;
using System;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AppDbContext _context;
        public ClienteController(AppDbContext context) => _context = context;

        public IActionResult Index() => View(_context.Cliente.Include(c => c.BairroRel).ToList());

        [HttpGet]
        public IActionResult Criar()
        {
            ViewBag.Bairros = _context.Bairro.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nome, string data, string tipo, string cpf, string cnpj, int? codigoBairro)
        {
            var novo = new Cliente
            {
                Nome = nome,
                DataNascimento = Convert.ToDateTime(data),
                Tipo = tipo,
                CPF = cpf?.Replace(".", "").Replace("-", "") ?? "",
                CNPJ = cnpj?.Replace(".", "").Replace("-", "").Replace("/", "") ?? "",
                CodigoBairro = codigoBairro
            };
            _context.Cliente.Add(novo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var c = _context.Cliente.Find(id);
            if (c == null) return NotFound();
            ViewBag.Bairros = _context.Bairro.ToList();
            return View(c);
        }

        [HttpPost]
        public IActionResult Editar(int codigo, string nome, string data, string tipo, string cpf, string cnpj, int? codigoBairro)
        {
            var c = _context.Cliente.Find(codigo);
            if (c != null)
            {
                c.Nome = nome;
                c.DataNascimento = Convert.ToDateTime(data);
                c.Tipo = tipo;
                c.CPF = cpf?.Replace(".", "").Replace("-", "") ?? "";
                c.CNPJ = cnpj?.Replace(".", "").Replace("-", "").Replace("/", "") ?? "";
                c.CodigoBairro = codigoBairro;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Excluir(int codigo)
        {
            var c = _context.Cliente.Find(codigo);
            if (c != null) { _context.Cliente.Remove(c); _context.SaveChanges(); }
            return RedirectToAction("Index");
        }
    }
}