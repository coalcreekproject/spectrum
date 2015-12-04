CREATE TABLE [dbo].[UserAreaOfResponsibility]
(
	[Id] INT NULL,
    [UserId] INT NOT NULL,
	[AreaOfResponsibilityId] INT NOT NULL,

	CONSTRAINT [PK_UserAreaOfResponsibility_UserId_AreaOfResponsibilityId] PRIMARY KEY CLUSTERED ([UserId] ASC, [AreaOfResponsibilityId] ASC), 
    CONSTRAINT [FK_UserAreaOfResponsibility_Users] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE,  
    CONSTRAINT [FK_UserAreaOfResponsibility_AreaOfResponsibility] FOREIGN KEY ([AreaOfResponsibilityId]) REFERENCES [AreaOfResponsibility]([Id]) ON DELETE CASCADE
)
GO