
use dbFinanceiro
go

Create table Cidade (

Codigo int identity(1,1) primary key,
NomeCidade varchar(100) not null,
CodigoIBGE varchar(7) not null,
CodigoEstado int, 
CONSTRAINT fk_Codigo_Estado
FOREIGN KEY (CodigoEstado) REFERENCES Estado(Codigo)
)
