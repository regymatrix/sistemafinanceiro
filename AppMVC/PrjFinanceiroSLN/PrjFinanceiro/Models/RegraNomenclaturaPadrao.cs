namespace PrjFinanceiro.Models
{
    public class RegraNomenclaturaPadrao : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (cliente?.Nome?.Length < 10)
                return -40;

            return 0;
        }
    }
}
