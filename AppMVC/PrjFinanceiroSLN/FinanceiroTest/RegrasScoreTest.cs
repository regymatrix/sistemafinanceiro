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

        public void RegraInadimplencia_RJRetornaNegativo50()
        {
            var regra = new RegraInadimplenciaUF();
            var cliente = new Cliente { Nome = "Fausto", EstadoUF = "RJ" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(-50, resultado);
        }

        [Fact]
        public void RegraInadimplencia_SemEstadoRetorna0()
        {
            var regra = new RegraInadimplenciaUF();
            var cliente = new Cliente { Nome = "Fausto" };
             int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);
        }
        [Fact]
        public void RegraFidelidade_SCRetornaPositivo100()
        {
            var regra = new RegraFidelidade();
            var cliente = new Cliente { Nome = "Lara", EstadoUF = "SC" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(100, resultado);
        }
        [Fact]
        public void RegraFidelidade_SemEstadoRetorna0()
        {
            var regra = new RegraFidelidade();
            var cliente = new Cliente { Nome = "Lara" };
            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);
        }


        public void RegraDensidadeSudeste_SPRetornaPositivo50()
        {
            var regra = new RegraDensidadeSudeste();
            var cliente = new Cliente { Nome = "Joao", EstadoSudeste = "SP" };
            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(+50, resultado);
        }
        [Fact]
        public void RegraDensidadeSudeste_SemEstadoRetorna0()
        {
            var regra = new RegraDensidadeSudeste();
            var cliente = new Cliente { Nome = "Joao" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);
        }



        [Fact]
        public void RegraInadimplencia_EstadoDiferente_Retorna0()
        {
            var regra = new RegraInadimplenciaUF();
            var cliente = new Cliente { Nome = "Fausto", EstadoUF="SE" };
            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraDensidadeSudeste_EstadoDiferente_Retorna0()
        {
            var regra = new RegraDensidadeSudeste();
            var cliente = new Cliente { Nome = "Joao", EstadoSudeste="SE" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);
        }



        [Fact]
        public void RegraInadimplencia_ClienteVazio_Retorna0()
        {
            var regra = new RegraInadimplenciaUF();
            int resultado = regra.CalcularPontuacao(null, null);

            Assert.Equal(0, resultado);
        }
           

        [Fact]
        public void RegraFidelidade_EstadoDiferente_Retorna0()
        {
            var regra = new RegraFidelidade();
            var cliente = new Cliente { Nome = "Joao", EstadoSudeste="SE" };
            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);

        }
        [Fact]
        public void RegraDensidadeSudeste_ClienteVazio_Retorna0()
        {
            var regra = new RegraDensidadeSudeste();

            int resultado = regra.CalcularPontuacao(null, null);

            Assert.Equal(0, resultado);
        }
        public void RegraSaldoSeguranca_Clientecom50000_DeveRetornar_150()
        {
            
            var regra = new RegraSaldoSeguranca();
            var cliente = new Cliente { Nome = "Teste" };
            var conta = new ContaBancaria { Saldo = 50000m }; 

            int resultado = regra.CalcularPontuacao(cliente, conta);
            Assert.Equal(+150, resultado);
        }

        [Fact]
        public void RegraIncentivoCentroOeste_ClienteDeDF_DeveRetornar_60pontos()
        {
            var cliente = new Cliente { EstadoUF = "DF" };
            var banco = new ContaBancaria();
            var regra = new RegraIncentivoCentroOeste();
            int resultado = regra.CalcularPontuacao(cliente, banco);

            Assert.Equal(60, resultado);
        }

        [Fact]
        public void RegraIncentivoCentroOeste_ClienteDeGO_DeveRetornar_60pontos()
        {
            var cliente = new Cliente { EstadoUF = "GO" };
            var banco = new ContaBancaria();
            var regra = new RegraIncentivoCentroOeste();
            int resultado = regra.CalcularPontuacao(cliente, banco);

            Assert.Equal(60, resultado);
        }

        [Fact]
        public void RegraIncentivoCentroOeste_ClienteDeMT_DeveRetornar_60pontos()
        {
            var cliente = new Cliente { EstadoUF = "MT" };
            var banco = new ContaBancaria();
            var regra = new RegraIncentivoCentroOeste();
            int resultado = regra.CalcularPontuacao(cliente, banco);

            Assert.Equal(60, resultado);
        }

        [Fact]
        public void RegraIncentivoCentroOeste_Cliente_DeveRetornar_0pontos()
        {
            var cliente = new Cliente { EstadoUF = "SE" };
            var banco = new ContaBancaria();
            var regra = new RegraIncentivoCentroOeste();
            int resultado = regra.CalcularPontuacao(cliente, banco);

            Assert.Equal(0, resultado);
        }

    }
}
