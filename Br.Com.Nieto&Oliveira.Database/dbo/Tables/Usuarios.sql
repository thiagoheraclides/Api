CREATE TABLE [dbo].[Usuarios] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Nome]      NVARCHAR (200) NOT NULL,
    [Email]     NVARCHAR (200) NOT NULL,
    [Senha]     NVARCHAR (500) NOT NULL,
    [Role]      CHAR (3)       DEFAULT ('USR') NOT NULL,
    [Ativo]     BIT            DEFAULT ((1)) NOT NULL,
    [CriadoEm]  DATETIME       DEFAULT (getdate()) NOT NULL,
    [CriadoPor] BIGINT         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC)
);
GO

