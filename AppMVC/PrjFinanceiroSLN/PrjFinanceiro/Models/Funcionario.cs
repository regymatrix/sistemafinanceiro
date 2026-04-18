using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrjFinanceiro.Models
{
    public class Funcionario
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Cidade { get; set; }
        public string EstadoUF { get; set; }

        public string CPF { get; set; }
        public string Telefone { get; set; }

        public DateTime DataNascimento { get; set; }
    }
}
