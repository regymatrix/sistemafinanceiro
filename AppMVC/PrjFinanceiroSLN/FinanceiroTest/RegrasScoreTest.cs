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


    }
}
