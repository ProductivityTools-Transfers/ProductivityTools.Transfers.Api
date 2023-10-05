CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	Name VARCHAR(100),
	CONSTRAINT [PK_Account] PRIMARY KEY ([AccountId])
)


CREATE TABLE [dbo].[Transfer](
	[TransferId] [int] IDENTITY(1,1) NOT NULL,
	[Pillow] DECIMAL(7,2) NULL,
	[SourceId] INT,
	[TargetId] INT,
	[TransferDay] INT,
	[Value] DECIMAL(7,2),
	CONSTRAINT [PK_Transfer] PRIMARY KEY ([TransferId])
	)

ALTER TABLE [dbo].[Trasnfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_TransferHistory] FOREIGN KEY([SourceId])
REFERENCES [dbo].Account(AccountId)
GO

ALTER TABLE [dbo].[Trasnfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_TransferHistory] FOREIGN KEY([TargetId])
REFERENCES [dbo].Account(AccountId)
GO
