
use dbFinanceiro
go

Create table Estado (

Codigo int identity(1,1) primary key,
NomeEstado varchar(100) not null,
Sigla varchar(2) not null
)

--INSERT Estado VALUES('Sergipe','SE')

SELECT * FROM Estado