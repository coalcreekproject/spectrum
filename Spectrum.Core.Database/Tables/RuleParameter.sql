CREATE TABLE [dbo].[RuleParameter]
(
	[Id] INT NULL,
	[RuleId] INT NOT NULL,
    [ParameterId] INT NOT NULL,

	CONSTRAINT [PK_RuleParameter_RuleId_ParameterId] PRIMARY KEY ([RuleId], [ParameterId]),
	CONSTRAINT [FK_RuleParameter_RuleId] FOREIGN KEY ([RuleId]) REFERENCES [Rule]([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_RuleParameter_ParameterId] FOREIGN KEY ([ParameterId]) REFERENCES [Parameter]([Id]) ON DELETE CASCADE
)
GO