
use dbFinanceiro
go

Create table FUNCIONARIO (

Codigo int identity(1,1) primary key,
Nome varchar(100),
DataNascimento datetime,
Cidade varchar(100),
EstadoUF varchar(2),
CPF varchar (20),
Telefone varchar (20)
)

go
