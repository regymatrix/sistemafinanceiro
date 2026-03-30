
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PrjFinanceiro.Models
{
    public class RegraVipLocal : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente is null) return 0;

            var estadosNordeste = new List<string>() { "SE"};

            if (estadosNordeste.Contains(cliente.EstadoUF))
            {
                return +120;
            }
            return 0;
        }
    }
}
