using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.Models
{
    public class ContaBancaria
    {
        [Key]
        public int Codigo { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoAgencia { get; set; }
        public string NumeroConta { get; set; }
        public bool StatusConta { get; set; }
        public string TipoConta { get; set; }
    }
}
