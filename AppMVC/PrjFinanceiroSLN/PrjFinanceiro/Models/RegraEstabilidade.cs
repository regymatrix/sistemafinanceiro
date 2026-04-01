namespace PrjFinanceiro.Models
{
    public class RegraEstabilidade : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente == null || string.IsNullOrEmpty(cliente.EstadoUF))
                return 0;

            if (cliente.EstadoUF == "MG" || cliente.EstadoUF == "PR")
                return 25;

            return 0;
        }
    }
}
