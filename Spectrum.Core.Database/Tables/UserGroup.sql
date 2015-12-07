CREATE TABLE [dbo].[UserGroup]
(
	[Id] INT NULL,
    [UserId] INT NOT NULL,
	[GroupId] INT NOT NULL

	CONSTRAINT [PK_UserGroup_UserId_GroupId] PRIMARY KEY CLUSTERED ([UserId] ASC, [GroupId] ASC), 
    CONSTRAINT [FK_UserGroup_Users] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserGroup_Group] FOREIGN KEY ([GroupId]) REFERENCES [Group]([Id]) ON DELETE CASCADE
)

GO