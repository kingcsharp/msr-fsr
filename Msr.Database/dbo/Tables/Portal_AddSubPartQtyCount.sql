CREATE TABLE [dbo].[Portal_AddSubPartQtyCount]
(
Id int PRIMARY KEY identity(1,1),
CountValue int,
ParentId varchar(100),
Qty int,
ActualPartId varchar(100),
PartId varchar(50)
)
