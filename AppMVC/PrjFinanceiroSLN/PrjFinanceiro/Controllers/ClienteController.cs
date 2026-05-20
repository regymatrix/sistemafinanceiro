using Microsoft.AspNetCore.Mvc;
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
            var lista = _context.Cliente.ToList();
            ViewBag.nomesenai = "SENAI";

            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string Nome, DateTime DataNascimento, string TipoCliente, string CPF, string CNPJ, int CodigoBairro)
        {
            var novoCliente = new Cliente
            {
                Nome = Nome,
                DataNascimento = DataNascimento,
                TipoCliente = TipoCliente,
                CPF = CPF,
                CNPJ = CNPJ,
                CodigoBairro = CodigoBairro
            };

            if (!string.IsNullOrEmpty(Nome))
            {
                _context.Cliente.Add(novoCliente);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Cliente/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var cliente = _context.Cliente.FirstOrDefault(a => a.Codigo == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Cliente/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string Nome,DateTime DataNascimento,string TipoCliente,string CPF, string CNPJ,int CodigoBairro)
        {
            var table = _context.Cliente.FirstOrDefault(a => a.Codigo == codigo);

            if (table != null)
            {
                table.Nome = Nome;
                table.DataNascimento = DataNascimento;
                table.TipoCliente = TipoCliente;
                table.CPF = CPF;
                table.CNPJ = CNPJ;
                table.CodigoBairro = CodigoBairro;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Cliente/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var cliente = _context.Cliente.FirstOrDefault(a => a.Codigo == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Cliente/ExcluirConfirmado
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var cliente = _context.Cliente.FirstOrDefault(a => a.Codigo == codigo);

            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var cliente = _context.Cliente.FirstOrDefault(a => a.Codigo == codigo);

            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
                _context.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Excluído com sucesso!"
                });
            }

            return Json(new
            {
                success = false,
                message = "Erro ao excluir."
            });
        }
    }
}
