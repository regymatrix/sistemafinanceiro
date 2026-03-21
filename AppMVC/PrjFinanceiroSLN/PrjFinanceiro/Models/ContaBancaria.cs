namespace PrjFinanceiro.Models
{
    public class ContaBancaria
    {
        public string NumeroConta { get; set; }
        public decimal saldo { get; set; }
        public Cliente Cliente { get; set; }
        public Agencia Agencia { get; set; }
    }
}