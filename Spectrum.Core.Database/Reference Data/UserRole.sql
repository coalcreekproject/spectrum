--MERGE INTO [dbo].[UserRole] AS Target
--USING (VALUES

--    (1, N'Administrator'),
--    (2, N'Moderator')

--) AS Source ([Id], [Name])
--ON Target.[Id] = Source.[Id]
---- Update matched rows
--WHEN MATCHED THEN
--UPDATE SET [Name] = Source.[Name]
---- Insert new rows
--WHEN NOT MATCHED BY TARGET THEN
--INSERT ([Id], [Name])
--VALUES ([Id], [Name])
---- Delete rows that are in the target but not the source
--WHEN NOT MATCHED BY SOURCE THEN
--DELETE;
--GO