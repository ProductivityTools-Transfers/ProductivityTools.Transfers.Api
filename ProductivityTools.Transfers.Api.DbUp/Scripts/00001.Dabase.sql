CREATE TABLE [dbo].Transfer(
TransferId INT PRIMARY KEY IDENTITY(1,1), 
Date DATE,
Category NVARCHAR(100),
Name NVARCHAR (100), 
VALUE decimal(7,2))