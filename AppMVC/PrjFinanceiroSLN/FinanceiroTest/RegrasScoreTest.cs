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
            var regra = new RegraContaNegativa();
            var conta = new ContaBancaria { Saldo = -50 };


            int resultado = regra.CalcularPontuacao(null, conta);

            Assert.Equal(-200, resultado);
        }

        [Fact]
        public void RegraContaNegativa_SaldoPositivo_Retorna0()
        {
            var regra = new RegraContaNegativa();
            var conta = new ContaBancaria { Saldo = 100 };


            int resultado = regra.CalcularPontuacao(null, conta);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraContaNegativa_ContaNula_Retorna0()
        {
            var regra = new RegraContaNegativa();

            int resultado = regra.CalcularPontuacao(null, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraPerfilUniversitario_NomeComLTDA_RetornaMenos80()
        {
            var regra = new RegraPerfilUniversitario();
            var cliente = new Cliente { Nome = "Empresa LTDA" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(-80, resultado);
        }

        [Fact]
        public void RegraPerfilUniversitario_NomeComEIRELI_RetornaMenos80()
        {
            var regra = new RegraPerfilUniversitario();
            var cliente = new Cliente { Nome = "Mercado EIRELI" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(-80, resultado);
        }

        [Fact]
        public void RegraPerfilUniversitario_NomePessoaFisica_Retorna0()
        {
            var regra = new RegraPerfilUniversitario();
            var cliente = new Cliente { Nome = "Lili" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraPerfilUniversitario_ClienteNulo_Retorna0()
        {
            var regra = new RegraPerfilUniversitario();
            var cliente = new Cliente { Nome = "EmpresaLTDA" };

            int resultado = regra.CalcularPontuacao(null, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraIncentivoCentro_Oeste_EstadoDF_Retorna60()
        {
            var regra = new RegraIncentivoCentro_Oeste();
            var cliente = new Cliente { Nome = "Lili" , EstadoUF = "DF" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(60, resultado);
        }

        [Fact]
        public void RegraIncentivoCentro_Oeste_EstadoGO_Retorna60()
        {
            var regra = new RegraIncentivoCentro_Oeste();
            var cliente = new Cliente { Nome = "Lili", EstadoUF = "GO" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(60, resultado);
        }

        [Fact]
        public void RegraIncentivoCentro_Oeste_EstadoMT_Retorna60()
        {
            var regra = new RegraIncentivoCentro_Oeste();
            var cliente = new Cliente { Nome = "Lili", EstadoUF = "MT" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(60, resultado);
        }

        [Fact]
        public void RegraIncentivoCentro_Oeste_EstadoForaDaLista_Retorna0()
        {
            var regra = new RegraIncentivoCentro_Oeste();
            var cliente = new Cliente { Nome = "Lili", EstadoUF = "SE" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraIncentivoCentro_Oeste_ClienteNulo_Retorna0()
        {
            var regra = new RegraIncentivoCentro_Oeste();

            int resultado = regra.CalcularPontuacao(null, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraRestricaoNorte_EstadoAM_RetornaMenos20()
        {
            var regra = new RegraRestricaoNorte();
            var cliente = new Cliente { Nome = "Lili", EstadoUF = "AM" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(-20, resultado);
        }

        [Fact]
        public void RegraRestricaoNorte_EstadoRR_RetornaMenos20()
        {
            var regra = new RegraRestricaoNorte();
            var cliente = new Cliente { Nome = "Lili", EstadoUF = "RR" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(-20, resultado);
        }

        [Fact]
        public void RegraRestricaoNorte_EstadoAP_RetornaMenos20()
        {
            var regra = new RegraRestricaoNorte();
            var cliente = new Cliente { Nome = "Lili", EstadoUF = "AP" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(-20, resultado);
        }

        [Fact]
        public void RegraRestricaoNorte_EstadoForaDaLista_Retorna0()
        {
            var regra = new RegraRestricaoNorte();
            var cliente = new Cliente { Nome = "Lili", EstadoUF = "SE" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraRestricaoNorte_ClienteNulo_Retorna0()
        {
            var regra = new RegraRestricaoNorte();

            int resultado = regra.CalcularPontuacao(null, null);

            Assert.Equal(0, resultado);
        }


        [Fact]
        public void RegraVIPLocal_EstadoSE_Retorna120()
        {
            var regra = new RegraVipLocal();
            var cliente = new Cliente { Nome = "Lilian", EstadoUF = "SE" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(120, resultado);
        }

        [Fact]
        public void RegraVIPLocal_EstadoDiferente_Retorna0()
        {
            var regra = new RegraVipLocal();
            var cliente = new Cliente { Nome = "Lilian", EstadoUF = "BA" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraVIPLocal_ClienteNulo_Retorna0()
        {
            var regra = new RegraVipLocal();

            int resultado = regra.CalcularPontuacao(null, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraSaldoEntrada_SaldoDentroDoIntervalo_Retorna30()
        {
            var regra = new RegraSaldoEntrada();
            var conta = new ContaBancaria { Saldo = 3000 };

            int resultado = regra.CalcularPontuacao(null, conta);

            Assert.Equal(30, resultado);
        }

        [Fact]
        public void RegraSaldoEntrada_SaldoIgual1000_Retorna30()
        {
            var regra = new RegraSaldoEntrada();
            var conta = new ContaBancaria { Saldo = 1000 };

            int resultado = regra.CalcularPontuacao(null, conta);

            Assert.Equal(30, resultado);
        }

        [Fact]
        public void RegraSaldoEntrada_SaldoIgual5000_Retorna30()
        {
            var regra = new RegraSaldoEntrada();
            var conta = new ContaBancaria { Saldo = 5000 };

            int resultado = regra.CalcularPontuacao(null, conta);

            Assert.Equal(30, resultado);
        }

        [Fact]
        public void RegraSaldoEntrada_SaldoForaDoIntervalo_Retorna0()
        {
            var regra = new RegraSaldoEntrada();
            var conta = new ContaBancaria { Saldo = 900 };

            int resultado = regra.CalcularPontuacao(null, conta);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraNomenclaturaPadrao_NomeMenorQue10Caracteres_RetornaMenos40()
        {
            var regra = new RegraNomenclaturaPadrao();
            var cliente = new Cliente { Nome = "Lilian" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(-40, resultado);
        }

        [Fact]
        public void RegraNomenclaturaPadrao_NomeCom10OuMaisCaracteres_Retorna0()
        {
            var regra = new RegraNomenclaturaPadrao();
            var cliente = new Cliente { Nome = "Lilian Alves" };

            int resultado = regra.CalcularPontuacao(cliente, null);

            Assert.Equal(0, resultado);
        }

        [Fact]
        public void RegraNomenclaturaPadrao_ClienteNulo_Retorna0()
        {
            var regra = new RegraNomenclaturaPadrao();

            int resultado = regra.CalcularPontuacao(null, null);

            Assert.Equal(0, resultado);
        }
    }
}
