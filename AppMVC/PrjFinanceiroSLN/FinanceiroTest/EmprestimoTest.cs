using PrjFinanceiro.Models.Services;

namespace FinanceiroTest
{
    public class EmprestimoTest
    {
        [Fact]
        public void CalcularJurosSimples_DeveRetornarValorCorreto()
        {
            decimal valorEmprestimo = 1000m;
            double taxa= 0.10;
            decimal valorEsperado = 100m;

            EmprestimoService emprestimo = new EmprestimoService();

            decimal resultado = emprestimo.CalcularJurosSimples(valorEmprestimo, taxa);

            Assert.Equal(valorEsperado,resultado);
        }
    }
}