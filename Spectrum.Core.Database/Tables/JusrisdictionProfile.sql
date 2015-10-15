CREATE TABLE [dbo].[JusrisdictionProfile]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [JurisdictionId] INT NOT NULL, 
    [Description] NVARCHAR(MAX) NULL

	CONSTRAINT [FK_JurisdictionProfile_Jurisdiction] FOREIGN KEY ([JurisdictionId]) REFERENCES [Jurisdiction]([Id]) ON DELETE CASCADE
)
