namespace PrjFinanceiro.NovaPasta1
{
    public class EmprestimoService : IEmprestimoService
    {
        public decimal CalcularJurosSimples(decimal valor, double taxa)
        {
            return (valor*(decimal)taxa);
        }
    }
}
