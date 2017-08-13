CREATE VIEW [dbo].[Portal_ApprovalWorkflowsView]
	AS 
SELECT NAME as Name,
Id As Id, STAMP_ID as Stamp_Id, STAMP_NAME as Stamp_Name, CREATING_CO as Creating_Co, OBJECT_ID as Object_Id, HIDE as Hide
	FROM A_O_WORKFLOWS
