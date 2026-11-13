using Xunit;
// Lembre-se de ajustar o namespace abaixo para onde sua EmprestimoService vai ficar no projeto principal
using PrjFinanceiro.Services;

namespace PrjFinanceiro.Tests
{
    public class EmprestimoServiceTest
    {
        [Fact]
        public void CalcularValorFinal_EmprestimoMilComTaxaDezPorCento_DeveRetornarMilECem()
        {
            // Arrange (Preparação)
            var service = new EmprestimoService();
            decimal valorEmprestimo = 1000.00m;
            decimal taxaJuros = 0.10m;
            decimal valorEsperado = 1100.00m;

            // Act (Ação)
            decimal resultado = service.CalcularValorFinal(valorEmprestimo, taxaJuros);

            // Assert (Verificação)
            Assert.Equal(valorEsperado, resultado);
        }
    }
}