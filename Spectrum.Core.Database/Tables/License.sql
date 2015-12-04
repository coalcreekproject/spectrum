CREATE TABLE [dbo].[License]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [OrganizationId] INT NOT NULL,
	[ApplicationId] INT NOT NULL, 
    [Key] INT NOT NULL
)
