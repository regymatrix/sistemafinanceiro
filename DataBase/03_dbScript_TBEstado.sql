
use dbFinanceiro
go

CREATE TABLE Cidade (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    NomeCidade VARCHAR(100) NOT NULL,
    CodigoIBGE VARCHAR(7) NOT NULL,
    CodigoEstado INT,

    CONSTRAINT fk_Codigo_Estado
    FOREIGN KEY (CodigoEstado) REFERENCES Estado(Codigo)
);
CREATE TABLE Bairro (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    NomeBairro VARCHAR(100) NOT NULL,
    CodigoCidade INT
);