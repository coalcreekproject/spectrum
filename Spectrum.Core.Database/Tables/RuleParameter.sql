CREATE TABLE [dbo].[RuleParameter]
(
	[RuleId] INT NOT NULL,
    [ParameterId] INT NOT NULL,
    [Cloaked]			   BIT					  NULL, 
    [Archive]			   BIT					  NULL, 
    [CreatedDate]		   DATETIME				  NULL,
	[CreatedByUserId]	   INT					  NULL,
    [ModifiedDate]		   DATETIME				  NULL, 
	[ModifiedByUserId]	   INT					  NULL

	CONSTRAINT [PK_RuleParameter_RuleId_ParameterId] PRIMARY KEY ([RuleId], [ParameterId]),
	CONSTRAINT [FK_RuleParameter_RuleId] FOREIGN KEY ([RuleId]) REFERENCES [Rule]([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_RuleParameter_ParameterId] FOREIGN KEY ([ParameterId]) REFERENCES [Parameter]([Id]) ON DELETE CASCADE
)

GO

CREATE NONCLUSTERED INDEX [IX_RuleParameter_RuleId] 
	ON [dbo].[RuleParameter] ([RuleId])

GO
CREATE NONCLUSTERED INDEX [IX_RuleParameter_ParameterId] 
	ON [dbo].[RuleParameter] ([ParameterId])
