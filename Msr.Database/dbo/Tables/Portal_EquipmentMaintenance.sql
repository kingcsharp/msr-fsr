CREATE TABLE [dbo].[Portal_EquipmentMaintenance](
	[Id] [varchar](50) NOT NULL,
	[ObjectId] [varchar](50) NULL,
	[ScanBarcode] [nvarchar](130) NULL,
	[PrimaryLocationId] [nvarchar](100) NOT NULL,
	[SubLocationFirstId] [varchar](50) NULL,
	[SubLocationSecondId] [varchar](50) NULL,
	[DateTime] [datetime] NULL,
	[Technician] [nvarchar](50) NULL,
	[TroubleState] [bit] NULL,
	[MaintenanceTask] [nvarchar](50) NULL,
	[Comments] [nvarchar](4000) NULL,
	[Status] [nvarchar](50) NULL,
	[StrNTLogin] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PortalEquipmentMaintenance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

