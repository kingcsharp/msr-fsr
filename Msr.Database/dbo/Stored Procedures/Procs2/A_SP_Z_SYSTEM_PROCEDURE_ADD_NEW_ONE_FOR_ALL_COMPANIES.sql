



CREATE     PROCEDURE dbo.A_SP_Z_SYSTEM_PROCEDURE_ADD_NEW_ONE_FOR_ALL_COMPANIES
@id varchar(50),
@NAME nvarchar(200)
AS
BEGIN TRANSACTION
print 'Inside A_SP_Z_SYSTEM_PROCEDURE_ADD_NEW_ONE_FOR_ALL_COMPANIES'
Declare @it nvarchar(50),@coName varchar(1000)
Declare @curs Cursor
declare @strNTLogin varchar(50)
set @curs = Cursor For SELECT ID,NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE PARENT is NULL
open @curs
Fetch Next from @curs Into @it,@coName
while (@@fetch_status = 0)
Begin
	print 'Adding System Procedure for Company = ' + @coName
	set @strNTLogin = NULL
	SELECT TOP 1 @strNTLogin = ID FROM A_V_PEOPLE_APPROVED_DATA WHERE ROOT_COMPANY = @it
	if @strNTLogin is null
		begin
			print 'There was nobody with the company ' + @coName
			SELECT TOP 1 @strNTLogin = ID FROM A_V_PEOPLE_APPROVED_DATA WHERE ROOT_COMPANY = @it
		end
	if @strNTLogin is null
		begin
			print 'There was nobody in this company at all so I can not make this system Task'
			goto skip
		end 
	print 'Wanted to call A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK ''' + @id + ''',''' + @name + ''',''' + @strNTLogin + ''''
	exec A_SP_Z_SYSTEM_TASKS_MAKE_A_SYSTEM_TASK @id,@name,@strNTLogin
	skip:
	Fetch Next from @curs Into @it,@coName
End
close @curs
Deallocate @curs

if @@ERROR <> 0 GOTO PROBLEM
fin:
COMMIT TRANSACTION
RETURN 0
PROBLEM:
ROLLBACK TRANSACTION
RETURN 1




