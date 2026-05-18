using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class CidadeCreateDto
    {
        [Required(ErrorMessage = "O nome da Cidade é obrigatório.")]
        [StringLength(100)]
        public string NomeCidade { get; set; }

        [Required(ErrorMessage = "O código IBGE é obrigatório.")]
        [StringLength(100)]
        public string CodigoIBGE { get; set; }

        [Required(ErrorMessage = "O código do Estado é obrigatório.")]
        public int CodigoEstado { get; set; }
    }
}