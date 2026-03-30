using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PrjFinanceiro.Models
{
    public class RegraIncentivoCentroOeste : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente is null) return 0;

            var estadosNordeste = new List<string>() { "DF", "GO", "MT" };

            if (estadosNordeste.Contains(cliente.EstadoUF))
            {
                return +60;
            }
            return 0;
        }
    }
}
