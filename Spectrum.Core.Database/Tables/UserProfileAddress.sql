CREATE TABLE [dbo].[UserProfileAddress]
(
	[Id] INT NULL,
	[UserProfileId] INT NOT NULL, 
    [AddressId] INT NOT NULL,
	 
    PRIMARY KEY CLUSTERED ([UserProfileId], [AddressId]), 
    CONSTRAINT [FK_UserProfileAddress_UserProfile] FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserProfileAddress_Address] FOREIGN KEY ([AddressId]) REFERENCES [Address]([Id]) ON DELETE CASCADE

)
GO