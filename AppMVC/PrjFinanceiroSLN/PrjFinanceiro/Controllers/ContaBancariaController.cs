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

            return View(lista); // Passa a lista para a View
        }



        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(int codigoCliente, int codigoAgencia, string numeroConta, bool statusConta, string tipoConta)
        {
            // Criamos o objeto manualmente com os dados que vieram do formulário
            var novaContaBancaria = new ContaBancaria
            {
                CodigoCliente = codigoCliente,
                CodigoAgencia = codigoAgencia,
                NumeroConta = numeroConta,
                StatusConta = statusConta,
                TipoConta = tipoConta 

            };

            if (!string.IsNullOrEmpty(numeroConta))
            {
                _context.ContaBancaria.Add(novaContaBancaria);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: ContaBancaria/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            // Busca a agência pelo código (ID)
            var contabancaria = _context.ContaBancaria.FirstOrDefault(a => a.Codigo == id);

            if (contabancaria == null)
            {
                return NotFound();
            }

            return View(contabancaria); // Passa o objeto para a View preencher os campos
        }

        // POST: ContaBancaria/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, int codigoCliente, int codigoAgencia, string numeroConta, bool statusConta, string tipoConta)
        {
            // Busca o registro existente no banco
            var contabancariaNoBanco = _context.ContaBancaria.FirstOrDefault(a => a.Codigo == codigo);

            if (contabancariaNoBanco != null)
            {
                // Atualiza os atributos manualmente
                contabancariaNoBanco.CodigoCliente = codigoCliente;
                contabancariaNoBanco.CodigoAgencia = codigoAgencia;
                contabancariaNoBanco.NumeroConta = numeroConta;
                contabancariaNoBanco.StatusConta = statusConta;
                contabancariaNoBanco.TipoConta = tipoConta;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        // GET: ContaBancaria/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            // Busca a conta bancária para mostrar ao usuário o que ele está prestes a apagar
            var contabancaria = _context.ContaBancaria.FirstOrDefault(a => a.Codigo == id);

            if (contabancaria == null)
            {
                return NotFound();
            }

            return View(contabancaria);
        }

        // POST: ContaBancaria/ExcluirConfirmado
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var contabancaria = _context.ContaBancaria.FirstOrDefault(a => a.Codigo == codigo);

            if (contabancaria != null)
            {
                _context.ContaBancaria.Remove(contabancaria);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var contabancaria = _context.ContaBancaria.FirstOrDefault(a => a.Codigo == codigo);

            if (contabancaria != null)
            {
                _context.ContaBancaria.Remove(contabancaria);
                _context.SaveChanges();
                return Json(new { success = true, message = "Excluído com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao excluir." });
        }




    }
}
