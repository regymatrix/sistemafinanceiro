using System;
namespace PrjFinanceiro.Models
{
    public class Cliente
    {
        public int Codigo { get; set; }
        public int CodigoBairro { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Nome { get; set; }
        public string TipoCliente { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }

    }
}
