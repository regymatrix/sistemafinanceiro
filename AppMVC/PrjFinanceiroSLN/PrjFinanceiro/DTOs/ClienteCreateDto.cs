using System;
using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class ClienteCreateDto
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome não pode exceder 150 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento/fundação é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O tipo de cliente é obrigatório ('Física' ou 'Jurídica').")]
        public string TipoCliente { get; set; }

        public string CPF { get; set; }

        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O código do bairro é obrigatório.")]
        public int CodigoBairro { get; set; }
    }
}