use dbFinanceiro
go

Create table CONTABANCARIA(

CodigoCliente int not null,
CONSTRAINT fk_Codigo_Cliente
FOREIGN KEY (CodigoCliente) REFERENCES Cliente(Codigo),
CodigoAgencia int not null,
CONSTRAINT fk_Codigo_Agencia
FOREIGN KEY (CodigoAgencia) REFERENCES Agencia(Codigo),
NumeroConta varchar(100) not null,
StatusConta bit not null,
TipoConta varchar (100) not null

)

SELECT * FROM CONTABANCARIA