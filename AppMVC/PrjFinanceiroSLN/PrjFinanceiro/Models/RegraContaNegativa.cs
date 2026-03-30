using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PrjFinanceiro.Models
{
    public class RegraContaNegativa : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente is null) return 0;
          

            if (conta.Saldo < 0)
            {
                return -200;
            }
            return 0;
        }
    }
}
