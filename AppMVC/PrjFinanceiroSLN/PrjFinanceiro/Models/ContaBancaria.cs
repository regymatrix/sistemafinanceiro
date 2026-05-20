namespace PrjFinanceiro.Models
{
    public class ContaBancaria
    {
        public string NumeroConta { get; set; }
        public decimal StatusConta { get; set; }
        public Cliente ContaCliente { get; set; }
        public Agencia CodigoAgencia { get; set; }
        public string TipoConta { get; set; }
    }
}
