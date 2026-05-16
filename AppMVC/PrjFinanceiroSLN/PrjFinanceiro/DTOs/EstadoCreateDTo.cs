using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class EstadoCreateDto
    {
            [Required(ErrorMessage = "O nome do estado é obrigatório.")]
            [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
            public string NomeEstado { get; set; }

            [Required(ErrorMessage = "A sigla é obrigatória.")]
            [StringLength(2, MinimumLength = 2, ErrorMessage = "A sigla deve conter exatamente 2 caracteres.")]
            public string Sigla { get; set; }
    }

}

