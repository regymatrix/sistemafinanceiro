using System.Collections.Generic;

namespace PrjFinanceiro.Models
{
    public class RegraSaldoDeSeguranca : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            
            if (conta.Saldo > 50000)
            {
                return +150;
            }
            return 0;
        }
    }
}
