
CREATE  PROCEDURE dbo.A_SP_ROLES_SAVE_SUB_DEPARTMENT
@newID varchar(50) OUTPUT,
@messages varchar(2000) OUTPUT,
@ID varchar(50),
@ROLE_HIST_ID varchar(50),
@DEPT varchar(50),
@SUB_ROLE varchar(50),
@strNTLogin varchar(50)
AS
if @ID is null
	begin
		print 'ID is Null we need to create this Sub Dept'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		INSERT INTO A_ROLES_DEPARTMENT_SUB_ROLES
			(ID,ROLE_HIST_ID,DEPARTMENT,MODBY,DRCM,SUB_ROLE) 
		VALUES
			(@newID,@ROLE_HIST_ID,@DEPT,@strNTLogin,getDATE(),@SUB_ROLE)
	end
else
	begin
	set @newID = @ID
	UPDATE A_ROLES_DEPARTMENT_SUB_ROLES
	SET
	DEPARTMENT = @DEPT,
	DRCM = getDate(),
	SUB_ROLE = @SUB_ROLE,
	MODBY = @strNTLogin
	WHERE ID = @ID
	end

	


