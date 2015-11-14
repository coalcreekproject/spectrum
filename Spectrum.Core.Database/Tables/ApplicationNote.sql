CREATE TABLE [dbo].[ApplicationNote]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ApplicationId] INT NOT NULL,
	[Note] NVARCHAR(MAX) NOT NULL

    CONSTRAINT [FK_ApplicationNotes_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id]) ON DELETE CASCADE
)
