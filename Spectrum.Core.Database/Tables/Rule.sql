CREATE TABLE [dbo].[Rule]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [OrganizationId] INT NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(256) NULL, 
    [RuleTypeId] INT NOT NULL

    CONSTRAINT [FK_Rule_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE
	CONSTRAINT [FK_Rule_RuleType] FOREIGN KEY ([RuleTypeId]) REFERENCES [RuleType]([Id]) ON DELETE CASCADE
)
