using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class EtniaCreateDto
    {
        [Required(ErrorMessage = "A descrição da Etnia é obrigatória.")]
        [StringLength(100)]
        public string Descricao { get; set; }
    }
}