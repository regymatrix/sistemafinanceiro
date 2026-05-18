using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class BairroCreateDto
    {
        [Required(ErrorMessage = "O nome do Bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string NomeBairro { get; set; }

        [Required(ErrorMessage = "O CodigoCidade é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public int CodigoCidade { get; set; }
    }
}