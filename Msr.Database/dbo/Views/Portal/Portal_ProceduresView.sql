CREATE VIEW dbo.Portal_ProceduresView
AS
SELECT        PH.SPECIAL_ROOT AS SpecialRoot, PH.SPECIAL_ID AS SpecialId, PH.ID AS Id, PH.OBJECT_ID AS ObjectId, PH.VERB AS Verb, PH.NAME AS Name, PH.SECURITY_LEVEL AS SecurityLevel, PH.LOCKED_BY AS LockedBy, 
                         PH.CREATED_BY AS CreatedBy, PH.ROOT AS Root, PH.CREATING_CO AS CreatingCo, PH.STATUS AS Status, PH.REV AS Rev, PH.LOCKED_BY_NAME AS LockedByName, PH.CREATING_CO_NAME AS CreatingCoName, 
                         PH.VERB_NAME AS VerbName, PH.VERB_ID AS VerbId, PH.OBJ_ID AS ObjId, PH.CREATING_DEPT AS CreatingDept, PH.DEPT_NAME AS DeptName, PH.SECURITY_NAME AS SecurityName, PH.IS_SYSTEM AS IsSystem, 
                         AP.COMMENTS AS Comments, AP.STEPS_IN_AP AS StepInAp, AP.WIP_MSG AS WipMsg, AP.DURATION AS Duration, AP.DURATION_TYPE AS DurationType, AP.SYSTEM_ID AS SystemId, AP.Threshold
FROM            dbo.A_V_PROCEDURE_HISTORY_SEARCH AS PH LEFT OUTER JOIN
                         dbo.A_O_PROCEDURES AS AP ON AP.OBJECT_ID = PH.OBJECT_ID
GO
