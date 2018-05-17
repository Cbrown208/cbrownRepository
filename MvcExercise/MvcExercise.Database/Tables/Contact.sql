CREATE TABLE [dbo].[Contact]
(
    [ContactID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(40) NOT NULL, 
    [LastName] VARCHAR(40) NOT NULL, 
    [Telephone] VARCHAR(16) NULL, 
    [EmailAddress] VARCHAR(80) NOT NULL, 
    [BestTimeToCall] DATETIME NULL
)