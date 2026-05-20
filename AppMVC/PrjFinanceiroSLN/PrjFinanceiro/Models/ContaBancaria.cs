namespace PrjFinanceiro.Models
{
    public class ContaBancaria
    {
        public int CodigoCliente { get; set; }

        public int CodigoAgencia { get; set; }

        public int NumeroConta { get; set; }

        public bool StatusConta { get; set; }

        public string TipoConta { get; set; }
    }
}
