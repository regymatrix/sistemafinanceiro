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

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Cliente.Include(c => c.BairroRel).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            ViewBag.Bairros = _context.Bairro.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nome, string data, string tipoCliente, string cpf, string cnpj, int codigoBairro)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                var novo = new Cliente
                {
                    Nome = nome,
                    DataNascimento = Convert.ToDateTime(data),
                    TipoCliente = tipoCliente,
                    CPF = tipoCliente == "Física" ? cpf?.Replace(".", "").Replace("-", "") : null,
                    CNPJ = tipoCliente == "Jurídica" ? cnpj?.Replace(".", "").Replace("-", "").Replace("/", "") : null,
                    CodigoBairro = codigoBairro
                };

                _context.Cliente.Add(novo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Bairros = _context.Bairro.ToList();
            return View();
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
        public IActionResult Editar(int codigo, string nome, string data, string tipoCliente, string cpf, string cnpj, int codigoBairro)
        {
            var c = _context.Cliente.Find(codigo);
            if (c != null)
            {
                c.Nome = nome;
                c.DataNascimento = Convert.ToDateTime(data);
                c.TipoCliente = tipoCliente;
                c.CPF = tipoCliente == "Física" ? cpf?.Replace(".", "").Replace("-", "") : null;
                c.CNPJ = tipoCliente == "Jurídica" ? cnpj?.Replace(".", "").Replace("-", "").Replace("/", "") : null;
                c.CodigoBairro = codigoBairro;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Bairros = _context.Bairro.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var c = _context.Cliente.Include(x => x.BairroRel).FirstOrDefault(x => x.Codigo == id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var c = _context.Cliente.Find(codigo);
            if (c != null)
            {
                _context.Cliente.Remove(c);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}