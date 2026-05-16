using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.Models
{
    public class Cidade
    {

        [Key]
        public int Codigo { get; set; }
        public string NomeCidade { get; set; }
        public string CodigoIBGE { get; set; }
        public int CodigoEstado { get; set; }
    }

}
