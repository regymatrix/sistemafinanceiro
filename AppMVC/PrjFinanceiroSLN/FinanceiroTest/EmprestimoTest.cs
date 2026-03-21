using PrjFinanceiro.NovaPasta1;

namespace FinanceiroTest
{
    public class EmprestimoTest
    {
        [Fact]
        public void CalculoJurosSimples_Retorna1100()
        {
            decimal valorEmprestado = 1000m;
            double taxa = 0.10;
            decimal valorEsperado = 100;

            EmprestimoService emprestimo = new EmprestimoService();
            decimal resultado = emprestimo.CalcularJurosSimples(valorEmprestado, taxa);
            Assert.Equal(valorEsperado, resultado);
        }
    }
}