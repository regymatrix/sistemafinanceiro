namespace PrjFinanceiro.Services
{
    public interface IEmprestimoService
    {
        decimal CalcularValorFinal(decimal valorEmprestimo, decimal taxaJuros);
    }
}