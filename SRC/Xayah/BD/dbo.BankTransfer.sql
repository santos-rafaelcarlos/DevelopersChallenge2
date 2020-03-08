CREATE TABLE [dbo].[BankTransfer] (
    [Id]          INT            NOT NULL IDENTITY,
    [Type]        INT            NOT NULL,
    [Date]        DATETIME       NOT NULL,
    [Value]       FLOAT (53)     NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [LastUpdate]  DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

