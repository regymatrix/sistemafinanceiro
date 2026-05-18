using System;
using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class FuncionarioCreateDto
    {
        [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome não pode exceder 150 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres.")] // Permite com ou sem máscara
        public string CPF { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [StringLength(20, ErrorMessage = "O telefone não pode exceder 20 caracteres.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O Estado (UF) é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A UF deve conter exatamente 2 caracteres.")]
        public string EstadoUF { get; set; }

        [Required(ErrorMessage = "O código da cidade é obrigatório.")]
        public int CodigoCidade { get; set; }

        [Required(ErrorMessage = "O código do bairro é obrigatório.")]
        public int CodigoBairro { get; set; }

        [Required(ErrorMessage = "O código da escolaridade é obrigatório.")]
        public int CodigoEscolaridade { get; set; }

        [Required(ErrorMessage = "O código da etnia é obrigatório.")]
        public int CodigoEtnia { get; set; }
    }
}