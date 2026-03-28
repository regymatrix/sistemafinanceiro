using System.Collections.Generic;

namespace PrjFinanceiro.Models
{
    public class RegraFidelidade : IRegraEscore
    {
        public int CalcularPontuacao (Cliente cliente, ContaBancaria conta)
        {
            var estados = new List<string> { "SC" };
            if (estados.Contains(cliente.FidelidadeUF)) 
            {
                return 100;
            }

            /*if (cliente.FidelidadeUF == "SC")
            {
                return 0;
            }*/
            return 0;
        }
    }
}
