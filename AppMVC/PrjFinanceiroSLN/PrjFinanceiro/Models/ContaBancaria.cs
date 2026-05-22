using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PrjFinanceiro.Models
{
    public class ContaBancaria
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoAgencia { get; set; }
        public string NumeroConta { get; set; }
        public bool StatusConta  { get; set; }
        public string TipoConta { get; set; }

    }
}
