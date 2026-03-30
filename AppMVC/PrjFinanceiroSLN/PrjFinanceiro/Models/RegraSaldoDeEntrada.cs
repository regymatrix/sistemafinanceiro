using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrjFinanceiro.Models;

namespace FinanceiroTest
{
    public class RegraSaldoDeEntrada : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if(conta.Saldo >= 1000m && conta.Saldo <= 5000m)
                return 30;

            return 0;
        }
    }
}
