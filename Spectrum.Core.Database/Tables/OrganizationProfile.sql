CREATE TABLE [dbo].[OrganizationProfile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [OrganizationId] INT NOT NULL,
	[Default] [dbo].[Flag] NOT NULL,
    [ProfileName] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL,
	[PrimaryContact] NVARCHAR(MAX) NULL,
    [Phone] [dbo].[Phone] NULL, 
    [Fax] [dbo].[Phone] NULL, 
 	[Email] [dbo].[Email] NULL, 
    [Country] NVARCHAR(256) NULL, 
    [County] NVARCHAR(256) NULL, 
    [TimeZone] NVARCHAR(100) NULL, 
    [DstAdjust] BIT NULL, 
    [Language] NVARCHAR(100) NULL,
	[Notes] NVARCHAR(MAX) NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL,
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL, 
	[ModifiedByUserId]	   INT					  NULL

    CONSTRAINT [FK_OrganizationProfile_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE
)
