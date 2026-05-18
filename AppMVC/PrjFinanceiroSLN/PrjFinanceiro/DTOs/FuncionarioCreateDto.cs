using System;
using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class FuncionarioCreateDto
    {
        [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data de nascimento inválida.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(100, ErrorMessage = "O nome da cidade não pode exceder 100 caracteres.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado/UF é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A UF deve conter exatamente 2 caracteres.")]
        public string EstadoUF { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"\d{3}\.\d{3}\.\d{3}-\d{2}", ErrorMessage = "CPF inválido. Formato esperado: XXX.XXX.XXX-XX")]
        public string CPF { get; set; }

        [Phone(ErrorMessage = "Telefone inválido.")]
        public string Telefone { get; set; }
    }
}