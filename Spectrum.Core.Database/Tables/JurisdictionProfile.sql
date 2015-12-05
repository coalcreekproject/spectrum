CREATE TABLE [dbo].[JurisdictionProfile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [JurisdictionId] INT NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,

	CONSTRAINT [FK_JurisdictionProfile_Jurisdiction] FOREIGN KEY ([JurisdictionId]) REFERENCES [Jurisdiction]([Id]) ON DELETE CASCADE
)
