CREATE TABLE [dbo].[UserPosition]
(
	[Id] INT NULL,
	[UserId] INT NOT NULL, 
    [PositionId] INT NOT NULL,
	[OrganizationId] INT NOT NULL

	CONSTRAINT [PK_UserPosition_UserId_PositionId] PRIMARY KEY CLUSTERED ([UserId], [PositionId]), 
	[Default] BIT NULL, 
    CONSTRAINT [FK_UserPosition_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserPosition_Position] FOREIGN KEY ([PositionId]) REFERENCES [Position]([Id]) ON DELETE CASCADE
)