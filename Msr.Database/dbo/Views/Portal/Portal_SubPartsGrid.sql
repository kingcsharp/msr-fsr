


CREATE VIEW [dbo].[Portal_SubPartsGrid]
AS

SELECT
distinct
concat(parent.id, subAP.id) as ID,
subP.COMPANY_PART_NUMBER AS SUB_PART_CO_NUM,
subP.NAME AS SUB_PART_NAME,
parent.ID AS ROOT,
subP.ID AS SUB_PART_ID,
subAP.ID as ACTUAL_SUB_PART_ID,
parent.OBJECT_ID AS PARENT_OBJECT_ID,
psp.QTY,
psp.ID as PSPID,
psp.NICK_NAME,
subAP.SERIAL as SerialNumber,
fs.id as FILL_ID,
isnull(wo.cyclecount, 0) as CycleCount
FROM dbo.A_PARTS_HISTORY parent
INNER JOIN A_PARTS_SUB_PARTS psp ON parent.ID = psp.PARENT
INNER JOIN A_V_PARTS_APPROVED_DATA_QUICK subP ON psp.PART_ID = subP.ID
inner join a_v_actual_parts_approved_data_quick subAP on subP.id = subAP.part_id
left join a_v_fills_search fs on (subAP.parent_id = fs.fill_obj_id)
left join Portal_workOrders wo on wo.partid = subP.ID

GO


