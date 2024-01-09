CREATE VIEW dbo.TransferHistorySnapshot
AS
SELECT Convert(date, getdate()) as Date
	  ,s.Name as Source
	  ,t.Name as Target
      ,[Value]
      ,[ValueComment]
  FROM [dbo].[Transfer]
  join dbo.Account s on s.AccountId=SourceId
  join dbo.Account t on t.AccountId=TargetId
