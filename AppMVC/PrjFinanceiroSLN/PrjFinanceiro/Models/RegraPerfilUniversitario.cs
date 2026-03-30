using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PrjFinanceiro.Models
{
    public class RegraPerfilUniversitario : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente is null) return 0;

            if (cliente.Nome.Contains("LTDA") || cliente.Nome.Contains("EIRELI"))
            {
                return -80;
            }
            return 0;
        }
    }
}
