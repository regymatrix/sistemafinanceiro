using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class BairroCreateDto
    {
        [Required(ErrorMessage = "O nome do bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string NomeBairro { get; set; }

        [Required(ErrorMessage = "O código é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O código deve conter exatamente 2 caracteres.")]
        public int CodigoCidade { get; set; }
    }
}
