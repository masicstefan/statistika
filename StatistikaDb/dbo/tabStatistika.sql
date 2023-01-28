CREATE TABLE [dbo].[tabStatistika]
(
	[IdStatistike] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DatumVpisa] DATE NOT NULL, 
    [ImeKlicaneStoritve] VARCHAR(250) NOT NULL
)
