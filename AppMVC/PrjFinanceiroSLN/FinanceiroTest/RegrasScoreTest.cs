using PrjFinanceiro.Models;
using PrjFinanceiro.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroTest
{
    public class RegrasScoreTest
    {
        [Fact]
        public void RegraSaldoMinimo_DeveSubtrair30Pontos_QuandoSaldoForMenorQue100()
        {
            // Arrange
            var regra = new RegraSaldoMinimo();
            var cliente = new Cliente { Nome = "Teste" };
            var conta = new ContaBancaria { Saldo = 50m }; // Saldo < 100

            // Act
            int resultado = regra.CalcularPontuacao(cliente, conta);

            // Assert
            Assert.Equal(-30, resultado);
        }

        [Fact]
        public void RegraFidelidade_SCRetornaPositivo100()
        {
            var regra = new RegraFidelidade();
            var cliente = new Cliente { Nome = "Lilian", EstadoUF = "SC" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(100, resultado);
        }

        [Fact]
        public void RegraFidelidade_SemEstadoRetorna0()
        {
            var regra = new RegraFidelidade();
            var cliente = new Cliente { Nome = "Lilian"};

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraFidelidade_EstadoDiferenteRetorna0()
        {
            var regra = new RegraFidelidade();
            var cliente = new Cliente { Nome = "Lilian", EstadoUF = "SE"};

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);
        }


        [Fact]
        public void RegraFidelidade_ClienteVazioRetorna0()
        {
            var regra = new RegraFidelidade();

            int resultado = regra.CalcularPontuacao(null, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraSaldoSeguranca_SaldoMaiorQue50000_Retorna150()
        {
            var regra = new RegraSaldoSeguranca();
            var conta = new ContaBancaria { Saldo = 60000 };

            int resultado = regra.CalcularPontuacao(null, conta);

            Assert.Equal(150, resultado);
        }

        [Fact]
        public void RegraSaldoSeguranca_SaldoMenorOuIgual50000_Retorna0()
        {
            var regra = new RegraSaldoSeguranca();
            var conta = new ContaBancaria { Saldo = 50000 };

            int resultado = regra.CalcularPontuacao(null, conta);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraSaldoSeguranca_ContaNula_Retorna0()
        {
            var regra = new RegraSaldoSeguranca();

            int resultado = regra.CalcularPontuacao(null, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraContaNegativa_SaldoNegativo_RetornaMenos200()
        {
            var regra = new RegraSaldoSeguranca();
            var conta = new ContaBancaria { Saldo = -50 };


            int resultado = regra.CalcularPontuacao(null, conta);

            Assert.Equal(-200, resultado);
        }

        [Fact]
        public void RegraContaNegativa_SaldoPositivo_Retorna0()
        {
            var regra = new RegraSaldoSeguranca();
            var conta = new ContaBancaria { Saldo = 100 };


            int resultado = regra.CalcularPontuacao(null, conta);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraContaNegativa_ContaNula_Retorna0()
        {
            var regra = new RegraSaldoSeguranca();

            int resultado = regra.CalcularPontuacao(null, null);

            Assert.Equal(0, resultado);
        }
    }
}
