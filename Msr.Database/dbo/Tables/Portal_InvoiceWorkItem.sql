CREATE TABLE [dbo].[Portal_InvoiceWorkItem]
(
	Id int PRIMARY KEY IDENTITY(1,1),
    ItemId nvarchar(50),
    InvoiceId nvarchar(50),
    RefPo nvarchar(100)
)
