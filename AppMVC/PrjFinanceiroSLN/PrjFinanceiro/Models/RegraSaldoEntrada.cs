namespace PrjFinanceiro.Models
{
    public class RegraSaldoEntrada
    {
        public int CalcularPontuacao(Cliente cliente, ContaBancaria conta) =>
        conta?.Saldo >= 1000 && conta?.Saldo <= 5000 ? 30 : 0;
    }
}


    
