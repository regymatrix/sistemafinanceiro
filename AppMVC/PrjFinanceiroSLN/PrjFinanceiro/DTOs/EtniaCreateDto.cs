using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class EtniaCreateDto
    {
        [Required(ErrorMessage = "A descrição da etnia é obrigatória.")]
        [StringLength(100, ErrorMessage = "A descrição não pode exceder 100 caracteres.")]
        public string Descricao { get; set; }
    }
}