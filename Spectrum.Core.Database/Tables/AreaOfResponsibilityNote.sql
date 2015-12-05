CREATE TABLE [dbo].[AreaOfResponsibilityNote]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1),
	[AreaOfResponsibilityId] INT NOT NULL,
	[Note] NVARCHAR(MAX) NOT NULL

    CONSTRAINT [FK_AreaOfResponsibilityNotes_AreaOfResponsibility] FOREIGN KEY ([AreaOfResponsibilityId]) REFERENCES [AreaOfResponsibility]([Id]) ON DELETE CASCADE
)


