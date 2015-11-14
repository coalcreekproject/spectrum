CREATE TABLE [dbo].[OrganizationNote]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[OrganizationId] INT NOT NULL,
	[Note] NVARCHAR(MAX) NOT NULL

    CONSTRAINT [FK_OrganizationNotes_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE
)
