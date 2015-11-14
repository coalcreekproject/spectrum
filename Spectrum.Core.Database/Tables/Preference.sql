CREATE TABLE [dbo].[Preference]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(256) NULL, 
    [Value] NVARCHAR(MAX) NOT NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL,
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL, 
	[ModifiedByUserId]	   INT					  NULL
)
