using System.Collections.Generic;

namespace PrjFinanceiro.Models
{
    public class RegraRestricaoNorte : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente == null || cliente.EstadoUF == null) return 0;
            var estdos = new List<string> { "AM", "RR", "AP" };

            if (estdos.Contains(cliente.EstadoUF.ToUpper()))
            {
                return -20;
            }

            return 0;
        }
    }
}
