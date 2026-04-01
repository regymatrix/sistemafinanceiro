namespace PrjFinanceiro.Models
{
    public class RegraSaldoSeguranca : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente.Saldo > 50000)
                return 150;

            return 0;
        }
    }
}