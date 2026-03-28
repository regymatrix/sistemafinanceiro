namespace PrjFinanceiro.Services
{
    public interface IEmprestimoService
    {
        public decimal CalcularJurosSimples(decimal valor, double taxa);
    }
}
