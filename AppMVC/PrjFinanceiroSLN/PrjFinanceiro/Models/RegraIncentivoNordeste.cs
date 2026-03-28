using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PrjFinanceiro.Models
{
    public class RegraIncentivoNordeste : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if(cliente is null) return 0;

            var estadosNordeste = new List<string>() {"SE", "AL", "BA", "CE", "MA", "PB", "PE", "PI", "RN"};

            if (estadosNordeste.Contains(cliente.EstadoUF))
            {
                return +40;
            }
            return 0;
        }
    }
}
