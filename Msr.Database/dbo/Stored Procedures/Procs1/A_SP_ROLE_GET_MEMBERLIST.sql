





CREATE    PROCEDURE A_SP_ROLE_GET_MEMBERLIST
	@memberList varchar(8000) OUTPUT,
	@ID nvarchar(50),
	@strNTLogin nvarchar(50)
AS
print 'Getting all the members of the role = ' + @ID
Declare @oneStep nvarchar(50)
Declare @stepCursor Cursor
set @stepCursor = Cursor
For SELECT PERSON FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS WHERE ROLE_ID = @ID
open @stepCursor
Fetch Next from @stepCursor
Into @oneStep
while (@@fetch_status = 0)
	Begin
	print @oneStep
	set @memberList = isNull(@memberList + ',','') + @oneStep
	Fetch Next from @stepCursor
	Into @oneStep
	End
close @stepCursor
Deallocate @stepCursor		





