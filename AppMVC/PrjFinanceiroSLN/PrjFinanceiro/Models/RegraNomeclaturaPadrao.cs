namespace PrjFinanceiro.Models
{
    public class RegraNomeclaturaPadrao : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if(cliente.Nome.Length < 10)
                return -40;

            return 0;
        }
    }
}
