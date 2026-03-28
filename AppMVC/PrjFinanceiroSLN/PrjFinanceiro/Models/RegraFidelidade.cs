using System.Collections.Generic;

namespace PrjFinanceiro.Models
{
    public class RegraFidelidade : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente == null) return 0;

            var estados = new List<string> { "SC" };
            if (estados.Contains(cliente.EstadoUF))
            {
                return 100;
            }

            return 0; 
        }
    }
}
