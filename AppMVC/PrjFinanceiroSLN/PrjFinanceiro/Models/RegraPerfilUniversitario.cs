using System.Runtime.InteropServices;

namespace PrjFinanceiro.Models
{
    public class RegraPerfilUniversitario : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente is null || string.IsNullOrWhiteSpace(cliente.Nome)) return 0;

            if (cliente.Nome.ToUpper().Contains("EIRELI") || cliente.Nome.ToUpper().Contains("LTDA"))
            {
                return -80;
            }

            return 0;
        }
    }
}
