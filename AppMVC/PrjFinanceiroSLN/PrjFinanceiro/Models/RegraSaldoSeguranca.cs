namespace PrjFinanceiro.Models
{
    public class RegraSaldoSeguranca : IRegraEscore
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
        {
            if (conta is null) return 0;

            if (conta.Saldo > 50000)
            {
                return 150;
            }
            return 0; 
        }
    }
}
