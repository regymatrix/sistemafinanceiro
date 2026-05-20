use dbFinanceiro;

CREATE TABLE Cliente (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(150) NOT NULL,
    DataNascimento DATETIME NOT NULL,
    TipoCliente VARCHAR(20) NOT NULL, -- Ex: "Física" ou "Jurídica"
    CPF VARCHAR(14) NULL,
    CNPJ VARCHAR(18) NULL,
    CodigoBairro INT NOT NULL,
    CONSTRAINT FK_Cliente_Bairro FOREIGN KEY (CodigoBairro) REFERENCES Bairro(Codigo)
);
