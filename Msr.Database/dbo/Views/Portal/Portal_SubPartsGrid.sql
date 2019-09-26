CREATE VIEW [dbo].[Portal_SubPartsGrid]
AS

SELECT
distinct
subp.COMPANY_PART_NUMBER AS SUB_PART_CO_NUM,
subp.NAME AS SUB_PART_NAME,
parent.ID AS ROOT,
subp.ID AS SUB_PART_ID,
parent.OBJECT_ID AS PARENT_OBJECT_ID,
psp.QTY,
psp.ID,
psp.NICK_NAME,
p.SerialNumber,
psp.part_id,
wo.CycleCount
FROM dbo.A_PARTS_HISTORY parent
INNER JOIN dbo.A_PARTS_SUB_PARTS psp ON parent.ID = psp.PARENT
INNER JOIN dbo.A_V_PARTS_APPROVED_DATA_QUICK subp ON psp.PART_ID = subp.ID
INNER JOIN PartsTransactionLog p ON p.PartId=parent.Object_Id
INNER JOIN Portal_workOrders wo ON subp.ID = wo.partid

GO


