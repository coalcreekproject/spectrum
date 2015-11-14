CREATE TABLE [dbo].[UserAreaOfResponsibility]
(
    [UserId] INT NOT NULL,
	[AreaOfResponsibilityId] INT NOT NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL

	CONSTRAINT [PK_UserAreaOfResponsibility_UserId_AreaOfResponsibilityId] PRIMARY KEY CLUSTERED ([UserId] ASC, [AreaOfResponsibilityId] ASC), 
    CONSTRAINT [FK_UserAreaOfResponsibility_Users] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE,  
    CONSTRAINT [FK_UserAreaOfResponsibility_AreaOfResponsibility] FOREIGN KEY ([AreaOfResponsibilityId]) REFERENCES [AreaOfResponsibility]([Id]) ON DELETE CASCADE
)

GO

CREATE NONCLUSTERED INDEX [IX_AreaOfResponsibility_UserId] 
ON [dbo].[UserAreaOfResponsibility] ([UserId])

GO
CREATE NONCLUSTERED INDEX [IX_AreaOfResponsibility_AreaOfResponsibility] 
ON [dbo].[UserAreaOfResponsibility] ([AreaOfResponsibilityId])

