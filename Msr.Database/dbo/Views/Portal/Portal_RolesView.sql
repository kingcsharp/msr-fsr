

CREATE VIEW [dbo].[Portal_RolesView]
AS
SELECT DISTINCT 
                         SECURITY_LEVEL AS SecurityLevel, 
						 SECURITY_LEVEL_NAME AS SecurityLevelName, 
						 ROOT AS Root, 
						 ID AS Id, 
						 ROLE_ID,
						 ROLE_NAME AS RoleName, 
						 COMMENTS as Comments,
						 TRAININGIDREV as TrainingIdRev,
						 OBJ_ID AS ObjectId, 
						 STATUS AS Status, 
						 LOCKED_BY AS LockedBy, 
						 UNLOCKED_BY AS UnLockedBy, 
						 CREATED_BY AS CreatedBy, 
						 CREATING_CO AS CreatingCo, 
						 REV AS Revision, 
						 WFS_ID AS WFSID, 
						 HIDDEN AS Hidden, 
						 SOURCE AS Source, 
                         LOCKED_BY_NAME AS LockedByName
FROM            dbo.A_V_ROLES_WITH_ASSIGNEES


GO
