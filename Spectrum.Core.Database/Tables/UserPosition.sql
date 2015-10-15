CREATE TABLE [dbo].[UserPosition]
(
	[UserId] INT NOT NULL, 
    [PositionId] INT NOT NULL,

	CONSTRAINT [PK_UserPosition_UserId_PositionId] PRIMARY KEY CLUSTERED ([UserId], [PositionId]), 
	CONSTRAINT [FK_UserPosition_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserPosition_Position] FOREIGN KEY ([PositionId]) REFERENCES [Position]([Id]) ON DELETE CASCADE
)
GO

CREATE NONCLUSTERED INDEX [IX_UserPosition_UserId] 
	ON [dbo].[UserPosition] ([UserId])

GO

CREATE NONCLUSTERED INDEX [IX_UserPosition_PositionId] 
	ON [dbo].[UserPosition] ([PositionId])