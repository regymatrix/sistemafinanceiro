using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class CidadeCreateDto
    {
        [Required(ErrorMessage = "O nome da Cidade é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string NomeCidade { get; set; }

        [Required(ErrorMessage = "O código IBGE é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string CodigoIBGE { get; set; }

        [Required(ErrorMessage = "O código do Estado é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public int CodigoEstado { get; set; }
    }
}