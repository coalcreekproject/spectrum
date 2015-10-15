CREATE TABLE [dbo].[AddressInternational]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL, 
	[Primary] [dbo].[Flag] NULL,
    [Description] NVARCHAR(256) NULL, 
    [StreetOne] NVARCHAR(256) NOT NULL, 
    [StreetTwo] NVARCHAR(256) NULL, 
    [Country] NVARCHAR(128) NOT NULL, 
    [City] NVARCHAR(128) NOT NULL, 
    [PoliticalBoundary] NVARCHAR(128) NOT NULL, 
    [Postal Code] NVARCHAR(128) NOT NULL 
)
