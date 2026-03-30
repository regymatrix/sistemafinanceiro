namespace PrjFinanceiro.Models
{
    public class RegraVIPLocal : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente.EstadoUF == "SE")
                return 120;

            return 0;
        }
    }
}
