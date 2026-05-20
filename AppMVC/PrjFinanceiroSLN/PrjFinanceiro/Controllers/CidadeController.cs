<<<<<<< HEAD
﻿namespace PrjFinanceiro.Controllers
{
    public class Cidade
    {
    }
}
=======
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using PrjFinanceiro.Models;
using System.Linq;

namespace PrjFinanceiro.Controllers
{
    public class CidadeController : Controller
    {
        private readonly AppDbContext _context;

        public CidadeController(AppDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index()
        {
            
            var lista = _context.Cidade.Include(c => c.Estado).ToList();
            ViewBag.nomesenai = "SENAI";

            return View(lista);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            
            ViewBag.Estados = _context.Estado.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(string nomecidade, string codigoibge, int codigoestado)
        {
            if (!string.IsNullOrEmpty(nomecidade))
            {
                var novaCidade = new Cidade
                {
                    NomeCidade = nomecidade,
                    CodigoIBGE = codigoibge,
                    CodigoEstado = codigoestado 
                };

                _context.Cidade.Add(novaCidade);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            
            ViewBag.Estados = _context.Estado.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == id);

            if (cidade == null)
            {
                return NotFound();
            }

     
            ViewBag.Estados = _context.Estado.ToList();
            return View(cidade);
        }

        [HttpPost]
        public IActionResult Editar(int codigo, string nomecidade, string codigoibge, int codigoestado)
        {
            var cidadeNoBanco = _context.Cidade.FirstOrDefault(a => a.Codigo == codigo);

            if (cidadeNoBanco != null)
            {
                cidadeNoBanco.NomeCidade = nomecidade;
                cidadeNoBanco.CodigoIBGE = codigoibge;
                cidadeNoBanco.CodigoEstado = codigoestado; 

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Estados = _context.Estado.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
          
            var cidade = _context.Cidade.Include(c => c.Estado).FirstOrDefault(a => a.Codigo == id);

            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        [HttpPost]
        public IActionResult ExcluirConfirmado(int codigo)
        {
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == codigo);

            if (cidade != null)
            {
                _context.Cidade.Remove(cidade);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirConfirmadoModal(int codigo)
        {
            var cidade = _context.Cidade.FirstOrDefault(a => a.Codigo == codigo);

            if (cidade != null)
            {
                _context.Cidade.Remove(cidade);
                _context.SaveChanges();
                return Json(new { success = true, message = "Excluído com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao excluir." });
        }
    }
}
>>>>>>> origin/dayvisson/dev
