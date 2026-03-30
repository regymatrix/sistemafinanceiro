using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrjFinanceiro.Models
{
    public class RegraVipLocal : IRegraEscore
    {
            public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
            {
                if (cliente == null) return 0;

                if (cliente.EstadoUF == "SE") return 120;

                return 0;
            }
        
    }
}
