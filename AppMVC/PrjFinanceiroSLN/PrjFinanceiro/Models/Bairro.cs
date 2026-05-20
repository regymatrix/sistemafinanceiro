using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrjFinanceiro.Models
{
    public class Bairro
    {
        [Key]
        public int Codigo { get; set; }
        public string NomeBairro { get; set; }
        public int CodigoCidade { get; set; }

        [ForeignKey("CodigoCidade")]
        public virtual Cidade Cidade { get; set; }
    }
}