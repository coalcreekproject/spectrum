CREATE TABLE [dbo].[Message]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Subject] NVARCHAR(256) NULL, 
    [Urgent] BIT NULL, 
    [To] NVARCHAR(MAX) NULL,
    [ToEmail] NVARCHAR(MAX) NULL,
    [From] NVARCHAR(MAX) NULL,
    [FromEmail] NVARCHAR(MAX) NULL,
    [Cc] NVARCHAR(MAX) NULL,
    [Bcc] NVARCHAR(MAX) NULL,
    [Body] NVARCHAR(MAX) NULL, 
    [Encoding] NVARCHAR(128) NULL,
    [DisplayEncoding] NVARCHAR(128) NULL,
    [Relay] NVARCHAR(256) NULL,
    [RelayEmail] NVARCHAR(256) NULL,
)
