CREATE VIEW [dbo].[Portal_ProcedureTypesView]
AS
SELECT        ID AS Id,
              NAME AS Name,
			  OBJECT_ID AS ObjectId,
			  LOCKED_BY AS LockedBy, 
			  UNLOCKED_BY AS UnLockedBy, 
			  CREATED_BY AS CreatedBy, 
			  CREATE_DATE AS CreateDate, 
			  ROOT AS Root, 
			  REV_INFO AS RevInfo, 
			  CREATING_CO AS CreatingCo, 
			  STATUS AS Status, 
			  REV AS Revision, 
			  WFS_ID AS WFSID, 
			  LOCKED_BY_NAME AS LockedByName, 
			  CREATING_CO_NAME AS CreatingCoName, 
              APPROVAL_ACTIVITY AS ApprovalActivity, 
			  OBJ_ID AS ObjId, 
			  VERB_TYPE AS VerbType, 
			  VERB_TYPE_NAME AS VerbTypeName
FROM            dbo.A_O_TT_VERBS_HISTORY


GO


