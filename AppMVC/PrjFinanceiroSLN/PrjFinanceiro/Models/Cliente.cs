using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace PrjFinanceiro.Models
{
    public class Cliente
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string TipoCliente { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public int CodigoBairro { get; set; }

    }
}

