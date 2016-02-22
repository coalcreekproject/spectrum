CREATE TABLE [dbo].[AreaOfResponsibility]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[OrganizationId] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(256) NULL

	CONSTRAINT [FK_AreaOfResponsibility_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE
)
