CREATE TABLE [dbo].[Jurisdiction]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL,
    [OrganizationId] INT NOT NULL 

    CONSTRAINT [FK_Jurisdiction_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE
)
