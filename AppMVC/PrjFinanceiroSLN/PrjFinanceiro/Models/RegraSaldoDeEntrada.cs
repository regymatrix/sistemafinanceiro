
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PrjFinanceiro.Models
{
    public class RegraSaldoDeEntrada : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente is null) return 0;

            if (conta.Saldo > 1000 & conta.Saldo < 5000)
            {
                return +30;
            }
            return 0;
        }
    }
}
