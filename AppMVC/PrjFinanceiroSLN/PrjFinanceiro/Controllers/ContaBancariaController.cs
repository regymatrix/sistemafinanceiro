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

            return View(lista);
        }

        // GET CRIAR
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        // POST CRIAR
        [HttpPost]
        public IActionResult Criar(
            int codigoCliente,
            int codigoAgencia,
            int numeroConta,
            bool statusConta,
            string tipoConta)
        {
            var novaConta = new ContaBancaria
            {
                CodigoCliente = codigoCliente,
                CodigoAgencia = codigoAgencia,
                NumeroConta = numeroConta,
                StatusConta = statusConta,
                TipoConta = tipoConta
            };

            _context.ContaBancaria.Add(novaConta);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET EDITAR
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var conta = _context.ContaBancaria
                .FirstOrDefault(c => c.CodigoCliente == id);

            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        // POST EDITAR
        [HttpPost]
        public IActionResult Editar(
            int codigoCliente,
            int codigoAgencia,
            int numeroConta,
            bool statusConta,
            string tipoConta)
        {
            var contaNoBanco = _context.ContaBancaria
                .FirstOrDefault(c => c.CodigoCliente == codigoCliente);

            if (contaNoBanco != null)
            {
                contaNoBanco.CodigoAgencia = codigoAgencia;
                contaNoBanco.NumeroConta = numeroConta;
                contaNoBanco.StatusConta = statusConta;
                contaNoBanco.TipoConta = tipoConta;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET EXCLUIR
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var conta = _context.ContaBancaria
                .FirstOrDefault(c => c.CodigoCliente == id);

            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        // POST EXCLUIR
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigoCliente)
        {
            var conta = _context.ContaBancaria
                .FirstOrDefault(c => c.CodigoCliente == codigoCliente);

            if (conta != null)
            {
                _context.ContaBancaria.Remove(conta);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}