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

        public ContaBancariaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.ContaBancaria.Include(c => c.ClienteRel).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            ViewBag.Clientes = _context.Cliente.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(int numeroConta, int codigoCliente, int codigoAgencia, string statusConta, string tipoConta)
        {
            // Validação simples para evitar duplicar número de conta existente
            var existe = _context.ContaBancaria.Any(x => x.NumeroConta == numeroConta);
            if (existe)
            {
                ModelState.AddModelError("NumeroConta", "Este número de conta já está cadastrado.");
                ViewBag.Clientes = _context.Cliente.ToList();
                return View();
            }

            var novaConta = new ContaBancaria
            {
                NumeroConta = numeroConta,
                CodigoCliente = codigoCliente,
                CodigoAgencia = codigoAgencia,
                StatusConta = statusConta == "1", // Converte string do select para bool
                TipoConta = tipoConta
            };

            _context.ContaBancaria.Add(novaConta);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id) // O id aqui representa o NumeroConta
        {
            var conta = _context.ContaBancaria.Find(id);
            if (conta == null) return NotFound();

            ViewBag.Clientes = _context.Cliente.ToList();
            return View(conta);
        }

        [HttpPost]
        public IActionResult Editar(int numeroConta, int codigoCliente, int codigoAgencia, string statusConta, string tipoConta)
        {
            var conta = _context.ContaBancaria.Find(numeroConta);
            if (conta != null)
            {
                conta.CodigoCliente = codigoCliente;
                conta.CodigoAgencia = codigoAgencia;
                conta.StatusConta = statusConta == "1";
                conta.TipoConta = tipoConta;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Clientes = _context.Cliente.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var conta = _context.ContaBancaria.Include(c => c.ClienteRel).FirstOrDefault(x => x.NumeroConta == id);
            if (conta == null) return NotFound();
            return View(conta);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int numeroConta)
        {
            var conta = _context.ContaBancaria.Find(numeroConta);
            if (conta != null)
            {
                _context.ContaBancaria.Remove(conta);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}