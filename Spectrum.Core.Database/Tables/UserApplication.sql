CREATE TABLE [dbo].[UserApplication]
(
	[UserId] INT NOT NULL, 
    [ApplicationId] INT NOT NULL,
	[Key] NVARCHAR(128) NULL, 
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL

    CONSTRAINT [PK_UserApplication_UserId_ApplicationId] PRIMARY KEY CLUSTERED ([UserId] ASC, [ApplicationId] ASC), 
	CONSTRAINT [FK_UserApplication_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserApplication_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id]) ON DELETE CASCADE
)

GO

CREATE NONCLUSTERED INDEX [IX_UserApplication_UserId] 
	ON [dbo].[UserApplication] ([UserId])

GO
CREATE NONCLUSTERED INDEX [IX_UserApplication_ApplicationId] 
	ON [dbo].[UserApplication] ([ApplicationId])
