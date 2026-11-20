CREATE TABLE [dbo].[RefreshTokens] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [UsuarioId] BIGINT         NOT NULL,
    [Token]     NVARCHAR (500) NOT NULL,
    [ExpiraEm]  DATETIME2 (7)  NOT NULL,
    [Revogado]  BIT            DEFAULT ((0)) NOT NULL,
    [CriadoEm]  DATETIME       DEFAULT (getdate()) NOT NULL,
    [CriadoPor] BIGINT         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([Id])
);
GO

CREATE NONCLUSTERED INDEX [IX_RefreshTokens_Token]
    ON [dbo].[RefreshTokens]([Token] ASC);
GO

