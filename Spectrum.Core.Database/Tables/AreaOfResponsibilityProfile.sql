CREATE TABLE [dbo].[AreaOfResponsibilityProfile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1),
    [AreaOfResponsibilityId] INT NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL,
    [Description] NVARCHAR(256) NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL,
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL, 
	[ModifiedByUserId]	   INT					  NULL

    CONSTRAINT [FK_AreaOfResponsibilityProfile_AreaOfResponsibility] FOREIGN KEY ([AreaOfResponsibilityId]) REFERENCES [AreaOfResponsibility]([Id]) ON DELETE CASCADE
)
