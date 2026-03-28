namespace PrjFinanceiro.Models
{
    public interface IRegraEscore
    {
        int CalcularPontuacao(Cliente cliente, ContaBancaria conta);
    }
}
