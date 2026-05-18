using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class CidadeCreateDto
    {
        [Required(ErrorMessage = "O nome da cidade é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string NomeCidade { get; set; }

        [Required(ErrorMessage = "O código IBGE é obrigatório.")]
        public string CodigoIBGE { get; set; }

        [Required(ErrorMessage = "O código do estado é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A código deve conter exatamente 2 caracteres.")]
        public int CodigoEstado { get; set; }
    }
}
