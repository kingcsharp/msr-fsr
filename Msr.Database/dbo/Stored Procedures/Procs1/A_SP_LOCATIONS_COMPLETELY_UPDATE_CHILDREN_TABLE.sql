
CREATE PROCEDURE A_SP_LOCATIONS_COMPLETELY_UPDATE_CHILDREN_TABLE
AS
--First delete Everything in the child table
DELETE FROM A_LOCATIONS_CHILD_LOOKUP
--Now get a cursor for all Locations without a child
Declare @oneLocation varchar(50)
Declare @Cur Cursor
set @Cur = Cursor
For SELECT ID FROM A_LOCATIONS_APPROVED_DATA l WHERE
NOT EXISTS (SELECT ID FROM A_LOCATIONS_APPROVED_DATA s WHERE s.PARENT_LOCATION = l.ID)
open @Cur
Fetch Next from @Cur Into @oneLocation
while (@@fetch_status = 0)
Begin
	print 'This location has no child' + @oneLocation
	exec A_SP_LOCATIONS_UPDATE_ONE_LOCATION_CHILD_TABLE @oneLocation
	Fetch Next from @Cur Into @oneLocation
End
close @Cur
Deallocate @Cur


