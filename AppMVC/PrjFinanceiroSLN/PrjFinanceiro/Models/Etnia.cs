using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.Models
{
    public class Etnia
    {
        [Key]
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}