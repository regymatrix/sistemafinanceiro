using PrjFinanceiro.Models;
using System;

public class Funcionario
{
    public int Codigo { get; set; }

    public string Nome { get; set; }

    public DateTime DataNascimento { get; set; }

    public string Cidade { get; set; }

    public string EstadoUF { get; set; }

    public string CPF { get; set; }

    public string Telefone { get; set; }


    public int CodigoEscolaridade { get; set; }
    public Escolaridade Escolaridade { get; set; }


    public int CodigoEtnia { get; set; }
    public Etnia Etnia { get; set; }
}