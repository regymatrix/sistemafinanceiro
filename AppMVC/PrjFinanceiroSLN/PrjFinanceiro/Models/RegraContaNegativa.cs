namespace PrjFinanceiro.Models
{
    public class RegraContaNegativa : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (conta.Saldo < 0m)
                return -200;

            return 0;
        }
    }
}
