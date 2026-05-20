-- Adiciona a coluna CodigoCidade e cria o relacionamento
use dbFinanceiro;

ALTER TABLE Funcionario ADD CodigoCidade INT;
ALTER TABLE Funcionario ADD CONSTRAINT FK_Funcionario_Cidade 
FOREIGN KEY (CodigoCidade) REFERENCES Cidade(Codigo);

-- Adiciona a coluna CodigoBairro e cria o relacionamento
ALTER TABLE Funcionario ADD CodigoBairro INT;
ALTER TABLE Funcionario ADD CONSTRAINT FK_Funcionario_Bairro 
FOREIGN KEY (CodigoBairro) REFERENCES Bairro(Codigo);

DELETE FROM FUNCIONARIO;
SELECT * FROM FUNCIONARIO;
