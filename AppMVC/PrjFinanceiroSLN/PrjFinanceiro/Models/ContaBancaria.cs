using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrjFinanceiro.Models
{
    [Table("ContaBancaria")]
    public class ContaBancaria
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroConta { get; set; }
        public string TipoConta { get; set; }
        public bool StatusConta { get; set; }
        public int? CodigoCliente { get; set; }

        [ForeignKey("CodigoCliente")]
        public virtual Cliente ClienteRel { get; set; }
    }
}