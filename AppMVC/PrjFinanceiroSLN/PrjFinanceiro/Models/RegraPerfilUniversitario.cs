using Microsoft.Identity.Client;

namespace PrjFinanceiro.Models
{
    public class RegraPerfilUniversitario : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente == null || cliente.Nome == null || cliente.Nome == "") return 0;
            if (cliente.Nome.Contains("EIRELI") || cliente.Nome.Contains("LTDA")) return -80;
            return 0; 
        }
    }
}
