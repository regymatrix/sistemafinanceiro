
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PrjFinanceiro.Models
{
    public class RegraRestricaoNorte : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente is null) return 0;

            var estadosNordeste = new List<string>() { "AM", "RR", "AP" };

            if (estadosNordeste.Contains(cliente.EstadoUF))
            {
                return -20;
            }
            return 0;
        }
    }
}
