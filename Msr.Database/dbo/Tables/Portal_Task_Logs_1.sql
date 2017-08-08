CREATE TABLE [dbo].[Portal_Task_Logs]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TaskId] NVARCHAR(50) NOT NULL, 
    [StartTime] DATETIME2 NOT NULL, 
    [EndTime] DATETIME2 NULL, 
    [UserId] NVARCHAR(50) NOT NULL
)
