namespace PrjFinanceiro.Models
{
    public class RegraIncentivoCentroOeste : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente.EstadoUF == "DF"|| cliente.EstadoUF == "GO" || cliente.EstadoUF == "MT")
                return 60;

            return 0;
        }
    }
}
