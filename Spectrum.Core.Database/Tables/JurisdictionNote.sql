CREATE TABLE [dbo].[JurisdictionNote]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1),
	[JurisdictionId] INT NOT NULL,
	[Note] NVARCHAR(MAX) NOT NULL

    CONSTRAINT [FK_JurisdictionNotes_Jurisdiction] FOREIGN KEY ([JurisdictionId]) REFERENCES [Jurisdiction]([Id]) ON DELETE CASCADE

)
