using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjFinanceiro.Models
{
    public class RegraNomenclaturaPadrao : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente == null || cliente.Nome == null) return 0;

            if (cliente.Nome.Trim().Length < 10)
            {
                return -40;
            }

            return 0;
        }
    }
}
