CREATE TABLE [dbo].[User]
(
    [Id]				   INT		  IDENTITY(1000, 1)    NOT NULL,
    [UserName]             [dbo].[Name]           NOT NULL,
    [Email]                [dbo].[Email]          NULL,
    [EmailConfirmed]       [dbo].[Flag]           NOT NULL,
    [PasswordHash]         NVARCHAR (128)         NULL,
    [SecurityStamp]        NVARCHAR (128)         NULL,
    [PhoneNumber]          [dbo].[Phone]          NULL,
    [PhoneNumberConfirmed] [dbo].[Flag]           NOT NULL,
    [TwoFactorEnabled]     [dbo].[Flag]           NOT NULL,
    [LockoutEndDateUtc]    DATETIME               NULL,
    [LockoutEnabled]       [dbo].[Flag]           NOT NULL,
    [AccessFailedCount]    INT                    NOT NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL,
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL, 
	[ModifiedByUserId]	   INT					  NULL,

    CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_User_UserName] UNIQUE NONCLUSTERED ([UserName] ASC)

	--Note: when tables are created code forst they seem to lose their constraints
	--ALTER TABLE [SpectrumEm.Web.Identity].[dbo].[User] ADD CONSTRAINT UK_UserName UNIQUE (UserName)

);
