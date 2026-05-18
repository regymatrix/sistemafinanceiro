using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class BairroCreateDto
    {
        [Required(ErrorMessage = "O nome da cidadeo bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string NomeBairro { get; set; }

        [Required(ErrorMessage = "O código da cidade é obrigatória.")]
        public int CodigoCidade { get; set; }

    }
}
