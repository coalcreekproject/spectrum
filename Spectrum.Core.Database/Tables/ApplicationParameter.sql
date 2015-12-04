CREATE TABLE [dbo].[ApplicationParameter]
(
	[Id] INT NULL,
	[ApplicationId] INT NOT NULL, 
    [ParameterId] INT NOT NULL,

	CONSTRAINT [PK_ApplicationParameter_Application_Parameter] PRIMARY KEY CLUSTERED ([ApplicationId], [ParameterId]), 
    CONSTRAINT [FK_ApplicationParameter_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_ApplicationParameter_Parameter] FOREIGN KEY ([ParameterId]) REFERENCES [Parameter]([Id]) ON DELETE CASCADE
)
GO