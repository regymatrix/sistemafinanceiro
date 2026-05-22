using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class ContaBancariaCreateDto
    {
        [Required(ErrorMessage = "O código do cliente é obrigatório.")]
        public int CodigoCliente { get; set; }

        [Required(ErrorMessage = "O código do agência é obrigatório.")]
        public int CodigoAgencia { get; set; }

        [Required(ErrorMessage = "O número da conta é obrigatório.")]
        public string NumeroConta { get; set; }

        [Required(ErrorMessage = "O status da conta é obrigatório.")]
        public bool StatusConta { get; set; }

        [Required(ErrorMessage = "O tipo de conta é obrigatório.")]
        public string TipoConta { get; set; }
    }
}
