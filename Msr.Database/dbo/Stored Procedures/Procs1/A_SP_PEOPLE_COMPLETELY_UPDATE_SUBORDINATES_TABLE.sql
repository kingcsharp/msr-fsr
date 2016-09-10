

CREATE PROCEDURE A_SP_PEOPLE_COMPLETELY_UPDATE_SUBORDINATES_TABLE
AS
--First delete Everything in the sunordinates table
DELETE FROM A_PEOPLE_SUB_LOOKUP_TABLE
--Now get a cursor for all people without a subordinate
Declare @onePerson nvarchar(50)
Declare @Cur Cursor
set @Cur = Cursor
For SELECT ID FROM A_APPROVED_PEOPLE p WHERE
NOT EXISTS (SELECT ID FROM A_APPROVED_PEOPLE s WHERE s.BOSS = p.ID)
 
open @Cur
Fetch Next from @Cur
Into @onePerson
while (@@fetch_status = 0)
Begin
	print 'This person has no subordinates' + @onePerson
	exec A_SP_PEOPLE_UPDATE_ONE_PERSON_SUB_TABLE @onePerson
	Fetch Next from @Cur
	Into @onePerson
End
close @Cur
Deallocate @Cur








