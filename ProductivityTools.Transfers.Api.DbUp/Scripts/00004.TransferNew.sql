CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	Name VARCHAR(100),
	[Pillow] DECIMAL(7,2) NOT NULL,
	Type VARCHAR(20),
	Number VARCHAR(24),
	CONSTRAINT [PK_Account] PRIMARY KEY ([AccountId])
)


CREATE TABLE [dbo].[Transfer](
	[TransferId] [int] IDENTITY(1,1) NOT NULL,
	[SourceId] INT,
	[TargetId] INT,
	[TransferDay] INT,
	[Value] DECIMAL(7,2) NOT NULL,
	CONSTRAINT [PK_Transfer] PRIMARY KEY ([TransferId])
	)

Go

ALTER TABLE [dbo].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_TransferHistory_Source] FOREIGN KEY([SourceId])
REFERENCES [dbo].Account(AccountId)
GO

ALTER TABLE [dbo].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_TransferHistory_Target] FOREIGN KEY([TargetId])
REFERENCES [dbo].Account(AccountId)
GO
