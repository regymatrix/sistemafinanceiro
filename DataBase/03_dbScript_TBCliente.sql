USE dbFinanceiro
GO

CREATE TABLE Cliente
(
    Codigo INT IDENTITY(1,1) PRIMARY KEY,

    Nome varchar (500) NOT NULL,
	DataNascimento datetime,
	TipoCliente varchar (100),
	CPF varchar (100),
	CNPJ varchar (100),
	CodigoBairro Int
)
GO
