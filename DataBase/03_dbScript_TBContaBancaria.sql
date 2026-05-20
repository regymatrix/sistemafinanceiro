USE dbFinanceiro
GO

CREATE TABLE ContaBancaria
(
    CodigoCliente INT IDENTITY(1,1) PRIMARY KEY,
	CodigoAgencia INT IDENTITY(1,1),
     NumeroConta int,
	 StatusConta bit,
	 TipoConta varchar(100)
)
GO
