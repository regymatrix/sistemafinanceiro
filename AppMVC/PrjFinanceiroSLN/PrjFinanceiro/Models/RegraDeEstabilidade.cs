using System.Collections.Generic;

namespace PrjFinanceiro.Models
{
    public class RegraDeEstabilidade : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente == null || cliente.EstadoUF == null) return 0;
            var estados = new List<string>() { "MG", "PR" };

            if (estados.Contains(cliente.EstadoUF.ToUpper()))
            {
                return 25;
            }

            return 0;
        }
    }
}

