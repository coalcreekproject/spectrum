CREATE TABLE [dbo].[UserProfile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [UserId] INT NOT NULL,
	[OrganizationId] INT NULL,
	[Default] dbo.Flag NULL,
	[ProfileName] NVARCHAR(100) NULL,
    [Title] NVARCHAR(100) NULL, 
    [FirstName] NVARCHAR(100) NULL, 
    [MiddleName] NVARCHAR(100) NULL, 
    [LastName] NVARCHAR(100) NULL, 
    [Nickname] NVARCHAR(100) NULL, 
    [SecondaryEmail] [dbo].[Email] NULL, 
    [SecondaryPhoneNumber] [dbo].[Phone] NULL,
    [TimeZone] NVARCHAR(100) NULL, 
    [DstAdjust] BIT NULL, 
    [Language] NVARCHAR(100) NULL,
	[Photo] VARBINARY(MAX) NULL, 
    [Position] NVARCHAR(100) NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL,
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL, 
	[ModifiedByUserId]	   INT					  NULL
     
	CONSTRAINT [FK_UserProfile_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE,
)
