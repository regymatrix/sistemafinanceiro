
use dbFinanceiro
go

Create table FUNCIONARIO (

Codigo int identity(1,1) primary key,
Nome varchar(100),
DataNascimento date,
Cidade varchar(100),
EstadoUF varchar(100),
CPF varchar(20) not null,
Telefone varchar(20)
)
