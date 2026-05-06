
use dbFinanceiro
go

Create table Cidade (

Codigo int identity(1,1) primary key,
NomeCidade varchar(100) not null,
CodigoIBGE int not null,
CodigoEstado int
FOREIGN KEY (CodigoEstado) REFERENCES Estado(Codigo)
)

--INSERT Cidade VALUES('Estancia','SE')

SELECT * FROM Cidade

