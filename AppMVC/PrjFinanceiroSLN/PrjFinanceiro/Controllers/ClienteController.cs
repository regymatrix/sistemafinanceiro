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

            return View(lista); // Passa a lista para a View
        }



        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nome, DateTime dataNascimento, string tipoCliente, string cpf, string cnpj, int codigoBairro)
        {
            // Criamos o objeto manualmente com os dados que vieram do formulário
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

        // GET: Agencia/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            // Busca a agência pelo código (ID)
            var cliente = _context.Cliente.FirstOrDefault(a => a.Codigo == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente); // Passa o objeto para a View preencher os campos
        }

        // POST: Agencia/Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string nome, DateTime dataNascimento, string tipoCliente, string cpf, string cnpj, int codigoBairro)
        {
            // Busca o registro existente no banco
            var clienteNoBanco = _context.Cliente.FirstOrDefault(a => a.Codigo == codigo);

            if (clienteNoBanco != null)
            {
                // Atualiza os atributos manualmente
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
        // GET: Agencia/Excluir/5
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            // Busca a agência para mostrar ao usuário o que ele está prestes a apagar
            var cliente = _context.Cliente.FirstOrDefault(a => a.Codigo == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Agencia/ExcluirConfirmado
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
                return Json(new { success = true, message = "Excluído com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao excluir." });
        }




    }
}
