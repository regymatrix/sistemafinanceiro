namespace PrjFinanceiro.Models
{
    public class RegraVipLocal : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente == null || string.IsNullOrEmpty(cliente.EstadoUF))
                return 0;

            if (cliente.EstadoUF == "SE")
                return 120;

            return 0;
        }
    }
}
