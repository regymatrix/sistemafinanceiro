using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrjFinanceiro.Models;
using System;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class ContaBancariaController : Controller
    {
        private readonly AppDbContext _context;
        public ContaBancariaController(AppDbContext context) => _context = context;

        public IActionResult Index() => View(_context.ContaBancaria.Include(c => c.ClienteRel).ToList());

        [HttpGet]
        public IActionResult Criar()
        {
            ViewBag.Clientes = _context.Cliente.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nome, string data, string numeroConta, string tipoConta, bool statusConta, int? codigoCliente)
        {
            var nova = new ContaBancaria
            {
                Nome = nome,
                DataNascimento = Convert.ToDateTime(data),
                NumeroConta = numeroConta,
                TipoConta = tipoConta,
                StatusConta = statusConta,
                CodigoCliente = codigoCliente
            };
            _context.ContaBancaria.Add(nova);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var c = _context.ContaBancaria.Find(id);
            if (c == null) return NotFound();
            ViewBag.Clientes = _context.Cliente.ToList();
            return View(c);
        }

        [HttpPost]
        public IActionResult Editar(int codigo, string nome, string data, string numeroConta, string tipoConta, bool statusConta, int? codigoCliente)
        {
            var c = _context.ContaBancaria.Find(codigo);
            if (c != null)
            {
                c.Nome = nome;
                c.DataNascimento = Convert.ToDateTime(data);
                c.NumeroConta = numeroConta;
                c.TipoConta = tipoConta;
                c.StatusConta = statusConta;
                c.CodigoCliente = codigoCliente;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Excluir(int codigo)
        {
            var c = _context.ContaBancaria.Find(codigo);
            if (c != null) { _context.ContaBancaria.Remove(c); _context.SaveChanges(); }
            return RedirectToAction("Index");
        }
    }
}