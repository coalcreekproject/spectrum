CREATE TABLE [dbo].[Message]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Subject] NVARCHAR(256) NULL, 
    [Urgent] BIT NULL, 
    [To] NVARCHAR(128) NULL, 
    [From] NVARCHAR(256) NULL,
    [Body] NVARCHAR(MAX) NULL, 
    [EncodingType] NVARCHAR(128) NULL,
    [EmailRelay] NVARCHAR(256) NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[ModifiedByUserId]	   INT					  NULL
)
