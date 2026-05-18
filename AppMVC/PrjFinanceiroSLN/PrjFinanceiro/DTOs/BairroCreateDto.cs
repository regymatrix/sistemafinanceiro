using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class BairroCreateDto
    {
        [Required(ErrorMessage = "O nome do bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do bairro não pode exceder 100 caracteres.")]
        public string NomeBairro { get; set; }

        [Required(ErrorMessage = "O código da cidade é obrigatório.")]
        public int CodigoCidade { get; set; }
    }
}