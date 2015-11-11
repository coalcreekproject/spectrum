﻿CREATE TABLE [dbo].[Group]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL, 
    [OrganizationId] INT NOT NULL, 
    [Description] NVARCHAR(256) NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL
    
	CONSTRAINT [FK_Group_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id])
)
