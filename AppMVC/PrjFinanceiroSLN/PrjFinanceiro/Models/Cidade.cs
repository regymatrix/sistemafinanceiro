using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrjFinanceiro.Models
{
    public class Cidade
    {
        [Key]
        public int Codigo { get; set; }
        public string NomeCidade { get; set; }
        public string CodigoIBGE { get; set; }
        public int? CodigoEstado { get; set; }

        [ForeignKey("CodigoEstado")]
        public virtual Estado Estado { get; set; }
    }
}