CREATE               PROCEDURE [dbo].[A_SP_TASK_ASSUME_CONTROL] 
@RET_STATUS as varchar(500) OUTPUT,
@MSGS as varchar(500) OUTPUT,
@ID as varchar(50),
@strNTLogin as varchar(50)
AS
print 'Assuming a task'
print 'Verifying that I am a person who can accept it'
declare @requestee as varchar(50)
declare @roleRequestee as varchar(50)
SELECT @requestee = REQUESTEE_ID,@roleRequestee = GROUP_REQUESTEE_ID FROM A_TASKS WHERE ID = @ID
declare @tester as varchar(50),@taskStat varchar(50)
declare @stepRoles as nvarchar(max)
SELECT @taskStat = STATUS FROM A_TASKS WHERE ID = @ID              

SELECT @StepRoles=(SELECT Roles FROM A_V_TASKS_WITH_PROCEDURE_STEP_DATA WHERE STEP_ID=@ID)

 

 IF NOT EXISTS(SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON=@strNTLogin AND (ROLE_ID 
 IN (SELECT * from dbo.SplitString(@StepRoles)) 
 OR
 ROLE_ID IN (SELECT ROLE_ID FROM A_V_PROCEDURE_ROLES_TO_VIEW
 WHERE PROCEDURE_OBJ_ID IN ( SELECT ProcObjId FROM Portal_WorkOrders
 WHERE TaskId = @ID)))
 )
	BEGIN
	SET @RET_STATUS = 'ERROR - You are not a member of the role that this task was assigned to.'
	GOTO fin
    END

ELSE
   BEGIN
IF @taskStat not in ('ACCEPTED')
	BEGIN
	SET @RET_STATUS = 'ERROR - This task is not started so it can not be assumed.'
	GOTO fin
	END

declare @fwdID varchar(50),@fwdMSG varchar(50)
exec A_SP_TASKS_FORWARD_TASK 
@fwdID OUTPUT,
@fwdMSG OUTPUT,
@ID,@strNTLogin,null,@requestee

declare @accStat varchar(50),@accMSG varchar(50)
exec A_SP_TASK_ACCEPT 
@accStat OUTPUT,
@accMSG OUTPUT,
@ID,@strNTLogin


fin:
End 












