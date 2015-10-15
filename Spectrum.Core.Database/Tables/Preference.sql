CREATE TABLE [dbo].[Preference]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(256) NULL, 
    [Value] NVARCHAR(MAX) NOT NULL 
)
