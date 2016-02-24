CREATE TABLE [dbo].[Position]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
    [OrganizationId] INT NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(1024) NULL, 
    [Value] NVARCHAR(100) NULL

    CONSTRAINT [UK_Position_Name] UNIQUE NONCLUSTERED ([Name] ASC), 
    CONSTRAINT [FK_Position_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Organization]([Id])
)
