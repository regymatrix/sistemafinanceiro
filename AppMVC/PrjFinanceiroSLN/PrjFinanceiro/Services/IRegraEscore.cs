using PrjFinanceiro.Models;

namespace PrjFinanceiro.Services
{
    public interface IRegraEscore
    {
        int CalcularPontuacao(Cliente cliente, ContaBancaria conta);
    }
}
