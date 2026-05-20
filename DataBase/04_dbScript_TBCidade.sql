
use dbFinanceiro
go

Create table CLIENTE (

Codigo int identity(1,1) primary key,
Nome varchar(100) not null,
DataNascimento datetime not null,
tipoCliente varchar (100) not null, 
CPF varchar (11) not null, 
CNPJ varchar (14) not null,
CodigoBairro int 
CONSTRAINT fk_Codigo_Bairro
FOREIGN KEY (CodigoBairro) REFERENCES Bairro(Codigo) 
)

SELECT * FROM CLIENTE