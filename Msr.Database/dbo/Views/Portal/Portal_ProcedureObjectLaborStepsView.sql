CREATE VIEW [dbo].[Portal_ProcedureObjectLaborStepsView]
AS
SELECT        ROLE_NAME AS RoleName, PROCEDURE_ID AS ProcedureId, STEP_ID AS StepId, ROLE_ID AS RoleId, RELATIONSHIP AS RelationShip, LABOR_ROLE AS LaborRole, QTY AS Qty, QTY_TYPE AS QtyType, 
                         OBJ_REF_ID AS ObjRefId, OBJ_ID AS ObjId, OBJ_DESC AS ObjDesc, ID AS Id
FROM            dbo.A_V_PROCEDURE_STEP_LABOR