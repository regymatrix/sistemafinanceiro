CREATE TABLE ContaBancaria (
    NumeroConta INT PRIMARY KEY, -- Usando o próprio número da conta como Chave Primária
    CodigoCliente INT NOT NULL,
    CodigoAgencia INT NOT NULL,
    StatusConta BIT NOT NULL DEFAULT 1, -- 1 para Ativa, 0 para Inativa
    TipoConta VARCHAR(50) NOT NULL, -- Ex: "Corrente", "Poupança", "Salário"
    CONSTRAINT FK_ContaBancaria_Cliente FOREIGN KEY (CodigoCliente) REFERENCES Cliente(Codigo)
);