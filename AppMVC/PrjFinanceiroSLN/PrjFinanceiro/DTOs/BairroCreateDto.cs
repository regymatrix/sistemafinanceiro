using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class BairroCreateDto
    {
        [Required(ErrorMessage = "O nome do Bairro é obrigatório.")]
        [StringLength(100)]
        public string NomeBairro { get; set; }

        [Required(ErrorMessage = "O código da Cidade é obrigatório.")]
        public int CodigoCidade { get; set; }
    }
}