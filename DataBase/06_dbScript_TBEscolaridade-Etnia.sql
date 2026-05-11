-- Novas Tabelas

use dbFinanceiro;

CREATE TABLE Escolaridade (
    Codigo INT PRIMARY KEY IDENTITY(1,1),
    Descricao VARCHAR(100) NOT NULL
);

CREATE TABLE Etnia (
    Codigo INT PRIMARY KEY IDENTITY(1,1),
    Descricao VARCHAR(100) NOT NULL
);

-- Atualizando a Tabela de Funcionário
-- Removendo colunas antigas se necessário e adicionando as FKs
ALTER TABLE Funcionario ADD CodigoEscolaridade INT;
ALTER TABLE Funcionario ADD CodigoEtnia INT;

-- Adicionando os Relacionamentos
ALTER TABLE Funcionario ADD CONSTRAINT FK_Func_Escolaridade FOREIGN KEY (CodigoEscolaridade) REFERENCES Escolaridade(Codigo);
ALTER TABLE Funcionario ADD CONSTRAINT FK_Func_Etnia FOREIGN KEY (CodigoEtnia) REFERENCES Etnia(Codigo);