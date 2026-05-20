using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
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

        // LISTAR
        public IActionResult Index()
        {
            var lista = _context.Cliente.ToList();

            ViewBag.nomesenai = "SENAI";

            return View(lista);
        }

        // =========================
        // CRIAR
        // =========================

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(
            string nome,
            string dataNascimento,
            string tipoCliente,
            string cpf,
            string cnpj,
            string codigoBairro)
        {
            var novoCliente = new Cliente
            {
                Nome = nome,
                DataNascimento = dataNascimento,
                TipoCliente = tipoCliente,
                CPF = cpf,
                CNPJ = cnpj,
                CodigoBairro = codigoBairro
            };

            if (!string.IsNullOrEmpty(nome))
            {
                _context.Cliente.Add(novoCliente);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // =========================
        // EDITAR
        // =========================

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var cliente = _context.Cliente
                .FirstOrDefault(c => c.Codigo == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Editar(
            int codigo,
            string nome,
            string dataNascimento,
            string tipoCliente,
            string cpf,
            string cnpj,
            string codigoBairro)
        {
            var clienteNoBanco = _context.Cliente
                .FirstOrDefault(c => c.Codigo == codigo);

            if (clienteNoBanco != null)
            {
                clienteNoBanco.Nome = nome;
                clienteNoBanco.DataNascimento = dataNascimento;
                clienteNoBanco.TipoCliente = tipoCliente;
                clienteNoBanco.CPF = cpf;
                clienteNoBanco.CNPJ = cnpj;
                clienteNoBanco.CodigoBairro = codigoBairro;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // =========================
        // EXCLUIR
        // =========================

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var cliente = _context.Cliente
                .FirstOrDefault(c => c.Codigo == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var cliente = _context.Cliente
                .FirstOrDefault(c => c.Codigo == codigo);

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
            var cliente = _context.Cliente
                .FirstOrDefault(c => c.Codigo == codigo);

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