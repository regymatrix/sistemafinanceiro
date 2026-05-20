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

        // LISTAR
        public IActionResult Index()
        {
            var lista = _context.ContaBancaria.ToList();

            ViewBag.nomesenai = "SENAI";

            return View(lista);
        }

        // GET: ContaBancaria/Criar
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        // POST: ContaBancaria/Criar
        [HttpPost]
        public IActionResult Criar(
            string NumeroConta,
            decimal StatusConta,
            Cliente ContaCliente,
            Agencia CodigoAgencia,
            string TipoConta)
        {
            var novaConta = new ContaBancaria
            {
                NumeroConta = NumeroConta,
                StatusConta = StatusConta,
                ContaCliente = ContaCliente,
                CodigoAgencia = CodigoAgencia,
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

        // GET: ContaBancaria/Editar/5
        [HttpGet]
        public IActionResult Editar(string id)
        {
            var conta = _context.ContaBancaria
                .FirstOrDefault(c => c.NumeroConta == id);

            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        // POST: ContaBancaria/Editar
        [HttpPost]
        public IActionResult Editar(
            string NumeroConta,
            decimal StatusConta,
            Cliente ContaCliente,
            Agencia CodigoAgencia,
            string TipoConta)
        {
            var conta = _context.ContaBancaria
                .FirstOrDefault(c => c.NumeroConta == NumeroConta);

            if (conta != null)
            {
                conta.StatusConta = StatusConta;
                conta.ContaCliente = ContaCliente;
                conta.CodigoAgencia = CodigoAgencia;
                conta.TipoConta = TipoConta;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: ContaBancaria/Excluir/5
        [HttpGet]
        public IActionResult Excluir(string id)
        {
            var conta = _context.ContaBancaria
                .FirstOrDefault(c => c.NumeroConta == id);

            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        // POST: ContaBancaria/ExcluirConfirmado
        [HttpPost]
        public IActionResult ExcluirConfirmado(string NumeroConta)
        {
            var conta = _context.ContaBancaria
                .FirstOrDefault(c => c.NumeroConta == NumeroConta);

            if (conta != null)
            {
                _context.ContaBancaria.Remove(conta);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // EXCLUIR VIA MODAL
        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(string NumeroConta)
        {
            var conta = _context.ContaBancaria
                .FirstOrDefault(c => c.NumeroConta == NumeroConta);

            if (conta != null)
            {
                _context.ContaBancaria.Remove(conta);
                _context.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Conta bancária excluída com sucesso!"
                });
            }

            return Json(new
            {
                success = false,
                message = "Erro ao excluir conta bancária."
            });
        }
    }
}