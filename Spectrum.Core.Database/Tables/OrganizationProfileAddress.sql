CREATE TABLE [dbo].[OrganizationProfileAddress]
(
	[Id] INT NULL,
	[OrganizationProfileId] INT NOT NULL, 
    [AddressId] INT NOT NULL
	 
    PRIMARY KEY CLUSTERED ([OrganizationProfileId], [AddressId]), 
    CONSTRAINT [FK_OrganizationAddress_OrganizationProfile] FOREIGN KEY ([OrganizationProfileId]) REFERENCES [OrganizationProfile]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_OrganizatioProfilenAddress_Address] FOREIGN KEY ([AddressId]) REFERENCES [Address]([Id]) ON DELETE CASCADE
)

GO
CREATE NONCLUSTERED INDEX [IX_OrganizationProfileAddress_OrganizationId] 
	ON [dbo].[OrganizationProfileAddress] ([OrganizationProfileId])

GO
CREATE NONCLUSTERED INDEX [IX_OrganizationProfileAddress_AddressId] 
	ON [dbo].[OrganizationProfileAddress] ([AddressId])
