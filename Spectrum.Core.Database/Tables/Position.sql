CREATE TABLE [dbo].[Position]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [OrganizationId] NVARCHAR(100) NULL, 
    [Name] NVARCHAR(100) NULL, 
    [Description] NVARCHAR(100) NULL, 
    [Value] NVARCHAR(100) NULL
)
