
using System.ComponentModel.DataAnnotations;


namespace PrjFinanceiro.Models
{
    using PrjFinanceiro.Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System;

    public class Funcionario
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cidade { get; set; }
        public string EstadoUF { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }

        // Chaves Estrangeiras
        public int CodigoEscolaridade { get; set; }
        public int CodigoEtnia { get; set; }

        [ForeignKey("CodigoEscolaridade")]
        public virtual Escolaridade Escolaridade { get; set; }

        [ForeignKey("CodigoEtnia")]
        public virtual Etnia Etnia { get; set; }
    }
}


