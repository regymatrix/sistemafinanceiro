using System;
using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.Models
{
    public class CodigoEstado
    {
        [Key]
        public int Codigo { get; set; }
        public string NomeEstado { get; set; }
        public string Sigla { get; set; }
    }
}
