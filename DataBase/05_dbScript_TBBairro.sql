

use dbFinanceiro;
go

Create table BAIRRO (

Codigo int identity(1,1) primary key,
NomeBairro varchar(100) not null,
CodigoCidade int not null
CONSTRAINT FK_Codigo_Cidade
FOREIGN KEY (CodigoCidade) REFERENCES Cidade (codigo)
)

--INSERT Estado VALUES('Sergipe','SE')

SELECT * FROM BAIRRO