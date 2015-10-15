CREATE TABLE [dbo].[UserRole]
(
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,

    CONSTRAINT [PK_UserRole_UserID_RoleID] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_UserRole] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]) ON DELETE CASCADE, 
);

GO
CREATE NONCLUSTERED INDEX [IX_UserRole_UserID]
    ON [dbo].[UserRole] ([UserId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_UserRole_RoleID]
    ON [dbo].[UserRole] ([RoleId] ASC);
