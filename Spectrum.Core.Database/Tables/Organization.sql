CREATE TABLE [dbo].[Organization]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [Name] NVARCHAR(128) NOT NULL, 
    [OrganizationTypeId] INT NULL

    CONSTRAINT [FK_Organization_OrganizationType] FOREIGN KEY ([OrganizationTypeId]) REFERENCES [OrganizationType]([Id]) 
)
