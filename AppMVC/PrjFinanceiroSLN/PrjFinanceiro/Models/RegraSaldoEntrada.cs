using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjFinanceiro.Models
{
    public class RegraSaldoEntrada : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (conta is null) return 0;

            if (conta.Saldo >= 1000 && conta.Saldo <= 5000)
            {
                return 30;
            }

            return 0;
        }
    }
}
