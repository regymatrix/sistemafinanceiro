namespace PrjFinanceiro.Models
{
    public class RegraSaldoEntrada : IRegraEscore
	{
		public int CalcularPontuacao(Cliente cliente, ContaBancaria conta) =>
		conta?.Saldo >= 1000 && conta?.Saldo <= 5000 ? 30 : 0;
	}
}
