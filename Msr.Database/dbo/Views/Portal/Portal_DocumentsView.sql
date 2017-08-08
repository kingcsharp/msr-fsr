CREATE VIEW dbo.Portal_DocumentsView
AS
SELECT DISTINCT 
                         SPECIAL_ROOT AS SpecialRoot, SPECIAL_ID AS SpecialID, ID AS Id, OBJECT_ID AS ObjectId, SECURITY_LEVEL AS SecurityLevel, CREATING_DEPT AS CreatingDept, OBJ_ID AS ObjId, LOCKED_BY AS LockedBy, 
                         CREATED_BY AS CreatedBy, ROOT AS Root, CREATING_CO AS CreatingCo, NAME AS Name, CREATING_CO_NAME AS CreatingCoName, DEPT_NAME AS DeptName, REV AS Rev, STATUS AS Status, 
                         LOCKED_BY_NAME AS LockedByName, SECURITY_NAME AS SecurityName, APPROVAL_DATE AS ApprovalDate,
						 '' AS Comments
FROM            dbo.A_O_THEORY_WITH_PARAGRAPHS
