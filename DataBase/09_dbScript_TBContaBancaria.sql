
use dbFinanceiro
go

Create table CONTABANCARIA (

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

go
ALTER TABLE CONTABANCARIA
DROP COLUMN codigo;
go
ALTER TABLE CONTABANCARIA
add codigo int identity(1,1)
go

ALTER TABLE CONTABANCARIA
ADD CONSTRAINT PK_CodigoContaBancaria PRIMARY KEY (codigo);