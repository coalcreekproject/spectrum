CREATE TABLE [dbo].[UserClaim]
(
    [ClaimId]    INT IDENTITY (1000, 1)    NOT NULL,
    [UserId]     INT         NOT NULL,
    [ClaimType]  NVARCHAR (MAX)        NULL,
    [ClaimValue] NVARCHAR (MAX)        NULL,

    CONSTRAINT [PK_UserClaim_ClaimID] PRIMARY KEY CLUSTERED ([ClaimId] ASC),
    CONSTRAINT [FK_UserClaim_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);
GO