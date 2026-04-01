namespace PrjFinanceiro.Models
{
    public class RegraEstabilidade :IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {

            if (cliente?.EstadoUF == "MG" || cliente?.EstadoUF == "RR")
            {
                return 25;
            }

            return 0;
        }
    }
}
