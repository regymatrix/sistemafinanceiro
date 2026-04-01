using System.Collections.Generic;

namespace PrjFinanceiro.Models
{
    public class RegraIncentivoCentro_Oeste : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente == null || cliente.EstadoUF == null) return 0;
            var estdos = new List<string> { "DF", "GO", "MT" };

            if (estdos.Contains(cliente.EstadoUF.ToUpper()))
            {
                return 60;
            }

            return 0;
        }
    }
}
