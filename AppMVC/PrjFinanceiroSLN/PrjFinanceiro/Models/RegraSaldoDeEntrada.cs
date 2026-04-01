namespace PrjFinanceiro.Models
{
    public class RegraSaldoDeEntrada : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (conta.Saldo >= 1000m && conta.Saldo <= 5000m)
            {
                return 30;
            }
            return 0;
        }
    }
}
