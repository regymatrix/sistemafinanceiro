using System.Collections.Generic;

namespace PrjFinanceiro.Models
{
    public class RegraIncentivoCentro_Oeste : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if ( cliente is null || string.IsNullOrWhiteSpace(cliente.EstadoUF)) return 0;
            var estdos = new List<string> { "DF", "GO", "MT" };

            if (estdos.Contains(cliente.EstadoUF.ToUpper()))
            {
                return 60;
            }

            return 0;
        }
    }
}
