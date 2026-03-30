using System.ComponentModel;

namespace PrjFinanceiro.Models
{
    public class RegraPotencialDeInvestimento : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if(conta.Saldo > 2000m)
                return 45;

            return 0;
        }
    }
}
