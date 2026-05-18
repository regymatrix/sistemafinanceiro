using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class EtniaCreateDto
    {
        [Required(ErrorMessage = "A descrição da Etnia é obrigatória.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Descricao { get; set; }
    }
}