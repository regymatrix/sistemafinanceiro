namespace PrjFinanceiro.Models
{
    public class RegraIncentivoCentroOeste : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente.EstadoUF is "DF" or "GO" or "MT")
                return 60;

            return 0;
        }
    }
}
