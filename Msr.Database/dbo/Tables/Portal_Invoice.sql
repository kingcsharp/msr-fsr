CREATE TABLE [dbo].[Portal_Invoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Client] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [nvarchar](100) NULL,
	[CustPo] [varchar](50) NULL,
	[InvoiceDate] [datetime] NULL,
	[TotalDue] [float] NULL,
	[Tax] [float] NULL,
	[PaymentsAndCredits] [float] NULL,
	[Debits] [float] NULL,
	[LateFees] [float] NULL,
	[Items] [nvarchar](max) NULL,
	[Supplier] [nvarchar](50) NULL,
	[InvoiceClass] [nvarchar](50) NULL,
 CONSTRAINT [PK__Portal_I__3214EC079A175DE5] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
