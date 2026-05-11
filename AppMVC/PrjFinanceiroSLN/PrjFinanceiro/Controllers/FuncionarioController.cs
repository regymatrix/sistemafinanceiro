using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // Listagem
        public IActionResult Index()
        {
            var lista = _context.Funcionario
                .Include(f => f.Escolaridade)
                .Include(f => f.Etnia)
                .ToList();

            ViewBag.nomesenai = "SENAI";
            return View(lista);
        }

        // GET: Criar
        [HttpGet]
        public IActionResult Criar()
        {
            CarregarCombos();
            return View();
        }

        // POST: Criar
        [HttpPost]
        public IActionResult Criar(string nome, string cidade, string estadoUF, string data, string cpf, string telefone, int codigoEscolaridade, int codigoEtnia)
        {
            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(data))
            {
                try
                {
                    var novoFuncionario = new Funcionario
                    {
                        Nome = nome,
                        DataNascimento = Convert.ToDateTime(data),
                        Cidade = cidade,
                        EstadoUF = estadoUF,
                        CPF = cpf,
                        Telefone = telefone,
                        CodigoEscolaridade = codigoEscolaridade,
                        CodigoEtnia = codigoEtnia
                    };

                    _context.Funcionario.Add(novoFuncionario);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Erro ao converter a data. Verifique o formato.");
                }
            }

            CarregarCombos();
            return View();
        }

        // GET: Editar
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var funcionario = _context.Funcionario.Find(id);
            if (funcionario == null) return NotFound();

            CarregarCombos();
            return View(funcionario);
        }

        // POST: Editar
        [HttpPost]
        public IActionResult Editar(int codigo, string nome, string cidade, string estadoUF, string data, string cpf, string telefone, int codigoEscolaridade, int codigoEtnia)
        {
            var banco = _context.Funcionario.Find(codigo);

            if (banco != null)
            {
                try
                {
                    banco.Nome = nome;
                    banco.DataNascimento = Convert.ToDateTime(data);
                    banco.Cidade = cidade;
                    banco.EstadoUF = estadoUF;
                    banco.CPF = cpf;
                    banco.Telefone = telefone;
                    banco.CodigoEscolaridade = codigoEscolaridade;
                    banco.CodigoEtnia = codigoEtnia;

                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Erro ao salvar alterações.");
                }
            }

            CarregarCombos();
            return View(banco);
        }

        // GET: Excluir
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var f = _context.Funcionario
                .Include(f => f.Escolaridade)
                .Include(f => f.Etnia)
                .FirstOrDefault(a => a.Codigo == id);

            return f == null ? NotFound() : View(f);
        }

        // POST: Excluir
        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var f = _context.Funcionario.Find(codigo);
            if (f != null)
            {
                _context.Funcionario.Remove(f);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Método auxiliar para não repetir código de carregar as ViewBags
        private void CarregarCombos()
        {
            ViewBag.Escolaridades = _context.Escolaridade.OrderBy(e => e.Descricao).ToList();
            ViewBag.Etnias = _context.Etnia.OrderBy(e => e.Descricao).ToList();
        }
    }
}