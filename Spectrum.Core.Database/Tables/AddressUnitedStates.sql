CREATE TABLE [dbo].[AddressUnitedStates]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL, 
	[Default] [dbo].[Flag] NOT NULL,
    [Description] NVARCHAR(256) NULL, 
    [StreetOne] NVARCHAR(256) NOT NULL, 
    [StreetTwo] NVARCHAR(256) NULL, 
    [City] NVARCHAR(128) NOT NULL, 
    [State] NVARCHAR(2) NOT NULL, 
    [Zip] NVARCHAR(10) NOT NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL
)
