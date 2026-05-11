using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.Models
{
    public class Escolaridade
    {
        [Key]
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}