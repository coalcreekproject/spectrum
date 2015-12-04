CREATE TABLE [dbo].[UserPreference]
(
	[Id] INT NULL,
	[UserId] INT NOT NULL, 
    [PreferenceId] INT NOT NULL,

	CONSTRAINT [PK_UserPreference_UserId_PrefernceId] PRIMARY KEY CLUSTERED ([UserId], [PreferenceId]), 
	CONSTRAINT [FK_UserPreference_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserPreference_Preference] FOREIGN KEY ([PreferenceId]) REFERENCES [Preference]([Id]) ON DELETE CASCADE


)
GO