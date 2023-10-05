CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	Name VARCHAR(100)
)


CREATE TABLE [dbo].[Trasfer](
	[TransferId] [int] IDENTITY(1,1) NOT NULL,
	[Pillow] DECIMAL(7,2) NULL,
	[SourceId] INT,
	[TransferDay] INT,
	[Value] DECIMAL(7,2),
	[TargetId] INT,
	CONSTRAINT [PK_Transfer] PRIMARY KEY ([TransferId])
	)

