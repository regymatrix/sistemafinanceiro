using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class ClienteCreateDto
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do cliente não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = "O tipo do cliente é obrigatório.")]
        [StringLength(20, ErrorMessage = "O tipo do cliente não pode exceder 20 caracteres.")]
        public string TipoCliente { get; set; }

        [StringLength(14, ErrorMessage = "O CPF não pode exceder 14 caracteres.")]
        public string CPF { get; set; }

        [StringLength(18, ErrorMessage = "O CNPJ não pode exceder 18 caracteres.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O código do bairro é obrigatório.")]
        public string CodigoBairro { get; set; }
    }
}