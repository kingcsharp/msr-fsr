CREATE TABLE [dbo].[Portal_Task_Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] int NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[EndTime] [datetime2](7) NULL,
	[UserId] [nvarchar](50) NOT NULL,
	[TotalTime] TIME NOT NULL,
	[StatusId] [Int] NOT NULL,
	[FillId] int NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


