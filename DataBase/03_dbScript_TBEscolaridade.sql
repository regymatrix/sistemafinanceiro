
use dbFinanceiro
go

Create table Escolaridade (

Codigo int identity(1,1) primary key,
Descricao varchar(100) not null
)

SELECT * FROM Escolaridade