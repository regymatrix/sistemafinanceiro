using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace PrjFinanceiro.Models
{
    public class Funcionario
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string EstadoUF { get; set; }

        // REMOVA: public string Cidade { get; set; } <--- Esta linha causa o erro da imagem

        public int CodigoCidade { get; set; }
        [ForeignKey("CodigoCidade")]
        public virtual Cidade CidadeRel { get; set; }

        public int CodigoBairro { get; set; }
        [ForeignKey("CodigoBairro")]
        public virtual Bairro BairroRel { get; set; }

        public int CodigoEscolaridade { get; set; }
        [ForeignKey("CodigoEscolaridade")]
        public virtual Escolaridade Escolaridade { get; set; } // Verifique se esta classe existe no seu projeto

        public int CodigoEtnia { get; set; }
        [ForeignKey("CodigoEtnia")]
        public virtual Etnia Etnia { get; set; }
    }
}