CREATE VIEW [dbo].[Portal_SubPartsGrid]
AS

SELECT   distinct  subP.COMPANY_PART_NUMBER AS SUB_PART_CO_NUM, subP.NAME AS SUB_PART_NAME, parent.ID AS ROOT, subP.ID AS SUB_PART_ID, 
                      parent.OBJECT_ID AS PARENT_OBJECT_ID, link.QTY, link.ID, link.NICK_NAME,
					  p.SerialNumber
FROM         dbo.A_PARTS_HISTORY parent INNER JOIN
                      dbo.A_PARTS_SUB_PARTS link ON parent.ID = link.PARENT INNER JOIN
                      dbo.A_V_PARTS_APPROVED_DATA_QUICK subP ON link.PART_ID = subP.ID INNER join PartsTransactionLog p on p.PartId=parent.Object_Id

GO


