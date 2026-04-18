
use dbFinanceiro
go

Create table Funcionario (

Codigo int identity(1,1) primary key,
Nome varchar(100),
DataNascimento datetime,
Cidade varchar(100),
EstadoUF varchar(100)
CPF varcha(20) not null,
Telefone varchar(20),
)

