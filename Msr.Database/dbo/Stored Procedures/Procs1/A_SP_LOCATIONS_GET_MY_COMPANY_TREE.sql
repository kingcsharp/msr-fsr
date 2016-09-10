CREATE PROCEDURE DBO.A_SP_LOCATIONS_GET_MY_COMPANY_TREE
@strNTLogin varchar(50)
AS
declare @rootCo varchaR(50)
SELECT @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin

CREATE TABLE 
#tempCoLocTree
	(ID varchar(50),LEV int,HAS_CHILD smallint,EXPANDED smallInt,idt smallint,idt2 smallInt Identity,
	NAME nvarchar(2000),ROOT_ID varchar(50)
	)

Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT a.ID FROM A_LOCATIONS a,A_LOCATIONS_HISTORY h,A_OBJECTS o
	WHERE 
	a.HISTORY_REF_ID = h.ID
	and h.OBJECT_ID = o.ID
	and PARENT_LOCATION IS NULL
	and o.CREATING_CO = @rootCo
	ORDER BY h.NAME
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
	Begin
	INSERT INTO #tempCoLocTree
		(LEV,EXPANDED,HAS_CHILD,NAME,ROOT_ID)
	exec A_SP_LOCATION_GET_TREE @it,@it,@it,@strNTLogin
	Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs



