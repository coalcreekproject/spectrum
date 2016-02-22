CREATE TABLE [dbo].[UserJurisdiction]
(
	[Id] INT NULL,
    [UserId] INT NOT NULL,
	[JurisdictionId] INT NOT NULL

	CONSTRAINT [PK_UserJurisdiction_UserId_JurisdictionId] PRIMARY KEY CLUSTERED ([UserId] ASC, [JurisdictionId] ASC), 
    CONSTRAINT [FK_UserJurisdiction_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE,  
    CONSTRAINT [FK_UserJurisdiction_Jurisdiction] FOREIGN KEY ([JurisdictionId]) REFERENCES [Jurisdiction]([Id]) ON DELETE CASCADE
)