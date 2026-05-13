using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.Models
{
    public class Bairro
    {
        [Key]
        public int Codigo { get; set; }
        public string NomeBairro { get; set; }
        public int CodigoCidade { get; set; }
    }
}
