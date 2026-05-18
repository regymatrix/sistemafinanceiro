using System.ComponentModel.DataAnnotations;
using PrjFinanceiro.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrjFinanceiro.DTOs
{
    public class CidadeCreateDto
    {
        [Required(ErrorMessage = "O nome da cidade é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string NomeCidade { get; set; }


        [Required(ErrorMessage = "O codigo do IBGE é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string CodigoIBGE { get; set; }


        [Required(ErrorMessage = "O  é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public int CodigoEstado { get; set; }
        [ForeignKey("CodigoEstado")]
         public virtual Estado Estado { get; set; }
        
    }
}
