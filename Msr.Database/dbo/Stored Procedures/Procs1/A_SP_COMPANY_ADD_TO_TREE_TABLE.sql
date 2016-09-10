

CREATE   PROCEDURE A_SP_COMPANY_ADD_TO_TREE_TABLE
@ID varchar(50),
@LEV integer,
@exAll smallint
AS
print 'First Checking to see if we are in the expand All List'
If @ID in (SELECT ID FROM #tempExpandAllList)
	begin
	print 'ID = ' + @ID + 'Is in the expand all list'
	set @exAll = 1
	end
print 'Insert My ID and Level into the tree table'
declare @tester as varchar(50)
SELECT TOP 1 @tester = ID FROM A_COMPANIES_HISTORY WHERE PARENT = (@ID)
print 'Checking for Child using ID = ' + @ID
if @tester is not null
	begin
	set @tester = '1'
	end
else
	set @tester = '0'

INSERT INTO #tempCoTree(ID,LEV,HAS_CHILD,EXPANDED) VALUES(@ID,@LEV,convert(smallInt,@tester),0)
IF ((@ID in (SELECT ID FROM #tempExpandList)) or (@exAll = 1))
	begin
	UPDATE #tempCoTree SET EXPANDED = 1 WHERE ID = @ID
	print 'This one needs to be expanded so make a cursor for children and do it'
	Declare @it nvarchar(50)
	Declare @curs Cursor
	set @curs = Cursor For SELECT a.ID FROM A_COMPANIES a,A_COMPANIES_HISTORY h 
	WHERE h.PARENT = @ID AND a.HISTORY_REF_ID = h.ID ORDER BY h.NAME
	open @curs
	Fetch Next from @curs Into @it
	set @LEV = @LEV + 1
	while (@@fetch_status = 0)
		Begin
		print 'Adding  a child ' + @it
		exec A_SP_COMPANY_ADD_TO_TREE_TABLE @it,@LEV,@exAll
		Fetch Next from @curs Into @it
		End
	close @curs
	Deallocate @curs
	end




