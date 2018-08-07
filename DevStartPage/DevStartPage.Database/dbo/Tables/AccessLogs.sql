CREATE TABLE [dbo].[AccessLogs]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Ip] VARCHAR(100) NOT NULL, 
	[Description] VARCHAR(100) NULL, 
    [LastSeenOn] DATETIME NOT NULL, 
    [VisitCount] INT NULL DEFAULT 0
)
