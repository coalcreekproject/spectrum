CREATE TABLE [dbo].[Organization]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [Name] NVARCHAR(128) NOT NULL, 
    [OrganizationTypeId] INT NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL,
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL, 
	[ModifiedByUserId]	   INT					  NULL
	
    CONSTRAINT [FK_Organization_OrganizationType] FOREIGN KEY ([OrganizationTypeId]) REFERENCES [OrganizationType]([Id]) 
)
