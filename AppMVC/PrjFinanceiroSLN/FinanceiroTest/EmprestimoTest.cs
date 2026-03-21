using PrjFinanceiro.Services;

namespace FinanceiroTest
{
    public class EmprestimoTest
    {
        [Fact]
        public void CalculoJurosSimples_Retorna100()
        {
            decimal valorEmprestado = 1000m;
            double taxa = 0.10;
            decimal valorEsperado = 100m;

            EmprestimoService emprestimo = new EmprestimoService();
            decimal resultado = emprestimo.CalcularJurosSimples(valorEmprestado, taxa);

            Assert.Equal(valorEsperado, resultado);
        }
    }
}