CREATE TABLE [dbo].[Group]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [Name] NVARCHAR(128) NOT NULL, 
    [OrganizationId] INT NOT NULL, 
    [Description] NVARCHAR(256) NULL

	CONSTRAINT [FK_Group_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id])
)
