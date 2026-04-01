namespace PrjFinanceiro.Models
{
    public class RegraDeEstabilidade : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente.EstadoUF == "MG" || cliente.EstadoUF == "MG")
            {
                return 25;
            }

            return 0;
        }
    }
}
