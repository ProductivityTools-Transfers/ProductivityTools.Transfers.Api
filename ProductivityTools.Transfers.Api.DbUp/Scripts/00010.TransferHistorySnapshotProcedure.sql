CREATE PROCEDURE dbo.TodayTransferHistory
AS
DECLARE @Today DATE
SELECT top 1 @Today=[Date] FROM [PTTransfers].[dbo].[TransferHistorySnapshot]
SELECT @Today

DELETE [dbo].[TransferHistory] WHERE Date=@Today
INSERT INTO [dbo].[TransferHistory] (Date,Source,Target,Value,ValueComment)
Select Date,Source,Target,Value,ValueComment FROM dbo.TransferHistorySnapshot