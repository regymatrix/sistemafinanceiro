using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class ContaBancariaCreateDto
    {
        [Required(ErrorMessage = "O nome da conta é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string ContaCliente { get; set; }

        [Required(ErrorMessage = "O numero da conta é obrigatória.")]
        public string NumeroConta { get; set; }

        [Required(ErrorMessage = "O tipo de conta é obrigatório.")]  
        public string TipoConta { get; set; }

        [Required(ErrorMessage = "O codigo da agencia é obrigatório.")]
        public string CodigoAgencia { get; set; }

        [Required(ErrorMessage = "O status da conta é obrigatório.")]
        public string StatusConta { get; set; }
    }
}
