using PrjFinanceiro.Services;

namespace FinanceiroTeste
{
    public class EmprestimoTest
    {   
    
        [Fact]
        public void CalculoJurosSimples_Retorna1100()
        {
            //arrange

            decimal ValorEmprestado = 100m;
            double taxa = 0.10;
            decimal valorEsperado = 100;

            EmprestimoService emprestimo = new EmprestimoService();
            decimal resultado = emprestimo.CalcularJurosSimples(ValorEmprestado, taxa)
             
            Assert.Equal(valorEsperado, resultado);
        }
    }
}