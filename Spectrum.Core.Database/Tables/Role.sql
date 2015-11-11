CREATE TABLE [dbo].[Role]
(
    [Id] INT          NOT NULL IDENTITY(1000, 1),
    [Name]   NVARCHAR(128) NOT NULL,
    [OrganizationId] INT NOT NULL, 
    [Description] NVARCHAR(256) NULL, 
    [ApplicationId] INT NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL DEFAULT (GETDATE()),
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL DEFAULT (GETDATE()), 
	[ModifiedByUserId]	   INT					  NULL

    CONSTRAINT [PK_Role_RoleID] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Role_Name] UNIQUE NONCLUSTERED ([Name] ASC), 
    CONSTRAINT [FK_Role_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]),
	CONSTRAINT [FK_Role_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id])
);
