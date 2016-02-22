CREATE TABLE [dbo].[Jurisdiction]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [Name] NVARCHAR(128) NOT NULL,
    [OrganizationId] INT NOT NULL

    CONSTRAINT [FK_Jurisdiction_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE
)
