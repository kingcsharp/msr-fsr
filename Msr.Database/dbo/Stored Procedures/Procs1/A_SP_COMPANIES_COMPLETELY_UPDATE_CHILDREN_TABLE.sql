CREATE   PROCEDURE A_SP_COMPANIES_COMPLETELY_UPDATE_CHILDREN_TABLE
AS
--First delete Everything in the child table
DELETE FROM A_COMPANIES_CHILD_LOOKUP_TABLE
--Now get a cursor for all companies without a child
Declare @oneCompany varchar(50)
Declare @Cur Cursor
set @Cur = Cursor
For SELECT ID FROM A_APPROVED_COMPANIES p WHERE
NOT EXISTS (SELECT ID FROM A_APPROVED_COMPANIES s WHERE s.PARENT = p.ID)
open @Cur
Fetch Next from @Cur
Into @oneCompany
while (@@fetch_status = 0)
Begin
	print 'This company has no child' + @oneCompany
	exec A_SP_COMPANIES_UPDATE_ONE_COMPANY_CHILD_TABLE @oneCompany
	Fetch Next from @Cur
	Into @oneCompany
End
close @Cur
Deallocate @Cur

