using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrjFinanceiro.Models
{
    public class ContaBancaria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // O número da conta é digitado pelo usuário, não é Identity
        public int NumeroConta { get; set; }

        [Required]
        public int CodigoCliente { get; set; }

        [Required]
        public int CodigoAgencia { get; set; }

        [Required]
        public bool StatusConta { get; set; }

        [Required]
        public string TipoConta { get; set; }

        [ForeignKey("CodigoCliente")]
        public virtual Cliente? ClienteRel { get; set; }
    }
}