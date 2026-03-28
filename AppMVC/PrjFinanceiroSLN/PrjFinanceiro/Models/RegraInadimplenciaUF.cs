using System.Collections.Generic;

namespace PrjFinanceiro.Models
{
    public class RegraInadimplenciaUF : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente == null) return 0;

            var estados = new List<string> { "SP", "RJ" };
            if (estados.Contains(cliente.EstadoUF))
            {
                return -50;
            }

            return 0;
        }
    }
}
