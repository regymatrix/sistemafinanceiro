namespace PrjFinanceiro.Models
{
    public class RegraRestricaoNorte : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente == null || string.IsNullOrEmpty(cliente.EstadoUF))
                return 0;

            if (cliente.EstadoUF == "AM" || cliente.EstadoUF == "RR" || cliente.EstadoUF == "AP")
                return -20;

            return 0;
        }
    }
}
