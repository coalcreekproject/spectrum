CREATE TABLE [dbo].[Parameter]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Key] NVARCHAR(128) NOT NULL, 
    [Value] NVARCHAR(128) NOT NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL
)
