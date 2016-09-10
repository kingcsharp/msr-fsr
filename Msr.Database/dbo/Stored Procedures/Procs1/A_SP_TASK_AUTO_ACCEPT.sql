



CREATE   PROCEDURE A_SP_TASK_AUTO_ACCEPT 
@ID varchar(50),
@strNTLogin varchar(50)
AS
print 'This function is not going to be used anymore'
goto fin


declare @requestee as varchar(50)
SELECT @requestee = REQUESTEE_ID FROM A_TASKS WHERE ID = @ID
if @requestee is null
	begin
		goto fin
	end
declare @tester as varchar(50)
SELECT @tester = ID FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE BOSS = @strNTlogin and SUBORDINATE = @requestee
if (@tester is not null) OR (@strNTlogin = @requestee)
	begin
	UPDATE A_TASKS SET STATUS = 'ACCEPTED' WHERE ID = @ID
	UPDATE A_TASK_ASSIGNEE SET ASSIGNEE_CURRENTLY_ACCEPTED = 1,ASSIGN_DATE = getDate(),ASSIGNER=@strNTlogin,
	ACCEPTED_DATE = getDate(),STATUS='ACCEPTED' WHERE TASK_ID = @ID AND ACTIVE = 1 AND PERSON_ASSIGNED = @requestee
	end

fin: 




