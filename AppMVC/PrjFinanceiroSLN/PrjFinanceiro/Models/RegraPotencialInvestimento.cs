namespace PrjFinanceiro.Models
{
    public class RegraPotencialInvestimento : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (conta == null) return 0;

          
            if (conta.Saldo > 2000m)
                return 45;

            return 0;
        }
    }
}
