CREATE TABLE [dbo].[OrganizationApplications]
(
	[OrganizationId] INT NOT NULL, 
    [ApplicationId] INT NOT NULL,
    [Key] NVARCHAR(256) NOT NULL,
  
    CONSTRAINT [PK_OrganizationApplications_OrganizationId_ApplicationId] PRIMARY KEY CLUSTERED ([OrganizationId], [ApplicationId]), 
    CONSTRAINT [FK_OrganizationApplications_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_OrganizationApplications_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id]) ON DELETE CASCADE,
)
GO