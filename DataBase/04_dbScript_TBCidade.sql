
use dbFinanceiro
go

Create table CIDADE (

Codigo int identity(1,1) primary key,
NomeCidade varchar(100) not null,
CodigoIBGE varchar(10) not null,
CodigoEstado int, 
CONSTRAINT fk_Codigo_Estado
FOREIGN KEY (CodigoEstado) REFERENCES Estado(Codigo) 
)

--INSERT Estado VALUES('Sergipe','SE')

SELECT * FROM CIDADE