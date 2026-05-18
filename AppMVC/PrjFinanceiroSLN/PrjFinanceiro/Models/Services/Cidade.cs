using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.Models.Services
{
    public class Cidade
    {
        [Key]
        public int Codigo { get; set; }
        public string NomeCidade { get; set; }
      
    }
}
