using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class BairroCreateDto
    {
        [Required(ErrorMessage = "O nome do bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string NomeBairro { get; set; }

        [Required(ErrorMessage = "O codigo é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A UF deve conter exatamente 2 caracteres.")]
        public string Codigo { get; set; }
    }
}
