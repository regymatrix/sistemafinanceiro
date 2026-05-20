using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class ContaBancariaCreateDto
    {
        [Required(ErrorMessage = "O número da conta é obrigatório.")]
        public int NumeroConta { get; set; }

        [Required(ErrorMessage = "O código do cliente (titular) é obrigatório.")]
        public int CodigoCliente { get; set; }

        [Required(ErrorMessage = "O código da agência é obrigatório.")]
        public int CodigoAgencia { get; set; }

        [Required(ErrorMessage = "O status da conta é obrigatório.")]
        public bool StatusConta { get; set; }

        [Required(ErrorMessage = "O tipo da conta é obrigatório.")]
        [StringLength(50, ErrorMessage = "O tipo de conta não pode exceder 50 caracteres.")]
        public string TipoConta { get; set; }
    }
}