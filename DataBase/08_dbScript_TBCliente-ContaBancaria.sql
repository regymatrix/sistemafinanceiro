-- Novas Tabelas

use dbFinanceiro;

CREATE TABLE Cliente (
    Codigo INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
	DataNascimento datetime not null,
	Tipo VARCHAR(100) NOT NULL,
	CPF VARCHAR(11) NOT NULL,
	CNPJ VARCHAR(100) NOT NULL,
	CodigoBairro int,

	CONSTRAINT FK_Func_Bairro FOREIGN KEY (CodigoBairro) REFERENCES Bairro(Codigo)
	);

CREATE TABLE ContaBancaria (
    Codigo INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(100) NOT NULL,
	DataNascimento datetime not null,
	NumeroConta VARCHAR(100) NOT NULL,
	TipoConta VARCHAR(100) NOT NULL,
	StatusConta bit not null,
	CodigoCliente int,
	CodigoAgencia int,

	CONSTRAINT FK_Func_Cliente FOREIGN KEY (CodigoCliente) REFERENCES Cliente(Codigo),
	CONSTRAINT FK_Func_Agencia FOREIGN KEY (CodigoAgencia) REFERENCES Agencia(Codigo)
);