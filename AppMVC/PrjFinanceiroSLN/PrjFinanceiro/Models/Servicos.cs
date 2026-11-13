namespace PrjFinanceiro.Models
{
    public class EmprestimoService
    {
        public decimal CalcularJurosSimples (decimal valor, decimal taxa)
        {
            return valor + (valor * taxa);
        }
    }
}
