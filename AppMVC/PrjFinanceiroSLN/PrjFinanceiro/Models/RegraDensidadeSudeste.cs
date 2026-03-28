using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace PrjFinanceiro.Models
{
    public class RegraDensidadeSudeste : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente == null ) return 0;

            var estados = new List<string> { "SP", "RJ","MG","ES" };
            if (estados.Contains(cliente.EstadoSudeste))
            {
                return +50;
            }
            return 0;
        }
        
    }
}
