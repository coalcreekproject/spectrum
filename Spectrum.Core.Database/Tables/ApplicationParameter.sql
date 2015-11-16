CREATE TABLE [dbo].[ApplicationParameter]
(
	[ApplicationId] INT NOT NULL, 
    [ParameterId] INT NOT NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL,
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL, 
	[ModifiedByUserId]	   INT					  NULL

	CONSTRAINT [PK_ApplicationParameter_Application_Parameter] PRIMARY KEY CLUSTERED ([ApplicationId], [ParameterId]), 
    CONSTRAINT [FK_ApplicationParameter_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_ApplicationParameter_Parameter] FOREIGN KEY ([ParameterId]) REFERENCES [Parameter]([Id]) ON DELETE CASCADE
)

GO

CREATE NONCLUSTERED INDEX [IX_ApplicationParameter_ApplicationId] 
	ON [dbo].[ApplicationParameter] ([ApplicationId])

GO
CREATE NONCLUSTERED INDEX [IX_ApplicationParameter_ParameterId] 
	ON [dbo].[ApplicationParameter] ([ParameterId])
