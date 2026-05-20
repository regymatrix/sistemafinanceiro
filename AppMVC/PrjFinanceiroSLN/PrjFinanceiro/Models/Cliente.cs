using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace PrjFinanceiro.Models
{
    public class Cliente
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string TipoCliente { get; set; }

        // O '?' diz ao Entity Framework que tudo bem se a coluna estiver vazia no banco
        public string? CPF { get; set; }
        public string? CNPJ { get; set; }

        public int CodigoBairro { get; set; }
        [ForeignKey("CodigoBairro")]
        public virtual Bairro BairroRel { get; set; }
    }
}