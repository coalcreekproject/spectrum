CREATE TABLE [dbo].[OrganizationAddressNorthAmerica]
(
	[OrganizationId] INT NOT NULL, 
    [AddressId] INT NOT NULL
	 
    PRIMARY KEY CLUSTERED ([OrganizationId], [AddressId]), 
    CONSTRAINT [FK_OrganizationAddressNorthAmerica_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_OrganizationAddressNorthAmerica_AddressNorthAmerica] FOREIGN KEY ([AddressId]) REFERENCES [AddressNorthAmerica]([Id]) ON DELETE CASCADE
)

GO

CREATE NONCLUSTERED INDEX [IX_OrganizationAddressNorthAmerica_OrganizationId] 
	ON [dbo].[OrganizationAddressNorthAmerica] ([OrganizationId])

GO
CREATE NONCLUSTERED INDEX [IX_OrganizationAddressNorthAmerica_AddressId] 
	ON [dbo].[OrganizationAddressNorthAmerica] ([AddressId])
