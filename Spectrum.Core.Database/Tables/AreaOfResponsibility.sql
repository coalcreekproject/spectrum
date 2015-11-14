CREATE TABLE [dbo].[AreaOfResponsibility]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[OrganizationId] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(250) NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL,
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL, 
	[ModifiedByUserId]	   INT					  NULL

    CONSTRAINT [FK_AreaOfResponsibility_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id]) ON DELETE CASCADE
)
