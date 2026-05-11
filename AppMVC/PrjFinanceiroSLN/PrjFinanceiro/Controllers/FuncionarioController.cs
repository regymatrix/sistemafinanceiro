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

        public IActionResult Index()
        {
            var lista = _context.Funcionario
                .Include(f => f.Escolaridade)
                .Include(f => f.Etnia)
                .Include(f => f.CidadeRel)
                .Include(f => f.BairroRel)
                .ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            CarregarCombos();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nome, string data, string cpf, string telefone, string estadoUF, int codigoCidade, int codigoBairro, int codigoEscolaridade, int codigoEtnia)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                var novo = new Funcionario
                {
                    Nome = nome,
                    DataNascimento = Convert.ToDateTime(data),
                    CPF = cpf.Replace(".", "").Replace("-", ""),
                    Telefone = telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""),
                    EstadoUF = estadoUF,
                    CodigoCidade = codigoCidade,
                    CodigoBairro = codigoBairro,
                    CodigoEscolaridade = codigoEscolaridade,
                    CodigoEtnia = codigoEtnia
                };

                _context.Funcionario.Add(novo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            CarregarCombos();
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var f = _context.Funcionario.Find(id);
            if (f == null) return NotFound();
            CarregarCombos();
            return View(f);
        }

        [HttpPost]
        public IActionResult Editar(int codigo, string nome, string data, string cpf, string telefone, string estadoUF, int codigoCidade, int codigoBairro, int codigoEscolaridade, int codigoEtnia)
        {
            var f = _context.Funcionario.Find(codigo);
            if (f != null)
            {
                f.Nome = nome;
                f.DataNascimento = Convert.ToDateTime(data);
                f.CPF = cpf.Replace(".", "").Replace("-", "");
                f.Telefone = telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                f.EstadoUF = estadoUF;
                f.CodigoCidade = codigoCidade;
                f.CodigoBairro = codigoBairro;
                f.CodigoEscolaridade = codigoEscolaridade;
                f.CodigoEtnia = codigoEtnia;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            CarregarCombos();
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var f = _context.Funcionario
                .Include(f => f.Escolaridade).Include(f => f.Etnia)
                .Include(f => f.CidadeRel).Include(f => f.BairroRel)
                .FirstOrDefault(x => x.Codigo == id);
            return View(f);
        }

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

        private void CarregarCombos()
        {
            ViewBag.Escolaridades = _context.Escolaridade.ToList();
            ViewBag.Etnias = _context.Etnia.ToList();
            ViewBag.Estados = _context.Estado.ToList();
            ViewBag.Cidades = _context.Cidade.ToList();
            ViewBag.Bairros = _context.Bairro.ToList();
        }
    }
}