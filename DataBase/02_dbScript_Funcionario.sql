CREATE DATABASE dbFinanceiro
go
use dbFinanceiro
go

Create table AGENCIA (

Codigo int identity(1,1) primary key,
Nome varchar(100),
Cidade varchar(100),
EstadoUF varchar(100)
)

go
--select * from agencia

--teste
INSERT AGENCIA (NOME,CIDADE,EstadoUF) VALUES ('BB Estância', 'Estância','SE')