using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class CidadeCreateDto
    {
        [Required(ErrorMessage = "O nome da cidade é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da cidade não pode exceder 100 caracteres.")]
        public string NomeCidade { get; set; }

        [Required(ErrorMessage = "O código IBGE é obrigatório.")]
        [StringLength(20, ErrorMessage = "O código IBGE não pode exceder 20 caracteres.")]
        public string CodigoIBGE { get; set; }

        [Required(ErrorMessage = "O código do estado é obrigatório.")]
        public int CodigoEstado { get; set; }
    }
}