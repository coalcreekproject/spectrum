CREATE TABLE [dbo].[Preference]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [Name] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(256) NULL, 
    [Value] NVARCHAR(MAX) NOT NULL,
)
