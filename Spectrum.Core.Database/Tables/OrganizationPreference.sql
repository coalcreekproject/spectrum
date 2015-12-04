CREATE TABLE [dbo].[OrganizationPreference]
(
	[Id] INT NULL,
	[OrganizationId] INT NOT NULL, 
    [PreferenceId] INT NOT NULL,

	CONSTRAINT [PK_OrganizationPreference_OrganizationId_PrefernceId] PRIMARY KEY CLUSTERED ([OrganizationId], [PreferenceId]),
	CONSTRAINT [FK_OrganizationPreference_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_OrganizationPreference_Preference] FOREIGN KEY ([PreferenceId]) REFERENCES [Preference]([Id]) ON DELETE CASCADE
)

GO

CREATE NONCLUSTERED INDEX [IX_OrganizationPreference_OrganizationId] 
	ON [dbo].[OrganizationPreference] ([OrganizationId])

GO
CREATE NONCLUSTERED INDEX [IX_OrganizationPreference_PreferenceId] 
	ON [dbo].[OrganizationPreference] ([PreferenceId])
