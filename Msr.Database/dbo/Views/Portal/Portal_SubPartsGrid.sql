



CREATE VIEW [dbo].[Portal_SubPartsGrid]
AS

SELECT DISTINCT
CONCAT(parent.id, subAP.id) AS ID,
subP.COMPANY_PART_NUMBER AS SUB_PART_CO_NUM,
subP.NAME AS SUB_PART_NAME,
parent.ID AS ROOT,
subP.ID AS SUB_PART_ID,
subAP.ID AS ACTUAL_SUB_PART_ID,
parent.OBJECT_ID AS PARENT_OBJECT_ID,
psp.QTY,
psp.ID AS PSPID,
psp.NICK_NAME,
subAP.SERIAL AS SerialNumber,
fs.id AS FILL_ID,
cc.CycleCount
FROM dbo.A_PARTS_HISTORY parent
INNER JOIN A_PARTS_SUB_PARTS psp ON parent.ID = psp.PARENT
INNER JOIN A_V_PARTS_APPROVED_DATA_QUICK subP ON psp.PART_ID = subP.ID
INNER JOIN a_v_actual_parts_approved_data_quick subAP on subP.id = subAP.part_id
LEFT JOIN a_v_fills_search fs on (subAP.parent_id = fs.fill_obj_id)
OUTER APPLY (
    SELECT COUNT(1) as CycleCount
    FROM PartsTransactionLog L
    WHERE L.PartId = subAP.PART_ID
    AND L.SerialNumber = subAP.SERIAL
) cc
;

GO


