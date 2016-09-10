
CREATE  PROCEDURE DBO.A_SP_PROJECT_CLOSE
@newID varchar(50) OUTPUT,
@msg varchar(4000) OUTPUT,
@ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Closing a project'
declare @tester varchar(50)
SELECT @tester = ID FROM A_PROJECTS WHERE (INITIATOR = @strNTLogin OR LEADER = @strNTLogin) AND ID = @ID
	if @tester is null
		begin
		print 'Error You can not close this project'
		goto fin
		end
UPDATE A_PROJECTS SET STATUS = 'CLOSED' WHERE ID = @ID



fin:

