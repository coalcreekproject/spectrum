CREATE TABLE [dbo].[UserProfileAddressNorthAmerica]
(
	[UserProfileId] INT NOT NULL, 
    [AddressId] INT NOT NULL,
	 
    PRIMARY KEY CLUSTERED ([UserProfileId], [AddressId]), 
    CONSTRAINT [FK_UserProfileAddressNorthAmerica_UserProfile] FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserProfileAddressNorthAmerica_AddressNorthAmerica] FOREIGN KEY ([AddressId]) REFERENCES [AddressUnitedStates]([Id]) ON DELETE CASCADE

)
GO
CREATE NONCLUSTERED INDEX [IX_UserAddressNorthAmerica_UserProfileId] 
	ON [dbo].[UserProfileAddressNorthAmerica] ([UserProfileId])

GO
CREATE NONCLUSTERED INDEX [IX_UserAddressNorthAmerica_AddressId] 
	ON [dbo].[UserProfileAddressNorthAmerica] ([AddressId])
