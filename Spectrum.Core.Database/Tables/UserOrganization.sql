CREATE TABLE [dbo].[UserOrganization]
(
	[Id] INT NULL,
    [UserId] INT NOT NULL, 
	[OrganizationId] INT NOT NULL,
	 
    CONSTRAINT [PK_UserOrganization_User_Organization] PRIMARY KEY CLUSTERED ([OrganizationId], [UserId]), 
    CONSTRAINT [FK_UserOrganization_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserOrganization_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE
)
GO
