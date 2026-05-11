
use dbo.Escolaridade
go

Create table Escolaridade (

Codigo int identity(1,1) primary key,
Descrição varchar(100) not null,
)


SELECT * FROM Escolaridade