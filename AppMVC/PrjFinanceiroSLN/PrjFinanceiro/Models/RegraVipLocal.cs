namespace PrjFinanceiro.Models
{
    public class RegraVipLocal : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta) =>
        cliente?.EstadoUF == "SE" ? 120 : 0;


    }
}
