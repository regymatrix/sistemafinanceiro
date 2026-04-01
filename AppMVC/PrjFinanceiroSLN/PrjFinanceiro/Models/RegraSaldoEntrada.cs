namespace PrjFinanceiro.Models
{
    public class RegraSaldoEntrada : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (conta == null) return 0;

            if (conta.Saldo >= 1000m && conta.Saldo <= 5000m)
                return 30;

            return 0;
        }
    }
}
