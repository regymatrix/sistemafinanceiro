namespace PrjFinanceiro.Models
{
    public class RegraRestricaoNorte : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if(cliente.EstadoUF is "AM" or "RR" or "AP")
                return -20;

            return 0;
        }
    }
}
