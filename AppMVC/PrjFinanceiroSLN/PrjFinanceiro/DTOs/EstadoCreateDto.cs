using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class EstadoCreateDto
    {
        [Required(ErrorMessage = "O nome do estado é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string NomeEstado { get; set; }

        [Required(ErrorMessage = "Sigla é obrigatório.")]
        public string Sigla { get; set; }

    
    }
}
