
using PrjFinanceiro.Services;

namespace FinanceiroTest
{
    public class EmprestimoTest
    {
        [Fact]
        public void CalculoJurosSimples_Retorna1100()
        {
            // Arrange
            decimal valorEmprestado = 1000m;
            double taxa = 0.10;
            decimal valorEsperado = 1100m;

            EmprestimoService emprestimo = new EmprestimoService();
            decimal resultado = emprestimo.CalcularJurosSimples(valorEmprestado, taxa);

            Assert.Equal(valorEsperado, resultado);
        }
    }
}