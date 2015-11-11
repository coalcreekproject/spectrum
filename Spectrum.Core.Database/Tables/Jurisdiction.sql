CREATE TABLE [dbo].[Jurisdiction]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL,
    [OrganizationId] INT NOT NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL

    CONSTRAINT [FK_Jurisdiction_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE
)
