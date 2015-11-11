CREATE TABLE [dbo].[OrganizationApplications]
(
	[OrganizationId] INT NOT NULL, 
    [ApplicationId] INT NOT NULL,
    [Key] NVARCHAR(256) NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL	
  
    CONSTRAINT [PK_OrganizationApplications_OrganizationId_ApplicationId] PRIMARY KEY CLUSTERED ([OrganizationId], [ApplicationId]), 
    CONSTRAINT [FK_OrganizationApplications_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_OrganizationApplications_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id]) ON DELETE CASCADE,

)

GO

CREATE NONCLUSTERED INDEX [IX_OrganizationApplications_OrganizationId] 
	ON [dbo].[OrganizationApplications] ([OrganizationId])

GO
CREATE NONCLUSTERED INDEX [IX_OrganizationApplications_ApplicationId] 
	ON [dbo].[OrganizationApplications] ([ApplicationId])
