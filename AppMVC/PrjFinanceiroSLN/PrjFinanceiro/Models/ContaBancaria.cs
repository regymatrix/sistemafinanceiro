using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace PrjFinanceiro.Models
{
    public class ContaBancaria
    {
        public string NumeroConta { get; set; }
        public string TipoConta { get;set; }
        public decimal Saldo { get; set; }
        public bool StatusConta { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoAgencia { get; set; }
        public Cliente Cliente { get; set; }
        public Agencia Agencia { get; set; }


    }
}
