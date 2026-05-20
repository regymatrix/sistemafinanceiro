using PrjFinanceiro.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrjFinanceiro.DTOs
{
    public class ContaBancariaCreateDto
    {
        [Required(ErrorMessage = "O nome da conta é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de abertura/nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O número da conta é obrigatório.")]
        public string NumeroConta { get; set; }

        [Required(ErrorMessage = "O tipo de conta é obrigatório.")]
        public string TipoConta { get; set; }

        public bool StatusConta { get; set; }

        public int? CodigoCliente { get; set; }
        public int CodigoAgencia { get; set; }
        [ForeignKey("CodigoAgencia")]
        public virtual Agencia Agencia { get; set; }
    }
}