





CREATE   PROCEDURE A_SP_OBJECT_SHOW_APPLICABLE_WORKFLOWS 
	@objID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
declare @ApprovalAct as nvarchar(50)
exec A_SP_OBJECT_GET_APPROVAL_ACTIVITY @ApprovalAct OUTPUT,@objID
print 'Approval Act = ' + @ApprovalAct
SELECT * FROM A_V_WORKFLOWS_FOR_ACTIVITIES 
WHERE ACTIVITY = @ApprovalAct AND CREATING_CO = (SELECT dbo.getCompany(@strNTLogin))
ORDER BY WF_NAME






