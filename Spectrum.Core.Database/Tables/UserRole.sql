CREATE TABLE [dbo].[UserRole]
(
	[Id] INT NULL,
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
	[OrganizationId] INT NOT NULL

    CONSTRAINT [PK_UserRole_UserID_RoleId] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organization] ([Id]), 	 
);