CREATE TABLE [dbo].[ApplicationProfile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [ApplicationId] INT NOT NULL, 
    [Description] NVARCHAR(MAX) NULL,
	[Company] NVARCHAR(128) NOT NULL,
	[Author] NVARCHAR(128) NOT NULL,
	[License] NVARCHAR(256)
    
	CONSTRAINT [FK_ApplicationDetail_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id])
)
