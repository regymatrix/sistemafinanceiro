using System;
using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class ClienteCreateDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Tipo é obrigatório")]
        public string Tipo { get; set; }

        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public int? CodigoBairro { get; set; }
    }
}