namespace PrjFinanceiro.Models
{
    public class RegraSaldoDeSeguranca : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente is null) return 0;

            if (conta.Saldo > 50000m) return +150;

            return 0;
        }
    }
}
