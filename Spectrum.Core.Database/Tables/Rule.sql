CREATE TABLE [dbo].[Rule]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [OrganizationId] INT NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(256) NULL, 
    [RuleTypeId] INT NOT NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL

    CONSTRAINT [FK_Rule_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE
	CONSTRAINT [FK_Rule_RuleType] FOREIGN KEY ([RuleTypeId]) REFERENCES [RuleType]([Id]) ON DELETE CASCADE
)
