﻿CREATE TABLE [dbo].[JusrisdictionProfile]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [JurisdictionId] INT NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL

	CONSTRAINT [FK_JurisdictionProfile_Jurisdiction] FOREIGN KEY ([JurisdictionId]) REFERENCES [Jurisdiction]([Id]) ON DELETE CASCADE
)
