﻿CREATE TABLE [dbo].[ApplicationProfile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [ApplicationId] INT NOT NULL, 
    [Description] NVARCHAR(MAX) NULL,
	[Company] NVARCHAR(128) NOT NULL,
	[Author] NVARCHAR(128) NOT NULL,
	[License] NVARCHAR(256),
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL
    
	CONSTRAINT [FK_ApplicationDetail_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id])
)
