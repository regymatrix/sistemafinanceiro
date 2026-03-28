using System.Collections.Generic;

namespace PrjFinanceiro.Models
{
    public class RegraDensidadeSudeste : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            var estados = new List<string> { "SP", "RJ", "MG", "ES" };
            if (estados.Contains(cliente.EstadoSudeste))
            {
                return +50;
            }
            return 0;
        }
    }
}
