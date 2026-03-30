namespace PrjFinanceiro.Models
{
    public class RegraContaNegativa : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (conta is null) return 0;

            if (conta.Saldo < 0 )
            {
                return -200;
            }

            return 0;
        }
    }
}
