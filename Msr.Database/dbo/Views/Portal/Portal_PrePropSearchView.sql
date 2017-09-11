CREATE VIEW dbo.Portal_PrePropSearchView
AS
SELECT        dbo.A_PROCEDURE_STEPS.ID, p.OBJECT_ID AS ObjectId,
o.LOCKED_BY AS LockedBy, o.UNLOCKED_BY AS UnlockedBy, o.CREATED_BY AS CreatedBy, 
o.CREATE_DATE AS CreatedDate, o.ROOT, o.REV_INFO AS RevInfo, 
                         o.CREATING_CO AS CreatingCo, o.STATUS, o.REV, o.WFS_ID AS WfsId, 
						 o.LOCKED_BY_NAME AS LockedByName, o.CREATING_CO_NAME AS CreatingCoName, o.APPROVAL_ACTIVITY AS ApprovalActivity, o.OBJ_ID AS ObjId, 
                         p.PROC_STEP_ID AS ProcStepId, dbo.A_PROCEDURE_STEPS.STEP_TEXT AS StepText, dbo.A_PROCEDURE_STEPS.COMMENTS, dbo.A_PROCEDURE_STEPS.DURATION, 
                         dbo.A_PROCEDURE_STEPS.DURATION_TYPE AS DurationType, dbo.A_PROCEDURE_STEPS.REFERENCE_OBJECT AS ReferenceObject, dbo.A_PROCEDURE_STEPS.REFERENCE_VERB AS ReferenceVerb, 
                         dbo.A_PROCEDURE_STEPS.SYSTEM_TASK AS SystemTask, dbo.A_PROCEDURE_STEPS.START_ON_COUNTER AS StartOnCounter,
						 dbo.A_PROCEDURE_STEPS.TITLE
						 
FROM            dbo.A_OBJECTS AS o INNER JOIN
                         dbo.A_PREPOP_HISTORY AS p ON o.ID = p.OBJECT_ID INNER JOIN
                         dbo.A_PROCEDURE_STEPS ON p.PROC_STEP_ID = dbo.A_PROCEDURE_STEPS.ID
GO


