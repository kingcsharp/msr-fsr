CREATE VIEW dbo.Protal_ObjectsView
AS
SELECT        ID AS Id, OBJ_REF_ID AS ObjectRefId, OBJ_TABLE AS ObjectTable, OBJ_ID AS ObjectId, OBJ_DESC AS ObjectDesc, STATUS AS Status, CREATING_CO AS CreatingCo, REV AS Rev, 
                         CREATING_CO_NAME AS CreatingCoName
FROM            dbo.A_V_APPROVED_OBJECTS
GO

