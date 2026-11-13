namespace PrjFinanceiro.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        public decimal CalcularValorFinal(decimal valorEmprestimo, decimal taxaJuros)
        {
            return valorEmprestimo + (valorEmprestimo * taxaJuros);
        }
    }
}