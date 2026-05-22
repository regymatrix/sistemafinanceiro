using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
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
            var lista = _context.ContaBancaria.ToList();
            ViewBag.nomesenai = "SENAI";
            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(int CodigoCliente, int CodigoAgencia, string NumeroConta, bool StatusConta, string TipoConta)
        {
            var novaConta = new ContaBancaria
            {
                CodigoCliente = CodigoCliente,
                CodigoAgencia = CodigoAgencia,
                NumeroConta = NumeroConta,
                StatusConta = StatusConta,
                TipoConta = TipoConta
            };

            if (!string.IsNullOrEmpty(NumeroConta))
            {
                _context.ContaBancaria.Add(novaConta);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var conta = _context.ContaBancaria.FirstOrDefault(a => a.CodigoCliente == id);

            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        [HttpPost]
        public IActionResult Editar(int CodigoCliente, int CodigoAgencia, string NumeroConta, bool StatusConta, string TipoConta)
        {
            var conta = _context.ContaBancaria.FirstOrDefault(a => a.CodigoCliente == CodigoCliente);

            if (conta != null)
            {
                conta.CodigoCliente = CodigoCliente;
                conta.CodigoAgencia = CodigoAgencia;
                conta.NumeroConta = NumeroConta;
                conta.StatusConta = StatusConta;
                conta.TipoConta = TipoConta;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var conta = _context.ContaBancaria.FirstOrDefault(a => a.CodigoCliente == id);

            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var conta = _context.ContaBancaria.FirstOrDefault(a => a.CodigoCliente == codigo);

            if (conta != null)
            {
                _context.ContaBancaria.Remove(conta);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}