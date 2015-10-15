CREATE TABLE [dbo].[UserAddressNorthAmerica]
(
	[UserId] INT NOT NULL, 
    [AddressId] INT NOT NULL
	 
    PRIMARY KEY CLUSTERED ([UserId], [AddressId]), 
    CONSTRAINT [FK_UserAddressNorthAmerica_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserAddressNorthAmerica_AddressNorthAmerica] FOREIGN KEY ([AddressId]) REFERENCES [AddressNorthAmerica]([Id]) ON DELETE CASCADE

)

GO

CREATE NONCLUSTERED INDEX [IX_UserAddressNorthAmerica_UserId] 
	ON [dbo].[UserAddressNorthAmerica] ([UserId])

GO
CREATE NONCLUSTERED INDEX [IX_UserAddressNorthAmerica_AddressId] 
	ON [dbo].[UserAddressNorthAmerica] ([AddressId])
