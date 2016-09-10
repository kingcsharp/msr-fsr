

CREATE   PROCEDURE dbo.A_SP_Z_ADMIN_ROLE_JOB_ADD_NEW_JOB
@NAME nvarchar(200)
AS
BEGIN TRANSACTION
print 'Inside A_SP_Z_ADMIN_ROLE_JOB_ADD_NEW_JOB'
Declare @it nvarchar(50), @curs Cursor, @tester as varchar(50), @myRole as varchar(50)
set @curs = Cursor For SELECT ID FROM A_V_COMPANIES_APPROVED_DATA WHERE PARENT is NULL
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Job for Company = ' + @it
	set @tester = NULL
	print 'Need to get an admin role for the company = ' + @it
	SELECT top 1 @myRole = ROOT FROM A_APPROVED_ROLES WHERE IS_ADMIN = 1 and CREATING_CO = @it
	if @myRole is not null
	begin
		print 'We got a role = ' + @myRole
		print 'Now we need to add it to the table'
		SELECT @tester = ID FROM A_ADMIN_ROLE_JOBS WHERE CO_ID = @it and JOB = @NAME
		if @tester is null
		begin
			INSERT INTO A_ADMIN_ROLE_JOBS (ID,CO_ID,ROLE_ID,JOB,DRCM,MODBY)
				VALUES (newID(),@it,@myRole,@name,getdate(),'SYSTEM')
				if @@ERROR <> 0 GOTO PROBLEM
		end
	end
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
fin:
COMMIT TRANSACTION
RETURN 0
PROBLEM:
print 'Error Inserting'
ROLLBACK TRANSACTION
RETURN 1


