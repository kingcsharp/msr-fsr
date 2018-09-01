CREATE VIEW Portal_ObjectSearch
AS

SELECT NEWID() AS Id, ObjectId AS ObjectId, Name, '' AS Description, CreateDate AS Date,'Parts' AS ItemType FROM Portal_PartsView

UNION

SELECT NEWID() AS Id, ObjectId AS ObjectId, Name, '' AS Description, CreatedDate AS Date,'PartsTypes' AS ItemType FROM Portal_PartTypesView

UNION

SELECT NEWID() AS Id, ObjectId AS ObjectId, PartDesc AS Name, '' AS Description, DRCM AS Date,'ActualParts' AS ItemType FROM Portal_ActualPartsView

UNION

SELECT NEWID() AS Id, ObjectId AS ObjectId, TITLE AS Name, StepText AS Description, CreatedDate AS Date,'Templates' AS ItemType FROM Portal_PrePropSearchView


UNION

SELECT NEWID() AS Id, ObjectId AS ObjectId, Name AS Name, '' AS Description, DRCM AS Date, 'Companies' AS ItemType FROM Portal_CompanyView


UNION

SELECT NEWID() AS Id, ObjectId AS ObjectId, Name, COMMENTS AS Description, GETDATE() AS Date, 'Procedures' AS ItemType FROM Portal_ProceduresView

UNION

SELECT NEWID() AS Id, ObjectId AS ObjectId, Name, COMMENTS AS Description, GETDATE(), 'Documents' AS ItemType FROM Portal_DocumentsView

UNION

SELECT NEWID() AS Id, ObjectId AS ObjectId, MonitorType,  Description AS Description, GETDATE() AS Date, 'Monitors' as ItemType FROM Portal_MonitorView

UNION

SELECT NEWID() AS Id, STEP_ID AS ObjectId, TITLE AS Name, Description AS Description,  GETDATE() AS Date, 'Steps' AS ItemType  FROM A_V_TASKS_WITH_PROCEDURE_STEP_DATA
