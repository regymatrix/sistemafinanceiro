using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class EscolaridadeCreateDto
    {
        [Required(ErrorMessage = "A descrição da escolaridade é obrigatória.")]
        [StringLength(100)]
        public string Descricao { get; set; }

    }
}
