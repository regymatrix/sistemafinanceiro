using Microsoft.AspNetCore.Mvc;
using PrjFinanceiro.Models;
using System;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly AppDbContext _context;

        public FuncionarioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Funcionario.ToList();
            ViewBag.nomesenai = "SENAI";
            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(
            string nome,
            string cidade,
            string estadoUF,
            string data,
            string cpf,
            string telefone)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                var novoFuncionario = new Funcionario
                {
                    Nome = nome,
                    Cidade = cidade,
                    EstadoUF = estadoUF,
                    CPF = cpf,
                    Telefone = telefone,
                    DataNascimento = Convert.ToDateTime(data)
                };

                _context.Funcionario.Add(novoFuncionario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var funcionario = _context.Funcionario.FirstOrDefault(a => a.Codigo == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        [HttpPost]
        public IActionResult Editar(
            int codigo,
            string nome,
            string cidade,
            string estadoUF,
            string data,
            string cpf,
            string telefone)
        {
            var funcionarioNoBanco = _context.Funcionario.FirstOrDefault(a => a.Codigo == codigo);

            if (funcionarioNoBanco != null)
            {
                funcionarioNoBanco.Nome = nome;
                funcionarioNoBanco.Cidade = cidade;
                funcionarioNoBanco.EstadoUF = estadoUF;
                funcionarioNoBanco.CPF = cpf;
                funcionarioNoBanco.Telefone = telefone;
                funcionarioNoBanco.DataNascimento = Convert.ToDateTime(data);

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var funcionario = _context.Funcionario
                .FirstOrDefault(a => a.Codigo == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var funcionario = _context.Funcionario
                .FirstOrDefault(a => a.Codigo == codigo);

            if (funcionario != null)
            {
                _context.Funcionario.Remove(funcionario);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}