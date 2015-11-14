CREATE TABLE [dbo].[OrganizationProfileAddressUnitedStates]
(
	[OrganizationProfileId] INT NOT NULL, 
    [AddressId] INT NOT NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL
	 
    PRIMARY KEY CLUSTERED ([OrganizationProfileId], [AddressId]), 
    CONSTRAINT [FK_OrganizationAddressUnitedStates_OrganizationProfile] FOREIGN KEY ([OrganizationProfileId]) REFERENCES [OrganizationProfile]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_OrganizatioProfilenAddressUnitedStates_AddressUnitedStates] FOREIGN KEY ([AddressId]) REFERENCES [AddressUnitedStates]([Id]) ON DELETE CASCADE
)

GO
CREATE NONCLUSTERED INDEX [IX_OrganizationProfileAddressUnitedStates_OrganizationId] 
	ON [dbo].[OrganizationProfileAddressUnitedStates] ([OrganizationProfileId])

GO
CREATE NONCLUSTERED INDEX [IX_OrganizationProfileAddressUnitedStates_AddressId] 
	ON [dbo].[OrganizationProfileAddressUnitedStates] ([AddressId])
