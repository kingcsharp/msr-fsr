


CREATE PROCEDURE A_SP_ROLES_UPDATE_ALL_ASSIGNMENT_STATUSES
AS
declare @myCurs as CURSOR
Set @myCurs = Cursor for SELECT ID FROM A_ROLES
declare @ID nvarchar(50)
Open @myCurs
Fetch Next from @myCurs Into @ID
while (@@fetch_status = 0)
	Begin
		exec A_SP_ROLES_FINISH_APPROVAL_WF @ID,'A_SP_ROLES_UPDATE_ALL_ASSIGNMENT_STATUSES'
		Fetch Next from @myCurs Into @ID
	End




