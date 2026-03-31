namespace PrjFinanceiro.Models
{
	public class RegraPotencialInvestimento : IRegraEscore
	{
		public int CalcularPontuacao(Cliente cliente, ContaBancaria conta)
		{
			if (conta?.Saldo == 2000)
				return 45;

			return 0;
		}
	}
}
