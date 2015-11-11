CREATE TABLE [dbo].[AreaOfResponsibilityProfile]
(
	[Id] INT NOT NULL PRIMARY KEY,
    [AreaOfResponsibilityId] INT NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL

	CONSTRAINT [FK_AreaOfResponsibilityProfile_AreaOfResponsibility] FOREIGN KEY ([AreaOfResponsibilityId]) REFERENCES [AreaOfResponsibility]([Id]) ON DELETE CASCADE
)
