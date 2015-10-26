CREATE TABLE [dbo].[Contact]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1),
    [UserId] INT NOT NULL,
	[OrganizationId] INT NULL,
    [Title] NVARCHAR(100) NULL, 
    [FirstName] NVARCHAR(100) NULL, 
    [MiddleName] NVARCHAR(100) NULL, 
    [LastName] NVARCHAR(100) NULL, 
    [Nickname] NVARCHAR(100) NULL, 
    [PrimaryEmail] [dbo].[Email] NULL, 
    [PrimaryPhoneNumber] [dbo].[Phone] NULL,
    [SecondaryEmail] [dbo].[Email] NULL, 
    [SecondaryPhoneNumber] [dbo].[Phone] NULL,
    [TimeZone] NVARCHAR(100) NULL, 
    [Language] NVARCHAR(100) NULL,
	[Note] NVARCHAR(MAX) NULL,
	[Photo] VARBINARY(MAX) NULL, 
    [Position] NVARCHAR(100) NULL
)