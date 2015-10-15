CREATE TABLE [dbo].[Group]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL, 
    [OrganizationId] INT NOT NULL, 
    [Description] NVARCHAR(256) NULL
    
	CONSTRAINT [FK_Group_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id])
)
