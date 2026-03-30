using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrjFinanceiro.Models;

namespace FinanceiroTest
{
    public class RegraPerfilUniversitario : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente.Nome.Contains("EIRELI") || cliente.Nome.Contains("LTDA"))
                return -80;

            return 0;
        }
    }
}
