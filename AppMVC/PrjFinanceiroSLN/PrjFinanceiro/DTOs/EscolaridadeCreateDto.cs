using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class EscolaridadeCreateDto
    {
        [Required(ErrorMessage = "A descrição da escolaridade é obrigatória.")]
        [StringLength(100, ErrorMessage = "A descrição não pode exceder 100 caracteres.")]
        public string Descricao { get; set; }
    }
}
