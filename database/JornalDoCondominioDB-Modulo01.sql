CREATE DATABASE JornalDoCondominioDB
GO

USE JornalDoCondominioDB
GO

CREATE TABLE Usuarios
(
	Id INT PRIMARY KEY NOT NULL,
	DataCriacao DATETIME NOT NULL,
	DataAlteracao DATETIME NULL, 
	NomeUsuario VARCHAR(100) NOT NULL,
	EmailLogin VARCHAR(150) NOT NULL,
	PwdLogin VARCHAR(100) NOT NULL,
	Ativo BIT NOT NULL,
)
GO

-- INCLUSÁO DO USUÁRIO/JORNALISTA
INSERT INTO Usuarios (Id, DataCriacao, NomeUsuario, EmailLogin, PwdLogin, Ativo)
VALUES(1, GETDATE(), 'Paulo Manoel', 'paulo.manoel@aluno.faculdadeimpacta.com.br', 'fitpaulo', 1)
GO

CREATE TABLE Noticias
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titulo VARCHAR(200) NOT NULL,
    Texto NVARCHAR(MAX) NOT NULL,
    FotoPath VARCHAR(255) NULL,
    DataCriacao DATETIME NOT NULL,
    UsuarioCriacaoId INT NOT NULL,
    DataAlteracao DATETIME NULL,
    UsuarioAlteracaoId INT NULL,
    Ativo BIT NOT NULL,
    CONSTRAINT FK_Noticias_UsuarioCriacao FOREIGN KEY (UsuarioCriacaoId) REFERENCES Usuarios(Id),
    CONSTRAINT FK_Noticias_UsuarioAlteracao FOREIGN KEY (UsuarioAlteracaoId) REFERENCES Usuarios(Id)
)
GO