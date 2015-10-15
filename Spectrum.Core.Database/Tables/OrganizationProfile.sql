CREATE TABLE [dbo].[OrganizationProfile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [OrganizationId] INT NOT NULL,
	[Default] [dbo].[Flag] NOT NULL,
    [ProfileName] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [StreetAddressOne] NVARCHAR(MAX) NULL, 
    [StreetAddressTwo] NVARCHAR(MAX) NULL, 
    [City] NVARCHAR(MAX) NULL, 
    [State] NVARCHAR(50) NULL, 
    [Zip] NVARCHAR(15) NULL, 
    [Phone] [dbo].[Phone] NULL, 
    [Fax] [dbo].[Phone] NULL, 
 	[Email] [dbo].[Email] NULL, 
    [Country] NVARCHAR(256) NULL, 
    [County] NVARCHAR(256) NULL, 
    [TimeZone] NVARCHAR(100) NULL, 
    [DstAdjust] BIT NULL, 
    [Language] NVARCHAR(100) NULL
	 
    CONSTRAINT [FK_OrganizationProfile_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE
)
