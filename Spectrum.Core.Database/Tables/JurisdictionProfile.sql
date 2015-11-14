CREATE TABLE [dbo].[JurisdictionProfile]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [JurisdictionId] INT NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL,
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL, 
	[ModifiedByUserId]	   INT					  NULL

	CONSTRAINT [FK_JurisdictionProfile_Jurisdiction] FOREIGN KEY ([JurisdictionId]) REFERENCES [Jurisdiction]([Id]) ON DELETE CASCADE
)
