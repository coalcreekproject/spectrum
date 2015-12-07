CREATE TABLE [dbo].[OrganizationLicense]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [Name] NVARCHAR(256) NOT NULL, 
    [Application] INT NOT NULL, 
    [Key] NVARCHAR(MAX) NOT NULL
)
